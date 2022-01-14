using Kemiksiz.Model.Card;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.DB
{
    public interface IDbClient
    {
        IMongoCollection<CardViewModel> GetCardsCollection();
    }
}
