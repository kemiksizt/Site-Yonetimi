using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }
        public string BlockName { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentNo { get; set; }
        public int ApartmentFloor { get; set; }
        public bool IsFull { get; set; }

    }
}
