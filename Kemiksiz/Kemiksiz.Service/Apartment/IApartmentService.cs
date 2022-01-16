using Kemiksiz.Model;
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
        public General<ApartmentViewModel> GetEmptyApartments();
        public General<ApartmentViewModel> GetAllApartments();
        public General<InsertApartmentViewModel> Insert(InsertApartmentViewModel newApart);
        public General<ApartmentViewModel> Delete(int id);
        public General<ApartmentViewModel> Update(ApartmentViewModel updatedApart);
    }
}
