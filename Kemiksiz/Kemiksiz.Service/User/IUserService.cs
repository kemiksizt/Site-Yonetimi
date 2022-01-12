using Kemiksiz.Model;
using Kemiksiz.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.User
{
    public interface IUserService
    {
        public General<UserViewModel> GetUsers();
        public General<UserViewModel> Delete(int id);
        public General<UserViewModel> Update(UserViewModel updatedUser);
        public General<InsertUserViewModel> Insert(InsertUserViewModel newUser);
        public General<LoginViewModel> Login(LoginViewModel loginUser);
        public General<LoginViewModelPasswordProtected> GetById(int id);
        public General<LoginViewModel> GetByName(LoginViewModel loginUser);
    }
}
