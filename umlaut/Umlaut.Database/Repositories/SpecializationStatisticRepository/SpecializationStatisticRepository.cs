using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.MongoModels;
using Umlaut.Database.Models.PostgresModels;
using Umlaut.Database.Repositories.FacultyRepository;
using Umlaut.Database.Repositories.GraduateRepository;
using Umlaut.Database.Repositories.SpecializationRepository;

namespace Umlaut.Database.Repositories.SpecializationStatisticRepository
{
    public class SpecializationStatisticRepository : MongoBaseRepository, ISpecializationStatisticRepository
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IFacultyRepository _facultyRepository;
        public SpecializationStatisticRepository(IMongoDatabase db, ISpecializationRepository specializationRepository, 
                                                IFacultyRepository facultyRepository) : base(db)
        {
            _specializationRepository = specializationRepository;
            _facultyRepository = facultyRepository;
        }

        public BsonArray GetExperienceSalaryByName(string name)
        {
            var collection = _db.GetCollection<BsonDocument>("SpecializationStatistics");
            var projection = Builders<BsonDocument>.Projection.Include("ExperienceSalaryList").Exclude("_id");
            return collection.Find(new BsonDocument { { "Name", name } }).Project(projection).First().GetValue("ExperienceSalaryList").AsBsonArray;
        }

        public BsonArray GetFacultiesStatisticByName(string name)
        {
            var collection = _db.GetCollection<BsonDocument>("SpecializationStatistics");
            var projection = Builders<BsonDocument>.Projection.Include("FacultyList").Exclude("_id");
            return collection.Find(new BsonDocument { { "Name", name } }).Project(projection).First().GetValue("FacultyList").AsBsonArray;
        }

        public BsonDocument GetStatisticByName(string name)
        {
            var collection = _db.GetCollection<BsonDocument>("SpecializationStatistics");
            var projection = Builders<BsonDocument>.Projection.Exclude("FacultyList").Exclude("ExperienceSalaryList").Exclude("_id");
            return collection.Find(new BsonDocument { { "Name", name } }).Project(projection).First();
        }

        public async Task UpdateStatisticsAsync()
        {
            var collection = _db.GetCollection<SpecializationStatistic>("SpecializationStatistics");
            foreach (var specialization in _specializationRepository.GetSpecializationsGraduatesIncludedList())
            {
                var statistic = new SpecializationStatistic();
                var graduates = specialization.Graduates;
                statistic.Name = specialization.Name;
                if (graduates.Any(item => item.ExpectedSalary != 0))
                    statistic.AverageSalary = (int)graduates.Where(item => item.ExpectedSalary != 0).Average(item => item.ExpectedSalary);
                else
                    statistic.AverageSalary = 0;
                statistic.AverageExperience = (int)graduates.Average(item => item.Experience);
                statistic.StartYearAverage = (int)graduates.Average(item => item.Age - item.Experience);
                statistic.FacultyList = GetFacultyList(graduates);
                statistic.ExperienceSalaryList = GetExperienceSalaryList(graduates);
                statistic.ResumeCount = graduates.Count();
                await collection.ReplaceOneAsync(Builders<SpecializationStatistic>.Filter.Eq(item => item.Name, specialization.Name), statistic, new ReplaceOptions { IsUpsert = true });
            }
        }

        private List<ExperienceSalary> GetExperienceSalaryList(IEnumerable<Graduate> graduates)
        {
            var list = new List<ExperienceSalary>();
            for (int exp = 0; exp <= graduates.Max(item => item.Experience); exp++)
                if (graduates.Where(item => item.Experience == exp && item.ExpectedSalary != 0).Any())
                    list.Add(new ExperienceSalary
                    { 
                        Experience = exp,
                        Salary = graduates.Where(item => item.Experience == exp && item.ExpectedSalary != 0).Select(item => item.ExpectedSalary).ToList(),
                    });
            return list;
        }

        private List<NameCountPair> GetFacultyList(IEnumerable<Graduate> graduates)
        {
            var list = new List<NameCountPair>();
            foreach (var faculty in _facultyRepository.GetFacultiesList().Where(i => i.Name != "Мусор"))
                if (graduates.Where(item => item.Faculty == faculty).Any())
                    list.Add(new NameCountPair
                    {
                        Name = faculty.Name,
                        Count = graduates.Count(item => item.Faculty == faculty)
                    });
            return list.OrderByDescending(item => item.Count).ToList();
        }
    }
}
