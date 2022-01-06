using AutoMapper;
using Kemiksiz.DB.Entities;
using Kemiksiz.Model;

namespace Kemiksiz.API.Infrastructure
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<ApartmentViewModel, Apartment>();
            CreateMap<Apartment, ApartmentViewModel>();  
        }
    }
}
