using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MongoDBSettings
    {
        public class MongoDbSettings
        {
            public string ConnectionURI { get; set; }
            public string DatabaseName { get; set; }
            public string CollectionName { get; set; }
        }
    }
}
