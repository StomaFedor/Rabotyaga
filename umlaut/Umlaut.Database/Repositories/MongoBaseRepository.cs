using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Repositories
{
    public class MongoBaseRepository
    {
        protected readonly IMongoDatabase _db;

        public MongoBaseRepository(IMongoDatabase db)
        {
            _db = db;
        }
    }
}
