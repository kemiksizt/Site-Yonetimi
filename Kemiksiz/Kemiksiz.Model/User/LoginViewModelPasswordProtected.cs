using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kemiksiz.Model.User
{
    public class LoginViewModelPasswordProtected
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore] public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
