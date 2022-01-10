﻿using Kemiksiz.Model;
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

        [HttpGet]
        public General<BillViewModel> GetUsers()
        {
            return billService.GetBills();
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
    }
}
