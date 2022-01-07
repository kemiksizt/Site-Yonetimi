﻿using Kemiksiz.Model;
using Kemiksiz.Model.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service
{
    public interface IApartmentService
    {
        public General<ApartmentViewModel> GetApartments();

        public General<InsertApartmentViewModel> Insert(InsertApartmentViewModel newApart);
    }
}
