using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model.User
{
    public class InsertUserViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username 3 karakterden az olamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Şifre 3 karakterden az olamaz.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username 3 karakterden az olamaz.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-mail adresi!")]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "Tc boş bırakılamaz.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Tc 11 karakter olmalı.")]
        public string Tc { get; set; }

        [Required(ErrorMessage = "Telefon boş bırakılamaz.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telefon 10 karakter olmalı.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Apartman no boş bırakılamaz.")]
        public int ApartmentId { get; set; }
        public string PlateNo { get; set; }
    }
}
