using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database.Repositories.SpecializationRepository
{
    public interface ISpecializationRepository
    {
        IEnumerable<Specialization> GetSpecializationsList();

        IEnumerable<Specialization> GetSpecializationsGraduatesIncludedList();

        void CreateSpecialization(Specialization specialization);
        void DeleteSpecialization(string specialization);
    }
}
