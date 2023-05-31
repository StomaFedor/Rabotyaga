using Umlaut.Database.Repositories.CommonStatisticRepository;
using Umlaut.Database.Repositories.FacultyStatisticRepository;
using Umlaut.Database.Repositories.LocationStatisticRepository;
using Umlaut.Database.Repositories.SpecializationStatisticRepository;

namespace Umlaut.WebService.DBUpdaterService.DBUpdaters
{
    public class StatisticsDBUpdater : IDBUpdater
    {
        private ICommonStatisticRepository _commonStatRepository;
        private ILocationStatisticRepository _locationStatRepository;
        private ISpecializationStatisticRepository _specializationStatRepository;
        private IFacultyStatisticRepository _facultyStatRepository;

        public StatisticsDBUpdater(ICommonStatisticRepository commonStatRepository, ILocationStatisticRepository locationStatRepository, 
                                   ISpecializationStatisticRepository specializationStatRepository, IFacultyStatisticRepository facultyStatRepository)
        {
            _commonStatRepository = commonStatRepository;
            _locationStatRepository = locationStatRepository;
            _specializationStatRepository = specializationStatRepository;
            _facultyStatRepository = facultyStatRepository;
        }

        public async Task Update()
        {
            await _commonStatRepository.UpdateStatisticsAsync();
            await _locationStatRepository.UpdateStatisticsAsync();
            await _specializationStatRepository.UpdateStatisticsAsync();
            await _facultyStatRepository.UpdateStatisticsAsync();
        }
    }
}
