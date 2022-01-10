﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model.User
{
    public class InsertUserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Tc { get; set; }
        public string Phone { get; set; }
        public int ApartmentId { get; set; }
        public string PlateNo { get; set; }
    }
}