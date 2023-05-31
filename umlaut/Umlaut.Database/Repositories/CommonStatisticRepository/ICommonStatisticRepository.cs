using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.MongoModels;

namespace Umlaut.Database.Repositories.CommonStatisticRepository
{
    public interface ICommonStatisticRepository
    {
        Task UpdateStatisticsAsync();

        BsonDocument GetStatistics();

        BsonArray GetSpecializationsStatistics();

        BsonArray GetFacultiesStatistics();

        BsonArray GetLocationsStatistics();

    }
}
