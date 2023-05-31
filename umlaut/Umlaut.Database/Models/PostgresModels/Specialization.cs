using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Models.PostgresModels
{
    public class Specialization
    {
        //Id специализации
        public int Id { get; set; }

        //Специализация
        public string Name { get; set; }

        //Все выпускники конкретной специализации
        public List<Graduate> Graduates { get; set; }
    }
}
