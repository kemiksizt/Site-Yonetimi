using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model.Bill
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
    }
}
