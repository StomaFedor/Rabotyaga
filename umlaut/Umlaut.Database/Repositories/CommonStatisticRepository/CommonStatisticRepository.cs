using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.MongoModels;
using Umlaut.Database.Repositories.CommonStatisticRepository;
using Umlaut.Database.Repositories.FacultyRepository;
using Umlaut.Database.Repositories.GraduateRepository;
using Umlaut.Database.Models.PostgresModels;
using Umlaut.Database.Repositories.LocationRepository;
using Umlaut.Database.Repositories.SpecializationRepository;

namespace Umlaut.Database.Repositories.MongoStatisticRepository
{
    public class CommonStatisticRepository : MongoBaseRepository, ICommonStatisticRepository
    {
        private readonly IGraduateRepository _graduateRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ISpecializationRepository _specializationRepository;

        public CommonStatisticRepository(IMongoDatabase client, IGraduateRepository repository, IFacultyRepository facultyRepository,
                                         ISpecializationRepository specializationRepository, ILocationRepository locationRepository) : base(client) 
        {
            _graduateRepository = repository;
            _facultyRepository = facultyRepository;
            _locationRepository = locationRepository;
            _specializationRepository = specializationRepository;
        }

        public async Task UpdateStatisticsAsync()
        {
            CommonStatistic statistic = new CommonStatistic();
            var graduates = _graduateRepository.GetGraduatesList();
            statistic.ResumeCount = graduates.Count();
            statistic.AverageSalary = (int)graduates.Where(item => item.ExpectedSalary != 0).Average(item => item.ExpectedSalary);
            statistic.StartYearAverage = (int)graduates.Average(item => item.Age - item.Experience);
            statistic.GraduationYearAverage = (int)graduates.Average(item => item.YearGraduation);
            statistic.FacultyList = CountFacultiesStat();
            statistic.SpecializationList = CountSpecializatiosStat();
            statistic.LocationList = CountLocationsStat();
            var collection = _db.GetCollection<CommonStatistic>("CommonStatistic");
            await collection.ReplaceOneAsync(Builders<CommonStatistic>.Filter.Empty, statistic, new ReplaceOptions { IsUpsert = true });
        }

        private List<NameCountPair> CountSpecializatiosStat()
        {
            return _specializationRepository.GetSpecializationsGraduatesIncludedList()
                                                             .Where(item => item.Graduates.Count() >= 0)
                                                             .Select(item => new NameCountPair() { Name = item.Name, Count = item.Graduates.Count() })
                                                             .OrderByDescending(item => item.Count)
                                                             .ToList();
        }

        private List<NameCountPair> CountFacultiesStat()
        {
            return _facultyRepository.GetFacultiesGraduatesIncludedList()
                                                             .Where(item => item.Graduates.Count() >= 0)
                                                             .Select(item => new NameCountPair() { Name = item.Name, Count = item.Graduates.Count() })
                                                             .OrderByDescending(item => item.Count)
                                                             .ToList();
        }

        private List<NameCountPair> CountLocationsStat()
        {
            return _locationRepository.GetLocationsGraduatesIncludedList()
                                                              .Where(item => item.Graduates.Count() >= 0)
                                                              .Select(item => new NameCountPair() { Name = item.Name, Count = item.Graduates.Count() })
                                                              .OrderByDescending(item => item.Count)
                                                              .ToList();
        }

        public BsonDocument GetStatistics()
        {
            var collection = _db.GetCollection<BsonDocument>("CommonStatistic");
            var projection = Builders<BsonDocument>.Projection.Exclude("FacultyList").Exclude("LocationList")
                                                              .Exclude("SpecializationList").Exclude("_id");
            return collection.Find("{ }").Project(projection).FirstOrDefault();
        }

        public BsonArray GetSpecializationsStatistics()
        {
            var collection = _db.GetCollection<BsonDocument>("CommonStatistic");
            var projection = Builders<BsonDocument>.Projection.Include("SpecializationList").Exclude("_id");
            return collection.Find("{ }").Project(projection).FirstOrDefault().GetValue("SpecializationList").AsBsonArray;
        }

        public BsonArray GetFacultiesStatistics()
        {
            var collection = _db.GetCollection<BsonDocument>("CommonStatistic");
            var projection = Builders<BsonDocument>.Projection.Include("FacultyList").Exclude("_id");
            return collection.Find("{ }").Project(projection).FirstOrDefault().GetValue("FacultyList").AsBsonArray;
        }

        public BsonArray GetLocationsStatistics()
        {
            var collection = _db.GetCollection<BsonDocument>("CommonStatistic");
            var projection = Builders<BsonDocument>.Projection.Include("LocationList").Exclude("_id");
            return collection.Find("{ }").Project(projection).FirstOrDefault().GetValue("LocationList").AsBsonArray;
        }
    }
}
