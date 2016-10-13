using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Collections;



namespace NARMAR.Models
{
    public class Airport
    {
        [Required]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement("code")]
        public string Code { get; set; }

        [Required]
        [BsonElement("country")]
        public string Country;

        [Required]
        [BsonElement("city")]
        public string City;
    }
}
