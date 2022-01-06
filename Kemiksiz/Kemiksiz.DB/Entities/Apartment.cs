using System;
using System.Collections.Generic;

#nullable disable

namespace Kemiksiz.DB.Entities
{
    public partial class Apartment
    {
        public Apartment()
        {
            Bill = new HashSet<Bill>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string BlockName { get; set; }
        public string ApartmentType { get; set; }
        public int ApartmentNo { get; set; }
        public int ApartmentFloor { get; set; }
        public bool IsFull { get; set; }

        public virtual ICollection<Bill> Bill { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
