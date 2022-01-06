using Kemiksiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service
{
    public interface IApartmentService
    {
        public General<ApartmanetViewModel> GetApartments();
    }
}
