﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model.User
{
    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}