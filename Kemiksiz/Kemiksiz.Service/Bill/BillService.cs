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

        public General<BillViewModel> GetBills()
        {
            var result = new General<BillViewModel>();

            using (var context = new KemiksizContext())
            {
                var data = context.Bill.OrderBy(a => a.Id).ThenBy(a => a.UserId)
                                        .ThenBy(a => a.ApartmentId).ThenBy(a => a.BillType);


                if (data.Any())
                {
                    result.List = mapper.Map<List<BillViewModel>>(data);
                    result.IsSuccess = true;
                    result.Message = "Fatura listeleme işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Sistemde hiçbir fatura yok!";
                }

            }

            return result;
        }
    }
}
