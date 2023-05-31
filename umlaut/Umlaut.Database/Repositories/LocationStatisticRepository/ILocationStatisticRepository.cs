using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Repositories.LocationRepository;

namespace Umlaut.Database.Repositories.LocationStatisticRepository
{
    public interface ILocationStatisticRepository
    {
        Task UpdateStatisticsAsync();

        BsonDocument GetStatisticsByName(string name);
    }
}
