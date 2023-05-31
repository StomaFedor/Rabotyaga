using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database.Repositories.LocationRepository
{
    public class LocationRepositopy : BaseRepository, ILocationRepository
    {
        public LocationRepositopy(UmlautDBContext context) : base(context) { }

        public void CreateLocation(Location newLocation)
        {
            if (newLocation.Name == String.Empty)
                throw new ArgumentException();
            if (_context.Locations.Any(u => u.Name == newLocation.Name))
                throw new InvalidOperationException("Such a location already exists");
            _context.Locations.Add(newLocation);
            _context.SaveChanges();

        }

        public void DeleteLocation(string deleteLocationStr)
        {
            if (!_context.Locations.Any(u => u.Name == deleteLocationStr))
                throw new InvalidOperationException("There is no such location");
            var deleteLocation = _context.Locations.FirstOrDefault(u => u.Name == deleteLocationStr);
            _context.Locations.Remove(deleteLocation);
            _context.SaveChanges();

        }

        public IEnumerable<Location> GetLocationsList()
        {
            var list = _context.Locations.ToList();
            return list;
        }

        public IEnumerable<Location> GetLocationsGraduatesIncludedList()
        {
            var list = _context.Locations.Include(item => item.Graduates).ToList();
            return list;
        }
    }
}
