using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database.Repositories.GraduateRepository
{
    public interface IGraduateRepository
    {
        IEnumerable<Graduate> GetGraduatesList();

        IEnumerable<Graduate> GetGraduatesSpecializationsIncludedList();
        void CreateGraduate(Graduate g);
        void DeleteGraduate(string resumeLink);
        void UpdateGraduate(Graduate newG);
        bool IsAlreadyExists(string resumeLink);
    }
}
