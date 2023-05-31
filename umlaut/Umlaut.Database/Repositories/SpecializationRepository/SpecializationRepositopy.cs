using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database.Repositories.SpecializationRepository
{
    public class SpecializationRepositopy : BaseRepository, ISpecializationRepository
    {
        public SpecializationRepositopy(UmlautDBContext context) : base(context) { }

        public void CreateSpecialization(Specialization specialization)
        {
            if (specialization.Name == String.Empty)
                throw new ArgumentException();
            if (_context.Specializations.Any(u => u.Name == specialization.Name))
                throw new InvalidOperationException("Such a specialization already exists");
            _context.Specializations.Add(specialization);
            _context.SaveChanges();

        }

        public void DeleteSpecialization(string deleteSpecializationStr)
        {
            if (!_context.Specializations.Any(u => u.Name == deleteSpecializationStr))
                throw new InvalidOperationException("There is no such specialization");
            var deleteSpecialization = _context.Specializations.FirstOrDefault(u => u.Name == deleteSpecializationStr);
            _context.Specializations.Remove(deleteSpecialization);
            _context.SaveChanges();

        }

        public IEnumerable<Specialization> GetSpecializationsList()
        {
            return _context.Specializations.ToList();
        }

        public IEnumerable<Specialization> GetSpecializationsGraduatesIncludedList()
        {
            return _context.Specializations.Include(item => item.Graduates).ToList();
        }
    }
}
