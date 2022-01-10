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
        public General<InsertBillViewModel> Insert(InsertBillViewModel newBill);
        public General<UpdateBillViewModel> Update(UpdateBillViewModel updatedBill);
    }
}
