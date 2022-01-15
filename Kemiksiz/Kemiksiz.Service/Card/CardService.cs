using Kemiksiz.DB;
using Kemiksiz.Model;
using Kemiksiz.Model.Card;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.Card
{
    public class CardService : ICardService
    {

        private readonly IMongoCollection<CardViewModel> _cards;

        public CardService(IDbClient dbClient)
        {
            _cards = dbClient.GetCardsCollection();
        }

        public CardViewModel AddCard(CardViewModel card)
        {
            card.CardNumber = Extension.EncodeBase64(card.CardNumber);
            _cards.InsertOne(card);
            return card;
        }

        public General<CardViewModel> DeleteCard(string id)
        {
            var result = new General<CardViewModel>();
            try
            {
                _cards.DeleteOne(card => card.Id == id);
                result.IsSuccess = true;
                result.Message = "Kart silme işlemi başarılı!";

            }
            catch (Exception)
            {

                result.ExceptionMessage = "Kart numarasını kontrol edin!";
            }

            return result;
            
        }
        public CardViewModel GetCard(string id) => _cards.Find(card => card.Id == id).First();
        

        public List<CardViewModel> GetCards() => _cards.Find(card => true).ToList();

        public CardViewModel UpdateCard(CardViewModel card)
        {
            GetCard(card.Id);
            _cards.ReplaceOne(c => c.Id == card.Id, card);
            return card;
        }

        public CardViewModel GetCardByUserId(int id)
        {
            var cardList = GetCards();

            var card = _cards.Find(card => card.UserId == id).First();

            card.CardNumber = Extension.DecodeBase64(card.CardNumber);

            return card;

        }



    }
}
