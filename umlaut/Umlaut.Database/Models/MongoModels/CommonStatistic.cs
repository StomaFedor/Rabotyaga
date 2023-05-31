using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Models.MongoModels
{
    public class CommonStatistic
    {
        public int Id { get; set; }

        public int AverageSalary { get; set; }

        public int StartYearAverage { get; set; }

        public int GraduationYearAverage { get; set; }

        public List<NameCountPair> FacultyList { get; set; }

        public List<NameCountPair> LocationList { get; set; }

        public List<NameCountPair> SpecializationList { get; set; }

        public int ResumeCount { get; set; }
    }
}
