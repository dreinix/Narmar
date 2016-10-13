using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Collections;


namespace NARMAR.Models
{
    public class Airplane
    {
        [Required]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement("model")]
        public string Model { get; set; }

        [Required]
        [BsonElement("capacity")]
        public int Capacity { get; set; }
    }
}
