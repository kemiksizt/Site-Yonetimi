using AutoMapper;
using Kemiksiz.DB.Entities.DataContext;
using Kemiksiz.Model;
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
        public General<ApartmentViewModel> GetFullApartments()
        {
            var result = new General<ApartmentViewModel>();

            using (var context = new KemiksizContext())
            {
                var data = context.Apartment.Where(a => a.IsFull).OrderBy(a => a.Id);
            

                if(data.Any())
                {
                    result.List = mapper.Map<List<ApartmentViewModel>>(data);
                    result.IsSuccess = true;
                    result.Message = "Dolu daire listeleme işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Aktif hiçbir daire yok!";
                }

            }

            return result;
        }


        public General<ApartmentViewModel> GetEmptyApartments()
        {
            var result = new General<ApartmentViewModel>();

            using (var context = new KemiksizContext())
            {
                var data = context.Apartment.Where(a => !a.IsFull).OrderBy(a => a.Id);


                if (data.Any())
                {
                    result.List = mapper.Map<List<ApartmentViewModel>>(data);
                    result.IsSuccess = true;
                    result.Message = "Boş daire listeleme işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Tüm daireler dolu!";
                }

            }

            return result;
        }
    }
}
