using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model.Bill
{
    public class InsertBillViewModel
    {
        [Required(ErrorMessage = "Fatura tipi boş bırakılamaz.")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "Fatura tipi 8 karakterden fazla olamaz.")]
        public string BillType { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Ay bilgisi boş bırakılamaz.")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Kullanıcı Id si boş bırakılamaz.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Apartment Id si boş bırakılamaz.")]
        public int ApartmentId { get; set; }
    }
}
