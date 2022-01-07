﻿using AutoMapper;
using Kemiksiz.DB.Entities.DataContext;
using Kemiksiz.Model;
using Kemiksiz.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.User
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;

        public UserService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public General<UserViewModel> GetUsers()
        {
            var result = new General<UserViewModel>();

            using (var context = new KemiksizContext())
            {
                var data = context.User.Where(x => x.IsActive && !x.IsDelete).OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<UserViewModel>>(data);
                    result.IsSuccess = true;
                    result.Message = "Kullanıcı listeleme işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Hiçbir kullanıcı kayıtlı değil!";
                }
            }

            return result;
        }

        public General<UserViewModel> Delete(int id)
        {
            var result = new General<UserViewModel>();

            using (var context = new KemiksizContext())
            {
                var data = context.User.SingleOrDefault(i => i.Id == id);

                if (data is not null)
                {
                    context.User.Remove(data);
                    context.SaveChanges();

                    result.Entity = mapper.Map<UserViewModel>(data);
                    result.Message = "Kullanıcı silme işlemi başarılı!";
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Aranan kullanıcı bulunamadı!";
                }

            }

            return result;
        }


    }
}
