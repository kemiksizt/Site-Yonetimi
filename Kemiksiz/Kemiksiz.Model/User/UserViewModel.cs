using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username 3 karakterden az olamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadı alanı boş bırakılamaz!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Soyadı 3 karakterden az olamaz.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username 3 karakterden az olamaz.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-mail adresi!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Şifre 3 karakterden az olamaz.")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}
