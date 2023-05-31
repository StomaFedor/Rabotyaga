using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Umlaut.Database.Models.MongoModels;
using Umlaut.Database.Models.PostgresModels;
using Umlaut.Database.Repositories.FacultyRepository;
using Umlaut.Database.Repositories.GraduateRepository;
using Umlaut.Database.Repositories.SpecializationRepository;
using Newtonsoft.Json.Linq;

namespace Umlaut.Database.Repositories.FacultyStatisticRepository
{
    public class FacultyStatisticRepository : MongoBaseRepository, IFacultyStatisticRepository
    {
        private JArray faculties;
        private readonly IFacultyRepository _facultyRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IGraduateRepository _graduateRepository;
        public FacultyStatisticRepository(IMongoDatabase db, IFacultyRepository facultyRepository,
                                          ISpecializationRepository specializationRepository, IGraduateRepository graduateRepository) : base(db)
        {
            _facultyRepository = facultyRepository;
            _specializationRepository = specializationRepository;
            _graduateRepository = graduateRepository;
            faculties = JArray.Load(new JsonTextReader(File.OpenText("faculties.json")));
        }

        public BsonDocument GetStatisticByName(string name)
        {
            var collection = _db.GetCollection<BsonDocument>("FacultyStatistics");
            var projection = Builders<BsonDocument>.Projection.Exclude("GraduationYearList").Exclude("SpecializationList")
                                                              .Exclude("ExperienceSalaryList").Exclude("_id");
            return collection.Find(new BsonDocument { { "Name", name } }).Project(projection).First();
        }

        public BsonArray GetExperienceSalaryByName(string name)
        {
            var collection = _db.GetCollection<BsonDocument>("FacultyStatistics");
            var projection = Builders<BsonDocument>.Projection.Include("ExperienceSalaryList").Exclude("_id");
            return collection.Find(new BsonDocument { { "Name", name } }).Project(projection).First().GetValue("ExperienceSalaryList").AsBsonArray;
        }

        public BsonArray GetGraduationYearsByName(string name)
        {
            var collection = _db.GetCollection<BsonDocument>("FacultyStatistics");
            var projection = Builders<BsonDocument>.Projection.Include("GraduationYearList").Exclude("_id");
            return collection.Find(new BsonDocument { { "Name", name } }).Project(projection).First().GetValue("GraduationYearList").AsBsonArray;
        }

        public BsonArray GetSpecializationsByName(string name)
        {
            var collection = _db.GetCollection<BsonDocument>("FacultyStatistics");
            var projection = Builders<BsonDocument>.Projection.Include("SpecializationList").Exclude("_id");
            return collection.Find(new BsonDocument { { "Name", name } }).Project(projection).First().GetValue("SpecializationList").AsBsonArray;
        }

        public async Task UpdateStatisticsAsync()
        {
            var collection = _db.GetCollection<FacultyStatistic>("FacultyStatistics");
            await UpdateDepartment(collection);
            await UpdateFaculty(collection);
        }

        private async Task UpdateFaculty(IMongoCollection<FacultyStatistic> collection)
        {
            var allGraduates = _graduateRepository.GetGraduatesSpecializationsIncludedList();
            foreach (var faculty in _facultyRepository.GetFacultiesGraduatesIncludedList())
            {
                var statistic = CreateGeneralStatistics(faculty.Graduates);
                statistic.Name = faculty.Name;
                statistic.SpecializationMatchPercent = (int)GetSpecializationMatchPercent(faculty.Name, faculty.Graduates);
                await collection.ReplaceOneAsync(Builders<FacultyStatistic>.Filter.Eq(item => item.Name, faculty.Name), statistic, new ReplaceOptions { IsUpsert = true });
            }
        }

        private double GetSpecializationMatchPercent(string faculty, IEnumerable<Graduate> graduates)
        {
            var fac = faculties.First(item => item["Title"].Value<string>() == faculty);
            if(fac == null || fac["Specializations"] == null)
                return 0;
            else
            {
                var specs = fac.SelectToken("Specializations").ToObject<List<string>>();
                var count = graduates.Count(g => g.Specializations.Any(s => specs.Any(spec => spec == s.Name)));
                return (double)count / graduates.Count() * 100;
            }
        }

        private async Task UpdateDepartment(IMongoCollection<FacultyStatistic> collection)
        {
            var allGraduates = _graduateRepository.GetGraduatesSpecializationsIncludedList();
            foreach (var department in _facultyRepository.GetFacultiesList().Select(item => item.Department).Distinct())
            {
                var statistic = CreateGeneralStatistics(allGraduates.Where(item => item.Faculty.Department == department));
                statistic.Name = department;
                await collection.ReplaceOneAsync(Builders<FacultyStatistic>.Filter.Eq(item => item.Name, department), statistic, new ReplaceOptions { IsUpsert = true });
            }
        }

        private FacultyStatistic CreateGeneralStatistics(IEnumerable<Graduate> graduates)
        {
            var statistic = new FacultyStatistic();
            statistic.AverageSalary = (int)graduates.Average(item => item.ExpectedSalary); //добавить условие про зп
            statistic.AverageExperience = (int)graduates.Average(item => item.Experience);
            statistic.AverageGraduationYear = (int)graduates.Average(item => item.YearGraduation);
            statistic.StartYearAverage = (int)graduates.Average(item => item.Age - item.Experience);
            statistic.GraduationYearList = GetGraduationYearList(graduates);
            statistic.SpecializationList = GetSpecializationList(graduates);
            statistic.ExperienceSalaryList = GetExperienceSalaryList(graduates);
            statistic.ResumeCount = graduates.Count();
            return statistic;
        }

        private List<NameCountPair> GetGraduationYearList(IEnumerable<Graduate> graduates)
        {
            var list = new List<NameCountPair>();
            for (int year = graduates.Min(item => item.YearGraduation); year <= graduates.Max(item => item.YearGraduation); year++)
                if (graduates.Where(item => item.YearGraduation == year).Any())
                    list.Add(new NameCountPair
                    {
                        Name = year.ToString(),
                        Count = graduates.Where(item => item.YearGraduation == year).Count()
                    });
            return list;
        }

        private List<NameCountPair> GetSpecializationList(IEnumerable<Graduate> graduates)
        {
            var list = new List<NameCountPair>();
            foreach (var specialization in _specializationRepository.GetSpecializationsList().Where(i => i.Name != "Другое" || i.Name != "Other"))
                if (graduates.Where(item => item.Specializations.Contains(specialization)).Any())
                    list.Add(new NameCountPair
                    {
                        Name = specialization.Name,
                        Count = graduates.Where(item => item.Specializations.Contains(specialization)).Count(),
                    });
            return list.OrderByDescending(item => item.Count).ToList();
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
    }
}
