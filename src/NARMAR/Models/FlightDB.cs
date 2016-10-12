using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Framework.OptionsModel;

namespace NARMAR.Models
{
        public interface IFlightRepository
        {
            IEnumerable<Flight> AllFlight();
            Flight GetById(ObjectId id);
            void Add(Flight Flight);
            void Update(ObjectId id, Flight Flight);
            bool Remove(ObjectId id);
        }
        public class FlightRepository : IFlightRepository
        {
            public readonly IMongoDatabase Database;

            public readonly dynamic ConnectionSettings = new
            {
                Host = "localhost",
                Port = 27017,
                Database = "narmar"
            };
            public FlightRepository()
            {
                Database = new MongoClient($"mongodb://{ConnectionSettings.Host}:{ConnectionSettings.Port}/").GetDatabase(ConnectionSettings.Database);
            }
            public IEnumerable<Flight> AllFlight()
            {
                return Database.GetCollection<Flight>("Flight").FindSync<Flight>(Builders<Flight>.Filter.Empty).ToEnumerable<Flight>();
            }
            public Flight GetById(ObjectId id)
            {
                var query = Builders<Flight>.Filter.Eq(x => x.Id, id);
                var opts = new FindOptions<Flight>();
                return Database.GetCollection<Flight>("Flight").FindSync<Flight>(query, opts).Single<Flight>();
            }
            public void Add(Flight flight)
            {
                Database.GetCollection<Flight>("Flight").InsertOne(flight);
            }
            public void Update(ObjectId id, Flight flight)
            {
                flight.Id = id;
                Database.GetCollection<Flight>("Flight").FindOneAndReplace<Flight>(
                    Builders<Flight>.Filter.Eq(e => e.Id, id),
                    flight
                );
            }
            public bool Exists(ObjectId id)
            {
                return Database.GetCollection<Flight>("Flight").FindSync(Builders<Flight>.Filter.Eq("_id", id)).ToList<Flight>().Count > 0;
            }
            public bool Remove(ObjectId id)
            {
                Database.GetCollection<Flight>("Flight").DeleteOne(Builders<Flight>.Filter.Eq(x => x.Id, id));
                return !Exists(id);
            }
        }
    }

