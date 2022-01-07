using Kemiksiz.Model;
using Kemiksiz.Model.Apartment;
using Kemiksiz.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemiksiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService apartmentService;
        public ApartmentController(IApartmentService _apartmentService)
        {
            apartmentService = _apartmentService;
        }

        [HttpGet]
        public General<ApartmentViewModel> GetApartments()
        {
            return apartmentService.GetApartments();
        }

        
        
    }
}
