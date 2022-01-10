using Kemiksiz.Model;
using Kemiksiz.Model.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.Bill
{
    public interface IBillService
    {
        public General<BillViewModel> GetBills();
    }
}
