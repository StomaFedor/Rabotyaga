using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Models.MongoModels
{
    public class LocationStatistic
    {
        public string Name { get; set; }

        public int AverageSalary { get; set; }

        public double Percent { get; set; }

        public int ResumeCount { get; set; }
    }
}
