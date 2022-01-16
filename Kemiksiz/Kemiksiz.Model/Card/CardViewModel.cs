using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Kredi kart no boş bırakılamaz.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Kredi kartı numarası 16 karakterden oluşmalı.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "CVV boş bırakılamaz.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV 3 karakter olmalı")]
        public int CVV { get; set; }

        [Required(ErrorMessage = "User Id boş bırakılamaz.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Çekilen tutar boş bırakılamaz. 0 giriniz.")]
        public decimal PaidAmount { get; set; }

    }
}
