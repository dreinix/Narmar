using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Collections;

namespace NARMAR.Models
{
    public class ListMinLength : ValidationAttribute{
        private int _length;
        public ListMinLength(int length) {
            _length = length;
        }
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count >= _length;
            }
            return false;
        }
    }
    public class ListMaxLength : ValidationAttribute
    {
        private int _length;
        public ListMaxLength(int length)
        {
            _length = length;
        }
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count <= _length;
            }
            return false;
        }
    }
    public class User
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        
        [Required]
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [Required]
        [BsonElement("lastName")]
        public string LastName { get; set; }

        [Required]
        [BsonElement("passport")]
        public string Passport { get; set; }

        [MaxLength(25)]
        [BsonElement("identityDocument")]
        public string IdentityDocument { get; set; }

        [Required]
        [BsonElement("country")]
        public string Country { get; set; }

        [Required]
        [BsonElement("city")]
        public string City{ get; set; }

        [Required]
        [BsonElement("addressLine1")]
        public string AddressLine1 { get; set; }

        [BsonElement("addressLine2")]
        public string AddressLine2 { get; set; }

        [Required]
        [BsonElement("postalCode")]
        public string PostalCode { get; set; }

        [Required]
        [BsonElement("birthDate")]
        public string BirthDate { get; set; }

        [Required]
        [BsonElement("gender")]
        public string Gender { get; set; }

        [Required]
        [BsonElement("civilStatus")]
        public string CivilStatus { get; set; }

        [Required]
        [BsonElement("contactMethods")]
        [ListMinLength(2)]
        [ListMaxLength(10)]
        public List<ContactMethod> ContactMethods { get; set; }

        [BsonElement("job")]
        public Job Job { get; set; }

        public User(){
            Job = new Job();
            ContactMethods = new List<ContactMethod>();
        }
    }
    public class ContactMethod
    {
        [Required]
        [BsonElement("type")]
        public string Type { get; set; }

        [Required]
        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("auxiliar")]
        public string Auxiliar { get; set; } 
    }
    public class Job {
        [BsonElement("company")]
        public string Company { get; set; }

        [BsonElement("department")]
        public string Department { get; set; }

        [BsonElement("position")]
        public string Position { get; set; }

        [BsonElement("salary")]
        public decimal Salary { get; set; }
    }
}
