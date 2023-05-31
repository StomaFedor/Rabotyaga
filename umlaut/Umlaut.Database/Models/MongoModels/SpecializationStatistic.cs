using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Models.MongoModels
{
    public class SpecializationStatistic
    {
        public string Name { get; set; }

        public int AverageSalary { get; set; }

        public int AverageExperience { get; set; }

        public int StartYearAverage { get; set; }

        public int ResumeCount { get; set; }

        public List<NameCountPair> FacultyList { get; set; }

        public List<ExperienceSalary> ExperienceSalaryList { get; set; }
    }

    public class ExperienceSalary 
    {
        public int Experience { get; set; }

        public List<int> Salary { get; set; }
    }
}
