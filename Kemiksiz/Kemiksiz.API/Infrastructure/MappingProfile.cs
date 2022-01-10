using AutoMapper;
using Kemiksiz.DB.Entities;
using Kemiksiz.Model;
using Kemiksiz.Model.Apartment;
using Kemiksiz.Model.Bill;
using Kemiksiz.Model.User;

namespace Kemiksiz.API.Infrastructure
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //Apartment
            CreateMap<ApartmentViewModel, Apartment>();
            CreateMap<Apartment, ApartmentViewModel>();

            CreateMap<InsertApartmentViewModel, Apartment>();
            CreateMap<Apartment, InsertApartmentViewModel>();

            //User
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();

            CreateMap<InsertUserViewModel, User>();
            CreateMap<User, InsertUserViewModel>();

            CreateMap<User, LoginViewModel>();
            CreateMap<LoginViewModel, User>();

            //Bill
            CreateMap<BillViewModel, Bill>();
            CreateMap<Bill, BillViewModel>();

            CreateMap<InsertBillViewModel, Bill>();
            CreateMap<Bill, InsertBillViewModel>();

            CreateMap<UpdateBillViewModel, Bill>();
            CreateMap<Bill, UpdateBillViewModel>();
                
        }
    }
}
