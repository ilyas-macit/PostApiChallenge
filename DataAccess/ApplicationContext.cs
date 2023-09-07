using DataAccess.Models;
using Entity.Concrete;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationContext
    {
        public IMongoCollection<Post> Posts;

        public ApplicationContext(IOptions<MongoDBSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            Posts = database.GetCollection<Post>(mongoDbSettings.Value.CollectionName);
        }
    }
}
