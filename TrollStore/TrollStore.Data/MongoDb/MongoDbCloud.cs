namespace TrollStore.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using TrollStore.Model;

    public class MongoDbCloud
    {
        private const string DATABASE_NAME = "testmongolab";

        private const string CONNECTIONSTRING = @"mongodb://teamTroll:troll@ds033740.mongolab.com:33740/testmongolab";

        private readonly string databaseName;
        private readonly string connectionString;

        public MongoDbCloud()
            : this(CONNECTIONSTRING)
        {
        }

        public MongoDbCloud(string connectionString)
        {
            this.connectionString = connectionString;
            this.databaseName = DATABASE_NAME;
        }

        public void UploadToCollection(string collectionName, BsonDocument country)
        {
            var database = this.GetDatabase(this.databaseName);
            var collection = database.GetCollection<Country>(collectionName);

            collection.Insert(country);

            Console.WriteLine();
        }

        public IEnumerable<BsonDocument> GetItemsFromCollection(string collectionName)
        {
            var database = this.GetDatabase(this.databaseName);
            var collection = database.GetCollection(collectionName);

            return collection.FindAll();
        }

        private MongoDatabase GetDatabase(string databaseName)
        {
            var mongoClient = new MongoClient(this.connectionString);
            var mongoServer = mongoClient.GetServer();

            var database = mongoServer.GetDatabase(databaseName);
            return database;
        }
    }
}
