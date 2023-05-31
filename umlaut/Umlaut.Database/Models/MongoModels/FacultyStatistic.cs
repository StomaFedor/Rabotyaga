using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Models.MongoModels
{
    public class FacultyStatistic
    {
        public string Name { get; set; }

        public int AverageSalary { get; set; }

        public int AverageGraduationYear { get; set; }

        public int SpecializationMatchPercent { get; set; }

        public int AverageExperience { get; set; }

        public List<NameCountPair> GraduationYearList { get; set; }

        public List<NameCountPair> SpecializationList { get; set; }

        public List<ExperienceSalary> ExperienceSalaryList { get; set; }

        public int StartYearAverage { get; set; }

        public int ResumeCount { get; set; }
    }
}