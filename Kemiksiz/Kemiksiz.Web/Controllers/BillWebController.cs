using Kemiksiz.Model.Bill;
using Kemiksiz.Service.Bill;
using Kemiksiz.Service.Card;
using Kemiksiz.Service.Jwt;
using Kemiksiz.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemiksiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillWebController : ControllerBase
    {

        private readonly IBillService billService;

        public BillWebController(IBillService _billService)
        {

            billService = _billService;
        }

        [HttpGet("paidBill")]
        public IActionResult GetPaidBills()
        {
            var billList = billService.GetPaidBills();

            return Ok(billList);
        }


        [HttpGet("unpaidBill")]
        public IActionResult GetUnPaidBills()
        {
            var billList = billService.GetUnPaidBills();

            return Ok(billList);
        }



        [HttpPost("InsertBill")]
        public IActionResult InsertBill(InsertBillViewModel newBill)
        {
            var bill = billService.Insert(newBill);

            return Ok(bill);
        }


        [HttpPost("UpdateBill")]
        public IActionResult UpdateBill(UpdateBillViewModel updatedBill)
        {
            var bill = billService.Update(updatedBill);

            return Ok(bill);
        }


        [HttpDelete("DeleteBill")]
        public IActionResult DeleteBill(int id)
        {
            billService.Delete(id);

            return Ok();
        }


        [HttpPost("assignmentBills")]
        public IActionResult AssignBill(int price, string type, int month)
        {
            var bill = billService.AssignBill(price, type, month);

            return Ok(bill);
        }


        [HttpPost("assignmentDue")]
        public IActionResult AssignDues(decimal price, string type)
        {
            var due = billService.AssignDues(price, type);

            return Ok(due);
        }


        [HttpPost("paymentTotal")]
        public IActionResult PayTotalBill(int id, string type, long cardNumber)
        {
            var payment = billService.PayTotalBill(id, type, cardNumber);

            return Ok(payment);
        }


        [HttpPost("paymentBill")]
        public IActionResult PayBill(int id, string type, int month, long cardNumber)
        {
            var payment = billService.PayBill(id, type, month, cardNumber);

            return Ok(payment);
        }
    }
}
