using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Blok adı 1 karakterden fazla olamaz.")]
        public string BlockName { get; set; }

        [Required(ErrorMessage = "Daire tipi boş bırakılamaz.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "2+1, 3+1 şeklinde giriniz.")]
        public string ApartmentType { get; set; }

        [Required(ErrorMessage = "Daire no boş bırakılamaz.")]
        public int ApartmentNo { get; set; }

        [Required(ErrorMessage = "Daire katı boş bırakılamaz.")]
        public int ApartmentFloor { get; set; }
        public bool IsFull { get; set; }

    }
}
