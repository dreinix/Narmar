using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Framework.OptionsModel;

namespace NARMAR.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> AllUsers();
        User GetById(ObjectId id);
        bool Exists(ObjectId id);
        void Add(User user);
        void Update(ObjectId id,User user);
        bool Remove(ObjectId id);
    }
    public class UserRepository : IUserRepository
    {
        public readonly IMongoDatabase Database;

        public readonly dynamic ConnectionSettings = new {
            Host = "localhost",
            Port = 27017,
            Database = "narmar"
        };
        public UserRepository()
        {
            Database = new MongoClient($"mongodb://{ConnectionSettings.Host}:{ConnectionSettings.Port}/").GetDatabase(ConnectionSettings.Database);
        }
        public IEnumerable<User> AllUsers()
        {
            return Database.GetCollection<User>("Users").FindSync<User>(Builders<User>.Filter.Empty).ToEnumerable<User>();
        }
        public User GetById(ObjectId id)
        {
            var query = Builders<User>.Filter.Eq("_id", id);
            var opts= new FindOptions<User>();
            return Database.GetCollection<User>("Users").FindSync<User>(query, opts).Single<User>();
        }
        public bool Exists(ObjectId id)
        {
            return Database.GetCollection<User>("Users").FindSync(Builders<User>.Filter.Eq("_id", id)).ToList<User>().Count > 0;
        }
        public void Add(User user)
        {
            Database.GetCollection<User>("Users").InsertOne(user);
        }
        public void Update(ObjectId id, User user)
        {
            user.Id = id;
            Database.GetCollection<User>("Users").FindOneAndReplace<User>(
                Builders<User>.Filter.Eq(e => e.Id, id),
                user
            );
        }
        public bool Remove(ObjectId id)
        {
            Database.GetCollection<User>("Users").DeleteOne(Builders<User>.Filter.Eq("_id",id));
            return !Exists(id);
        }
    }
}
