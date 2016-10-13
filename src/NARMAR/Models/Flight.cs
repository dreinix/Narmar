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
        [BsonElement("origin")]
        public List<Airport> Origin { get; set; } = new List<Airport>();

        [Required]
        [BsonElement("destination")]
        public Airport Destination { get; set; } = new Airport();

        [Required]
        [BsonElement("date")]
        public DateTime Date { get; set; }

        [Required]
        [BsonElement("ariplane")]
        public Airplane Airplane { get; set; }
    }
}
