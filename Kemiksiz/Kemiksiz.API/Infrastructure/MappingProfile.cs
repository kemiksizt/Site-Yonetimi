﻿using AutoMapper;
using Kemiksiz.DB.Entities;
using Kemiksiz.Model;
using Kemiksiz.Model.Apartment;
using Kemiksiz.Model.User;

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

            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();

            CreateMap<InsertUserViewModel, User>();
            CreateMap<User, InsertUserViewModel>();
        }
    }
}
