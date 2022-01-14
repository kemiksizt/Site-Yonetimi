using AutoMapper;
using Kemiksiz.DB.Entities.DataContext;
using Kemiksiz.Model;
using Kemiksiz.Model.Bill;
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

        public BillService(IMapper _mapper)
        {
            mapper = _mapper;
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
    }
}
