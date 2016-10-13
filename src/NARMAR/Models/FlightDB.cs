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
        IEnumerable<Flight> AllFlights();
        Flight GetById(ObjectId id);
        List<Flight> GetByCapacity(int capacity);
        bool Exists(ObjectId id);
        void Add(Flight flight);
        void Update(ObjectId id, Flight flight);
        bool Remove(ObjectId id);

    }
    public class FlightRepository : IFlightRepository
    {
        public readonly IMongoDatabase Database;

        public readonly dynamic ConnectionSettings = new {
            Host = "localhost",
            Port = 27017,
            Database = "narmar"
        };
        public FlightRepository()
        {
            Database = new MongoClient($"mongodb://{ConnectionSettings.Host}:{ConnectionSettings.Port}/").GetDatabase(ConnectionSettings.Database);
        }
        public IEnumerable<Flight> AllFlights()
        {
            return Database.GetCollection<Flight>("Flights").FindSync<Flight>(Builders<Flight>.Filter.Empty).ToEnumerable<Flight>();
        }
        public Flight GetById(ObjectId id)
        {
            var query = Builders<Flight>.Filter.Eq(x => x.Id, id);
            var opts= new FindOptions<Flight>();
            return Database.GetCollection<Flight>("Flights").FindSync<Flight>(query, opts).Single<Flight>();
        }
        public List<Flight> GetByCapacity(int capacity)
        {
            var query = Builders<Flight>.Filter.AnyGte("ariplane.capacity", capacity);
            var opts = new FindOptions<Flight>();
            return Database.GetCollection<Flight>("Flights").FindSync<Flight>(query, opts).ToList<Flight>();
        }
        public bool Exists(ObjectId id)
        {
            return Database.GetCollection<Flight>("Flights").FindSync(Builders<Flight>.Filter.Eq("_id", id)).ToList<Flight>().Count > 0;
        }
        public void Add(Flight flight)
        {
            Database.GetCollection<Flight>("Flights").InsertOne(flight);
        }
        public void Update(ObjectId id, Flight flight)
        {
            flight.Id = id;
            Database.GetCollection<Flight>("Flights").FindOneAndReplace<Flight>(
                Builders<Flight>.Filter.Eq(e => e.Id, id),
                flight
            );
        }
        public bool Remove(ObjectId id)
        {
            Database.GetCollection<Flight>("Flights").DeleteOne(Builders<Flight>.Filter.Eq(x => x.Id,id));
            return !Exists(id);
        }
    }
}
