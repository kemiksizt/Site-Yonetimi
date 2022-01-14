using AutoMapper;
using Kemiksiz.DB.Entities.DataContext;
using Kemiksiz.Model;
using Kemiksiz.Model.Bill;
using Kemiksiz.Service.Card;
using Kemiksiz.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.Bill
{
    public class BillService : IBillService
    {
        private readonly IMapper mapper;
        private readonly IApartmentService apartmentService;
        private readonly ICardService cardService;
        private readonly IUserService userService;

        public BillService(IMapper _mapper, IApartmentService _apartmentService,
                           ICardService _cardService, IUserService _userService)
        {
            mapper = _mapper;
            apartmentService = _apartmentService;
            userService = _userService;
            cardService = _cardService;
        }

        public General<BillViewModel> GetPaidBills()
        {
            var result = new General<BillViewModel>();

            using (var context = new KemiksizContext())
            {
                var dataList = context.Bill.Where(x => x.IsPaid && !x.IsDeleted).OrderBy(a => a.Id).ThenBy(a => a.UserId)
                                        .ThenBy(a => a.ApartmentId).ThenBy(a => a.BillType);


                if (dataList.Any())
                {
                    result.List = mapper.Map<List<BillViewModel>>(dataList);
                    result.IsSuccess = true;
                    result.Message = "Fatura listeleme işlemi başarılı!";
                    result.Count = result.List.Count;
                }

                else
                {
                    result.ExceptionMessage = "Sistemde ödenmiş hiçbir fatura yok!";
                }

            }

            return result;
        }

        public General<BillViewModel> GetUnPaidBills()
        {
            var result = new General<BillViewModel>();

            using (var context = new KemiksizContext())
            {
                var dataList = context.Bill.Where(x => !x.IsPaid && !x.IsDeleted).OrderBy(a => a.Id).ThenBy(a => a.UserId)
                                        .ThenBy(a => a.ApartmentId).ThenBy(a => a.BillType);


                if (dataList.Any())
                {
                    result.List = mapper.Map<List<BillViewModel>>(dataList);
                    result.IsSuccess = true;
                    result.Message = "Fatura listeleme işlemi başarılı!";
                    result.Count = result.List.Count;
                }

                else
                {
                    result.ExceptionMessage = "Sistemde ödenmemiş hiçbir fatura yok!";
                }

            }

            return result;
        }

        public General<InsertBillViewModel> Insert(InsertBillViewModel newBill)
        {
            var result = new General<InsertBillViewModel>();

            using (var context = new KemiksizContext())
            {

                try
                {
                    var data = mapper.Map<Kemiksiz.DB.Entities.Bill>(newBill);
                    var isThere = context.Bill.Where(x => x.ApartmentId == newBill.ApartmentId &&
                                                          x.UserId == newBill.UserId &&
                                                          x.BillType == newBill.BillType &&
                                                          x.Month == newBill.Month);

                    if (isThere.Any())
                    {
                        result.ExceptionMessage = "Girdiğiniz bilgilerde zaten bir fatura mevcut, lütfen kontrol ediniz!";
                    }

                    else
                    {
                        context.Bill.Add(data);
                        context.SaveChanges();

                        result.Entity = mapper.Map<InsertBillViewModel>(data);
                        result.IsSuccess = true;
                        result.Message = "Fatura ekleme işlemi başarılı!";
                    }
                }
                catch (Exception)
                {

                    result.ExceptionMessage = "Beklenmeyen bir hata oluştu, lütfen daire ve user Id lerini kontrol edin!";
                }
                

            }

            return result;
        }

        public General<UpdateBillViewModel> Update(UpdateBillViewModel updatedBill)
        {
            var result = new General<UpdateBillViewModel>();

            using (var context = new KemiksizContext())
            {
                var permission = context.Bill.Any(x => x.Id == updatedBill.Id);
                var bill = context.Bill.SingleOrDefault(x => x.Id == updatedBill.Id);

                if (permission)
                {
                    bill.BillType = updatedBill.BillType;
                    bill.Price = updatedBill.Price;
                    bill.IsPaid = updatedBill.IsPaid;
                    

                    context.SaveChanges();

                    result.Entity = mapper.Map<UpdateBillViewModel>(updatedBill);
                    result.IsSuccess = true;
                    result.Message = "Fatura güncelleme işlemi başarılı!";

                }

                else
                {
                    result.ExceptionMessage = "Girilen Id de bir fatura yok, Lütfen kontrol edin!";
                }
            }

            return result;
        }

        public General<BillViewModel> Delete(int id)
        {
            var result = new General<BillViewModel>();

            using (var context = new KemiksizContext())
            {
                var data = context.Bill.SingleOrDefault(i => i.Id == id);

                if (data is not null)
                {
                    context.Bill.Remove(data);
                    context.SaveChanges();

                    result.Entity = mapper.Map<BillViewModel>(data);
                    result.Message = "Fatura silme işlemi başarılı!";
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Aranan fatura bulunamadı!";
                }

            }

            return result;
        }

        public General<BillViewModel> AssignBill(int price, string type, int month)
        {
            var result = new General<BillViewModel>();

                using (var context = new KemiksizContext())
                {

                    var userCount = apartmentService.GetApartments().Count;

                    var calculate = Extension.CalculatePrice(price, userCount);

                    if (calculate > 0 && userCount > 0)
                    {
                        var billList = context.Bill.Where(x => !x.IsDeleted && x.BillType == type && x.Month == month);

                        foreach (var item in billList)
                            item.Price = calculate;
                       
                        context.SaveChanges();

                    result.Message = type + " faturası atama işlemi başarılı!";
                    result.IsSuccess = true;
                    }

                    else
                    {
                        result.ExceptionMessage = "İşlem başarısız, lütfen değerleri kontrol edin!";
                    }

                }

            return result;

        }

        public General<BillViewModel> AssignDues(decimal price, string type)
        {
            var result = new General<BillViewModel>();

            using (var context = new KemiksizContext())
            {
                var apartmentList = apartmentService.GetApartments().List;
                var userCount = apartmentList.Count;

                

                // Aylık ödenecek olan toplam aidat miktarı
                price /=  12;

                // Kişi başı ödenecek aylık aidat miktarı
                var calculatePrice = Extension.CalculatePrice(price, userCount);

                foreach (var item in apartmentList)
                {

                    var billList = context.Bill.Where(x => x.ApartmentId == item.Id && x.BillType == "dues");

                    if(billList is not null)
                    {
                        foreach (var bill in billList)
                        {
                            if (item.ApartmentType == "3+1")
                            {
                                bill.Price = price;
                            }

                            else if (item.ApartmentType == "2+1")
                            {
                                bill.Price = price - 200;
                            }

                            else if (item.ApartmentType == "4+1")
                            {
                                bill.Price = price + 200;
                            }

                            else
                            {
                                result.ExceptionMessage = "Beklenmeyen bir hata oluştu!";
                            }
                        }
                        context.SaveChanges();

                        result.IsSuccess = true;
                        result.Message = "Aidat atama işlemi başarılı!";
                    }

                    else
                    {
                        result.ExceptionMessage = "Fatura bilgisi bulunamadı!";
                    }
                }
            }

            return result;
        }


        public General<BillViewModel> PayTotalBill(int id, string type)
        {
            var result = new General<BillViewModel>();

            var card = cardService.GetCardByUserId(id);

            using (var context = new KemiksizContext())
            {
                var billList = context.Bill.Where(x => x.UserId == id && x.BillType == type && !x.IsPaid);

                decimal totalUnPaid = 0;

                foreach (var item in billList)
                {
                    totalUnPaid += item.Price;
                

                    if(totalUnPaid == 0 && card.UserId != id)
                    {
                        result.ExceptionMessage = "Hiç borcunuz yok!";
                    }

                    else
                    {
                        card.PaidAmount += totalUnPaid;
                        item.IsPaid = true;
                        result.Message = "Toplu ödeme başarılı!";
                        result.IsSuccess = true;
                    }

                }

                context.SaveChanges();
                cardService.UpdateCard(card);

            }

            return result;
        }

        public General<BillViewModel> PayBill(int id, string type, int month)
        {
            var result = new General<BillViewModel>();

            var card = cardService.GetCardByUserId(id);

            using (var context = new KemiksizContext())
            {
                var bill = context.Bill.FirstOrDefault(x => x.UserId == id && x.BillType == type && x.Month == month);

                if(bill is not null && !bill.IsPaid && bill.UserId == card.UserId)
                {
                    card.PaidAmount += bill.Price;
                    bill.IsPaid = true;

                    result.IsSuccess = true;
                    result.Message = bill.BillType + " ödeme işleminiz başarılı!";

                    context.SaveChanges();
                    cardService.UpdateCard(card);
                }

                else
                {
                    result.ExceptionMessage = "Ödeme başarısız, lütfen bilgileri kontrol edin!";
                }

            }

            return result;
        }

    }
}
