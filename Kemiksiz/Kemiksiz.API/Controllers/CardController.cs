using Kemiksiz.Model;
using Kemiksiz.Model.Card;
using Kemiksiz.Service.Card;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kemiksiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService cardService;

        public CardController(ICardService _cardService)
        {
            cardService = _cardService;
        }
        [HttpGet("getCard")]
        public List<CardViewModel> GetCards()
        {
            return cardService.GetCards();
        }

        [HttpGet("{id}", Name = "GetCard")]
        public CardViewModel GetCard(string id)
        {
            return cardService.GetCard(id);
        }

        [HttpPost("addCard")]
        public CardViewModel AddCard(CardViewModel card)
        {
            return cardService.AddCard(card);
        }

        [HttpDelete("deleteCard")]
        public General<CardViewModel> DeleteCard(string id)
        {
            return cardService.DeleteCard(id);
        }

        [HttpPut("updateCard")]
        public CardViewModel UpdateCard(CardViewModel card)
        {
            return cardService.UpdateCard(card);
        }


    }
}
