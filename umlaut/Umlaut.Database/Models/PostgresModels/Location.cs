using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Models.PostgresModels
{
    public class Location
    {
        //Id города
        public int Id { get; set; }

        //Город
        public string Name { get; set; }

        //Все выпускники в конкретном городе
        public List<Graduate> Graduates { get; set; }
    }
}
