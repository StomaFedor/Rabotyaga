using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database.Repositories.LocationRepository
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetLocationsList();

        IEnumerable<Location> GetLocationsGraduatesIncludedList();
        void CreateLocation(Location location);
        void DeleteLocation(string location);
    }
}
