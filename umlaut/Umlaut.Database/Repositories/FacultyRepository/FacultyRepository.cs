using Microsoft.EntityFrameworkCore;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database.Repositories.FacultyRepository
{
    public class FacultyRepository : BaseRepository, IFacultyRepository
    {
        public FacultyRepository(UmlautDBContext context) : base(context) { }

        public void CreateFaculty(Faculty newFaculty)
        {
            if (newFaculty.Name == String.Empty)
                throw new ArgumentException();
            if (_context.Faculties.Any(u => u.Name == newFaculty.Name))
                throw new InvalidOperationException("Such a faculty already exists");
            _context.Faculties.Add(newFaculty);
            _context.SaveChanges();

        }

        public void DeleteFaculty(string deleteFacultyStr)
        {
            if (!_context.Faculties.Any(u => u.Name == deleteFacultyStr))
                throw new InvalidOperationException("There is no such faculty");
            var deleteFaculty = _context.Faculties.FirstOrDefault(u => u.Name == deleteFacultyStr);
            _context.Faculties.Remove(deleteFaculty);
            _context.SaveChanges();

        }

        public IEnumerable<Faculty> GetFacultiesList()
        {
            return _context.Faculties.Include(item => item.Graduates).ToList();
        }
        public IEnumerable<Faculty> GetFacultiesGraduatesIncludedList()
        {
            return _context.Faculties.ToList();
        }
    }
}
