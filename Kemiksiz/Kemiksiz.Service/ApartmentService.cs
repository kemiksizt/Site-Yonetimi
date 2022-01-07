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
                var data = context.Apartment.OrderBy(a => a.Id).ThenBy(a => a.IsFull);
            

                if(data.Any())
                {
                    result.List = mapper.Map<List<ApartmentViewModel>>(data);
                    result.IsSuccess = true;
                    result.Message = "Daire listeleme işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Sistemde hiçbir daire yok!";
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
                var IsThere = context.Apartment.Where(x => x.BlockName == newApart.BlockName &&
                                                           x.ApartmentType == newApart.ApartmentType &&
                                                           x.ApartmentNo == newApart.ApartmentNo &&
                                                           x.ApartmentFloor == newApart.ApartmentFloor);

                if (IsThere.Any())
                {
                    result.ExceptionMessage = "Girdiğiniz bilgilerde zaten bir daire mevcut, lütfen kontrol ediniz!";
                }

                else
                {
                    context.Apartment.Add(data);
                    context.SaveChanges();

                    result.Entity = mapper.Map<InsertApartmentViewModel>(data);
                    result.IsSuccess = true;
                    result.Message = "Ekleme işlemi başarılı!";
                }
 
            }

            return result;
        }

    }
}
