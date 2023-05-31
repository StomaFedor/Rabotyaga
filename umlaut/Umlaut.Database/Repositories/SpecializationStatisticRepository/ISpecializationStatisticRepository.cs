using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Repositories.SpecializationStatisticRepository
{
    public interface ISpecializationStatisticRepository
    {
        Task UpdateStatisticsAsync();

        BsonDocument GetStatisticByName(string name);

        BsonArray GetFacultiesStatisticByName(string name);

        BsonArray GetExperienceSalaryByName(string name);
    }
}
