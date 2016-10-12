using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Collections;
namespace NARMAR.Models
{
    public class Flight
    {   
        [Required]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement("originCity")]
        public string OriginCity { get; set; }

        [Required]
        [BsonElement("destinationCity")]
        public string DestinationCity { get; set; }

        [Required]
        [BsonElement("originCountry")]
        public string OriginCountry { get; set; }

        [Required]
        [BsonElement("destinationCountry")]
        public string DestinationCountry { get; set; }

        [Required]
        [BsonElement("scale")]
        public string Scale { get; set; }

        [Required]
        [BsonElement("depHour")]
        public string DepHour { get; set; }

        [Required]
        [BsonElement("arHour")]
        public string ArHour { get; set; }

        [Required]
        [BsonElement("avSite")]
        public int AvSite { get; set; }

        [Required]
        [BsonElement("aPrice")]
        public  decimal APrice { get; set; }

        [Required]
        [BsonElement("tPrice")]
        public decimal TPrice { get; set; }

        [Required]
        [BsonElement("kPrice")]
        public decimal KPrice { get; set; }
    }
}
