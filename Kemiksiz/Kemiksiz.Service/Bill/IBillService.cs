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
        public General<BillViewModel> GetPaidBillsByUserId(int id);
        public General<BillViewModel> GetUnPaidBillsByUserId(int id);
        public General<BillViewModel> GetPaidBills();
        public General<BillViewModel> GetUnPaidBills();
        public General<InsertBillViewModel> Insert(InsertBillViewModel newBill);
        public General<UpdateBillViewModel> Update(UpdateBillViewModel updatedBill);
        public General<BillViewModel> Delete(int id);
        public General<BillViewModel> AssignBill(int price, string type, int month);
        public General<BillViewModel> AssignDues(decimal price);
        public General<BillViewModel> PayTotalBill(int id, string type, string cardNumber);
        public General<BillViewModel> PayBill(int id, string type, int month, string cardNumber);
    }
}
