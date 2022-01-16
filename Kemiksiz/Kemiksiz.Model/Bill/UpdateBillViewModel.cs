using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model.Bill
{
    public class UpdateBillViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Fatura tipi boş bırakılamaz.")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "Fatura tipi 8 karakterden fazla olamaz.")]
        public string BillType { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
    }
}
