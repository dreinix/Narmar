using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Collections;
using NARMAR;
using static NARMAR.ValidationAttributes;

namespace NARMAR.Models
{
    public class User
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }

        [BsonElement("username")]
        public string username { get; set; } = "";

        [BsonElement("password")]
        public string password { get; set; } = "";

        [BsonElement("active")]
        public bool active { get; set; } = false;

        [Required]
        [BsonElement("firstName")]
        public string firstName { get; set; }

        [Required]
        [BsonElement("lastName")]
        public string lastName { get; set; }

        [Required]
        [BsonElement("passport")]
        public string passport { get; set; }

        [MaxLength(25)]
        [BsonElement("identityDocument")]
        public string identityDocument { get; set; }

        [Required]
        [BsonElement("country")]
        public string country { get; set; }

        [Required]
        [BsonElement("city")]
        public string city{ get; set; }

        [Required]
        [BsonElement("addressLine1")]
        public string addressLine1 { get; set; }

        [BsonElement("addressLine2")]
        public string addressLine2 { get; set; }

        [Required]
        [BsonElement("postalCode")]
        public string postalCode { get; set; }

        [Required]
        [BsonElement("birthDate")]
        public string birthDate { get; set; }

        [Required]
        [BsonElement("gender")]
        public string gender { get; set; }

        [Required]
        [BsonElement("civilStatus")]
        public string civilStatus { get; set; }

        [Required]
        [BsonElement("contactMethods")]
        [ListMinLength(2)]
        [ListMaxLength(10)]
        public List<ContactMethod> contactMethods { get; set; } = new List<ContactMethod>();

        [BsonElement("job")]
        public Job job { get; set; } = new Job();

    }
    public class ContactMethod
    {
        [Required]
        [BsonElement("type")]
        public string type { get; set; }

        [Required]
        [BsonElement("content")]
        public string content { get; set; }

        [BsonElement("auxiliar")]
        public string auxiliar { get; set; } 
    }
    public class Job {
        [BsonElement("company")]
        public string company { get; set; }

        [BsonElement("department")]
        public string department { get; set; }

        [BsonElement("position")]
        public string position { get; set; }

        [BsonElement("salary")]
        public decimal salary { get; set; }
    }
}
