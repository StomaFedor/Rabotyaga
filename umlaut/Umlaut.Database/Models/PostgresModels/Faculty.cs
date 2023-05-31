using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Models.PostgresModels
{
    public class Faculty
    {
        //Id факультета
        public int Id { get; set; }

        //Факультет
        public string? Name { get; set; }


        public string? Department { get; set; }
        //Все выпускники конкретного факультета
        public List<Graduate> Graduates { get; set; }
    }
}
