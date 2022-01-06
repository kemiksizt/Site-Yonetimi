﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Kemiksiz.DB.Entities
{
    public partial class Bill
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public decimal Price { get; set; }
        public DateTime Idate { get; set; }
        public DateTime? Udate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public int ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual User User { get; set; }
    }
}
