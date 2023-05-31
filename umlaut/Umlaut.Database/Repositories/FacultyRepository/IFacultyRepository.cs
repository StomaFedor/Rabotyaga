using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database.Repositories.FacultyRepository
{
    public interface IFacultyRepository
    {
        IEnumerable<Faculty> GetFacultiesList();

        IEnumerable<Faculty> GetFacultiesGraduatesIncludedList();
        void CreateFaculty(Faculty faculty);
        void DeleteFaculty(string faculty);
    }
}
