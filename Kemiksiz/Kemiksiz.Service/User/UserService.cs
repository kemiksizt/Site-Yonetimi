using AutoMapper;
using Kemiksiz.DB.Entities.DataContext;
using Kemiksiz.Model;
using Kemiksiz.Model.User;
using Kemiksiz.Service.Jwt;
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
        private readonly IJwtService jwtService;

        public UserService(IMapper _mapper, IJwtService _jwtService)
        {
            mapper = _mapper;
            jwtService = _jwtService;
        }

        string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
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


        public General<UserViewModel> Update(UserViewModel updatedUser)
        {
            var result = new General<UserViewModel>();

            using (var context = new KemiksizContext())
            {
                var permission = context.User.Any(x => x.Id == updatedUser.Id);
                var user = context.User.SingleOrDefault(x => x.Id == updatedUser.Id);

                if (permission)
                {
                    user.Name = updatedUser.Name;
                    user.Surname = updatedUser.Surname;
                    user.Email = updatedUser.Email;
                    user.Password = updatedUser.Password;
                    user.IsAdmin = updatedUser.IsAdmin;

                    context.SaveChanges();

                    result.Entity = mapper.Map<UserViewModel>(updatedUser);
                    result.IsSuccess = true;
                    result.Message = "Kullanıcı güncelleme işlemi başarılı!";

                }

                else
                {
                    result.ExceptionMessage = "Girilen Id de bir kullanıcı yok, Lütfen kontrol edin!";
                }
            }

            return result;
        }

        public General<InsertUserViewModel> Insert(InsertUserViewModel newUser)
        {
            var result = new General<InsertUserViewModel>();


            using (var context = new KemiksizContext())
            {

                try
                {
                    var data = mapper.Map<Kemiksiz.DB.Entities.User>(newUser);
                    var isThere = context.User.Where(x => x.ApartmentId == newUser.ApartmentId);

                    if (isThere.Any())
                    {
                        result.ExceptionMessage = "Girdiğiniz bilgilerdeki daire zaten dolu, lütfen kontrol ediniz!";
                    }

                    else
                    {
                        data.Password = CreatePassword(8);
                        data.Idate = DateTime.Now;
                        context.User.Add(data);
                        context.SaveChanges();

                        result.Entity = mapper.Map<InsertUserViewModel>(data);
                        result.IsSuccess = true;
                        result.Message = "Kullanıcı ekleme işlemi başarılı!";
                    }
                }
                catch (Exception)
                {

                    result.ExceptionMessage = "Beklenmeyen bir hata oluştu, lütfen daire Id sini kontrol edin!";
                }
               
            }

            return result;
        }

        public General<LoginViewModel> Login(LoginViewModel loginUser)
        {
            General<LoginViewModel> result = new();

            using (var context = new KemiksizContext())
            {
                var data = context.User.FirstOrDefault(x => x.IsActive && !x.IsDelete &&
                                            x.Name == loginUser.Name &&
                                            x.Password == loginUser.Password);

                if (data is not null)
                {
                    loginUser.IsAdmin = data.IsAdmin;

                    result.IsSuccess = true;
                    result.Entity = mapper.Map<LoginViewModel>(data);
                    result.Message = "Giriş işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Kullanıcı adı veya şifre yanlış, tekrar deneyin!";
                }

                return result;
            }
        }

        public General<LoginViewModel> GetById(int id)
        {
            var result = new General<LoginViewModel>();

            using (var context = new KemiksizContext())
            {

                var data = context.User.SingleOrDefault(x => x.Id == id && x.IsActive && !x.IsDelete);


                if (data is not null)
                {
                    result.Entity = mapper.Map<LoginViewModel>(data);
                    result.IsSuccess = true;
                    result.Message = "Kullanıcı getirme işlemi başarılı!";
                }
                else
                {
                    result.ExceptionMessage = "Herhangi bir kullanıcı bulunamadı.";
                }
            }

            return result;
        }


        public General<LoginViewModel> GetByName(LoginViewModel loginUser)
        {
            General<LoginViewModel> result = new();

            using (var context = new KemiksizContext())
            {
                var data = context.User.FirstOrDefault(x => x.IsActive && !x.IsDelete && x.Name == loginUser.Name);

                if (data is not null)
                {
                    loginUser.IsAdmin = data.IsAdmin;
                    loginUser.Password = data.Password;

                    result.IsSuccess = true;
                    result.Entity = mapper.Map<LoginViewModel>(data);
                    result.Message = "kullanıcı getirme işlemi başarılı!";
                }

                else
                {
                    result.ExceptionMessage = "Kullanıcı adı yanlış, lütfen tekrar deneyin!";
                }

                return result;
            }


        }


    }
}
