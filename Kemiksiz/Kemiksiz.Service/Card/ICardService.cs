using Kemiksiz.Model;
using Kemiksiz.Model.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.Card
{
    public interface ICardService
    {
        public List<CardViewModel> GetCards();
        CardViewModel AddCard(CardViewModel card);
        CardViewModel GetCard(string id);
        public General<CardViewModel> DeleteCard(string id);
        CardViewModel UpdateCard(CardViewModel card);
        
    }
}
