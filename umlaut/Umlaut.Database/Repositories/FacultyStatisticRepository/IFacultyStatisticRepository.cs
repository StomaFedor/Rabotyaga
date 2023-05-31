using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Repositories.FacultyStatisticRepository
{
    public interface IFacultyStatisticRepository
    {
        Task UpdateStatisticsAsync();

        BsonDocument GetStatisticByName(string name);

        BsonArray GetExperienceSalaryByName(string name);

        BsonArray GetGraduationYearsByName(string name);

        BsonArray GetSpecializationsByName(string name);
    }
}
