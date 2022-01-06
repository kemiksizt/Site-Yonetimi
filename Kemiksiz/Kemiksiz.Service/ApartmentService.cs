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

    }
}
