using AutoMapper;
using Kemiksiz.DB.Entities.DataContext;
using Kemiksiz.Model;
using Kemiksiz.Model.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service
{
    public class ApartmentService : IApartmentService
    {
        private readonly IMapper mapper;

        public ApartmentService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public General<ApartmentViewModel> GetApartments()
        {
            var result = new General<ApartmentViewModel>();

            using (var context = new KemiksizContext())
            {
                var dataList = context.Apartment.Where(x => x.IsFull).OrderBy(a => a.Id);
            

                if(dataList.Any())
                {
                    result.List = mapper.Map<List<ApartmentViewModel>>(dataList);
                    result.IsSuccess = true;
                    result.Message = "Daire listeleme işlemi başarılı!";
                    result.Count = result.List.Count;
                }

                else
                {
                    result.ExceptionMessage = "Sistemde dolu hiçbir daire yok!";
                }

            }

            return result;
        }

        public General<ApartmentViewModel> GetEmptyApartments()
        {
            var result = new General<ApartmentViewModel>();

            using (var context = new KemiksizContext())
            {
                var dataList = context.Apartment.Where(x => !x.IsFull).OrderBy(a => a.Id);


                if (dataList.Any())
                {
                    result.List = mapper.Map<List<ApartmentViewModel>>(dataList);
                    result.IsSuccess = true;
                    result.Message = "Daire listeleme işlemi başarılı!";
                    result.Count = result.List.Count;
                }

                else
                {
                    result.ExceptionMessage = "Sistemde dolu hiçbir daire yok!";
                }

            }

            return result;
        }

        public General<InsertApartmentViewModel> Insert(InsertApartmentViewModel newApart)
        {
            var result = new General<InsertApartmentViewModel>();  

            using (var context = new KemiksizContext())
            {
                var data = mapper.Map<Kemiksiz.DB.Entities.Apartment>(newApart);
                var isThere = context.Apartment.Where(x => x.BlockName == newApart.BlockName &&
                                                           x.ApartmentType == newApart.ApartmentType &&
                                                           x.ApartmentNo == newApart.ApartmentNo &&
                                                           x.ApartmentFloor == newApart.ApartmentFloor);

                if (isThere.Any())
                {
                    result.ExceptionMessage = "Girdiğiniz bilgilerde zaten bir daire mevcut, lütfen kontrol ediniz!";
                }

                else
                {
                    context.Apartment.Add(data);
                    context.SaveChanges();

                    result.Entity = mapper.Map<InsertApartmentViewModel>(data);
                    result.IsSuccess = true;
                    result.Message = "Daire ekleme işlemi başarılı!";
                }
 
            }

            return result;
        }

        public General<ApartmentViewModel> Delete(int id)
        {
            var result = new General<ApartmentViewModel>();

            using (var context = new KemiksizContext())
            {
                var data = context.Apartment.SingleOrDefault(x => x.Id == id);

                if(data is not null)
                {
                    context.Apartment.Remove(data);
                    context.SaveChanges();

                    result.Entity = mapper.Map<ApartmentViewModel>(data);
                    result.IsSuccess = true;
                    result.Message = "Daire silme işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Aranan daire bulunamadı!";
                }

            }

            return result;
        }


        public General<ApartmentViewModel> Update(ApartmentViewModel updatedApart)
        {
            var result = new General<ApartmentViewModel>();

            using (var context = new KemiksizContext())
            {
                var permission = context.Apartment.Any(x => x.Id == updatedApart.Id);
                var apart = context.Apartment.SingleOrDefault(x => x.Id == updatedApart.Id);

                if (permission)
                {
                    apart.BlockName = updatedApart.BlockName;
                    apart.ApartmentType = updatedApart.ApartmentType;
                    apart.ApartmentNo = updatedApart.ApartmentNo;
                    apart.ApartmentFloor = updatedApart.ApartmentFloor;
                    apart.IsFull = updatedApart.IsFull;

                    context.SaveChanges();

                    result.Entity = mapper.Map<ApartmentViewModel>(updatedApart);
                    result.IsSuccess = true;
                    result.Message = "Daire güncelleme işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Belirtilen Id de daire yok. Lütfen kontrol ediniz!";
                }

                return result;

            }

        }
    }
}
