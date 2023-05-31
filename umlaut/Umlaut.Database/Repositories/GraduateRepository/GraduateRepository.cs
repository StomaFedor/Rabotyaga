using Microsoft.EntityFrameworkCore;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database.Repositories.GraduateRepository
{
    public class GraduateRepository : BaseRepository, IGraduateRepository
    {
        public GraduateRepository(UmlautDBContext context) : base(context) { }

        public void CreateGraduate(Graduate newGraduate)
        {
            var graduate = IsUnique(newGraduate);
            _context.Graduates.Add(graduate);
            _context.SaveChanges();
        }

        private Graduate IsUnique(Graduate graduate)
        {
            if (_context.Graduates.Any(u => u.ResumeLink == graduate.ResumeLink))
                throw new InvalidOperationException("Such a graduate already exists");
            if (_context.Faculties.Any(u => u.Name == graduate.Faculty.Name))
                graduate.Faculty = _context.Faculties.FirstOrDefault(u => u.Name == graduate.Faculty.Name);
            if (_context.Locations.Any(u => u.Name == graduate.Location.Name))
                graduate.Location = _context.Locations.FirstOrDefault(u => u.Name == graduate.Location.Name);
            List<Specialization> specializations = new List<Specialization>();
            foreach (var item in graduate.Specializations)
                if (!_context.Specializations.Any(u => u.Name == item.Name))
                {
                    _context.Specializations.Add(item);
                    specializations.Add(item);
                } else
                    specializations.Add(_context.Specializations.FirstOrDefault(u => u.Name == item.Name));
            graduate.Specializations = specializations;
            return graduate;
        }

        public void DeleteGraduate(string resumeLink)
        {
            var deleteGraduate = _context.Graduates.FirstOrDefault(u => u.ResumeLink == resumeLink);
            _context.Graduates.Remove(deleteGraduate);
            _context.SaveChanges();
        }

        public IEnumerable<Graduate> GetGraduatesList()
        {
            var list = _context.Graduates.Include(item => item.Location).ToList();
            return list;
        }
        public IEnumerable<Graduate> GetGraduatesSpecializationsIncludedList()
        {
            return _context.Graduates.Include(item => item.Specializations).ToList();
        }

        public void UpdateGraduate(Graduate newG)
        {
            var g = _context.Graduates.FirstOrDefault(u => u.ResumeLink == newG.ResumeLink);
            g.Gender = newG.Gender;
            g.Age = newG.Age;
            g.Location = newG.Location;
            g.Vacation = newG.Vacation;
            g.Specializations = newG.Specializations;
            g.ExpectedSalary = newG.ExpectedSalary;
            g.Experience = newG.Experience;
            g.YearGraduation = newG.YearGraduation;
            g.Faculty = newG.Faculty;
            _context.Entry(g).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public bool IsAlreadyExists(string resumeLink)
        {
            return _context.Graduates.Where(u => u.ResumeLink == resumeLink).Any();
        }
    }
}
