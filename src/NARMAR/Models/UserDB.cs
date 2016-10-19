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
        List<User> AllUsers();
        List<User> AllActive();
        List<User> AllInactive();
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
        public List<User> AllUsers()
        {
            return Database.GetCollection<User>("Users").FindSync<User>(Builders<User>.Filter.Empty).ToList<User>();
        }
        public List<User> AllActive()
        {
            return Database.GetCollection<User>("Users").FindSync<User>(Builders<User>.Filter.Eq(x => x.active,true)).ToList<User>();
        }
        public List<User> AllInactive()
        {
            return Database.GetCollection<User>("Users").FindSync<User>(Builders<User>.Filter.Eq(x => x.active, false)).ToList<User>();
        }
        public User GetById(ObjectId id)
        {
            var query = Builders<User>.Filter.Eq(x => x._id, id);
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
            user._id = id;
            Database.GetCollection<User>("Users").FindOneAndReplace<User>(
                Builders<User>.Filter.Eq(e => e._id, id),
                user
            );
        }
        public bool Remove(ObjectId id)
        {
            Database.GetCollection<User>("Users").DeleteOne(Builders<User>.Filter.Eq(x => x._id,id));
            return !Exists(id);
        }
    }
}
