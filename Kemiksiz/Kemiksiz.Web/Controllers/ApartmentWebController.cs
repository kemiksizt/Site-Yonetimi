using Kemiksiz.Model;
using Kemiksiz.Model.Apartment;
using Kemiksiz.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemiksiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentWebController : ControllerBase
    {
        private readonly IApartmentService apartmentService;
        public ApartmentWebController(IApartmentService _apartmentService)
        {
            apartmentService = _apartmentService;
        }

        [HttpGet("Apartment")]
        public IActionResult GetApartments()
        {
            var apartmentList = apartmentService.GetApartments();

            return Ok(apartmentList);
        }


        [HttpPost("InsertApartment")]
        public IActionResult InsertApartment(InsertApartmentViewModel newApart)
        {
            var apartment = apartmentService.Insert(newApart);

            return Ok(apartment);
        }


        [HttpDelete("DeleteApartment")]
        public IActionResult DeleteApartment(int id)
        {
            apartmentService.Delete(id);

            return Ok();
        }

        [HttpPut("UpdateApartment")]
        public IActionResult UpdateApartment(ApartmentViewModel updatedApart)
        {
            var apartment = apartmentService.Update(updatedApart);

            return Ok(apartment);
        }

    }
}
