using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Model.Card
{
    public class CardViewModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public int UserId { get; set; }
        public decimal PaidAmount { get; set; }

    }
}
