using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.MongoModels;
using Umlaut.Database.Repositories.GraduateRepository;
using Umlaut.Database.Repositories.LocationRepository;

namespace Umlaut.Database.Repositories.LocationStatisticRepository
{
    public class LocationStatisticRepository : MongoBaseRepository, ILocationStatisticRepository
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IGraduateRepository _graduateRepository;

        public LocationStatisticRepository(IMongoDatabase db, ILocationRepository locationRepository, IGraduateRepository graduateRepository) : base(db) 
        { 
            _locationRepository = locationRepository;
            _graduateRepository = graduateRepository;
        }

        public BsonDocument GetStatisticsByName(string name)
        {
            var collection = _db.GetCollection<BsonDocument>("LocationStatistics");
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            return collection.Find(new BsonDocument { { "Name", name } }).Project(projection).First();
        }

        public async Task UpdateStatisticsAsync()
        {
            var collection = _db.GetCollection<LocationStatistic>("LocationStatistics");
            var allGraduatesCount = _graduateRepository.GetGraduatesList().Count();
            foreach (var location in _locationRepository.GetLocationsGraduatesIncludedList())
            {
                var statistic = new LocationStatistic();
                var graduates = location.Graduates;
                statistic.Name = location.Name;
                if(graduates.Any(item => item.ExpectedSalary != 0))
                    statistic.AverageSalary = (int)graduates.Where(item => item.ExpectedSalary != 0).Average(item => item.ExpectedSalary);
                else
                    statistic.AverageSalary = 0;
                statistic.Percent = Math.Round((double)graduates.Count() / allGraduatesCount * 100, 3);
                statistic.ResumeCount = graduates.Count();
                await collection.ReplaceOneAsync(Builders<LocationStatistic>.Filter.Eq(item => item.Name, location.Name), statistic, new ReplaceOptions { IsUpsert = true });
            }
        }
    }
}
