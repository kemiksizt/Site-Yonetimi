using Kemiksiz.Model.Card;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.DB
{
    public class DbClient : IDbClient
    {

        private readonly IMongoCollection<CardViewModel> _cards;

        public DbClient(IOptions<CardDbConfig> cardDbConfig)
        {
            var client = new MongoClient(cardDbConfig.Value.Connection_String);
            var database = client.GetDatabase(cardDbConfig.Value.Database_Name);
            _cards = database.GetCollection<CardViewModel>(cardDbConfig.Value.Cards_Collection_Name);
        }

        public IMongoCollection<CardViewModel> GetCardsCollection() => _cards;
        
    }
}
