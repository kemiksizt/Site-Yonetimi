using Kemiksiz.Model;
using Kemiksiz.Model.Bill;
using Kemiksiz.Service.Bill;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemiksiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService billService;

        public BillController(IBillService _billService)
        {
            billService = _billService;
        }

        [HttpGet("paid")]
        public General<BillViewModel> GetPaidBills()
        {
            return billService.GetPaidBills();
        }

        [HttpGet("unpaid")]
        public General<BillViewModel> GetUnPaidBills()
        {
            return billService.GetUnPaidBills();
        }

        [HttpPost]
        public General<InsertBillViewModel> Insert(InsertBillViewModel newBill)
        {
            return billService.Insert(newBill);
        }

        [HttpPut]
        public General<UpdateBillViewModel> Update(UpdateBillViewModel updatedBill)
        {
            return billService.Update(updatedBill);
        }

        [HttpDelete("{id}")]
        public General<BillViewModel> Delete(int id)
        {
            return billService.Delete(id);
        }

        [HttpPost("assignmentBills")]
        public General<BillViewModel> AssignBill(int price, string type, int month)
        {
            return billService.AssignBill(price, type, month);
        }

        [HttpPost("assignmentDues")]
        public General<BillViewModel> AssignDues(decimal price, string type)
        {
            return billService.AssignDues(price, type);
        }

        [HttpPost("paymentTotal")]
        public General<BillViewModel> PayTotalBill(int id, string type)
        {
            return billService.PayTotalBill(id, type);
        }

        [HttpPost("paymentBill")]
        public General<BillViewModel> PayBill(int id, string type, int month)
        {
            return billService.PayBill(id, type, month);
        }
    }
}
