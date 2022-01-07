using AutoMapper;
using Kemiksiz.DB.Entities;
using Kemiksiz.Model;
using Kemiksiz.Model.Apartment;

namespace Kemiksiz.API.Infrastructure
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<ApartmentViewModel, Apartment>();
            CreateMap<Apartment, ApartmentViewModel>();

            CreateMap<InsertApartmentViewModel, Apartment>();
            CreateMap<Apartment, InsertApartmentViewModel>();
        }
    }
}
