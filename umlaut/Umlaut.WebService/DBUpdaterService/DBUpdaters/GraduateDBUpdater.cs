using Umlaut.Database.Repositories.FacultyRepository;
using Umlaut.Database.Repositories.GraduateRepository;

namespace Umlaut.WebService.DBUpdaterService.DBUpdaters
{
    public class GraduateDBUpdater : IDBUpdater
    {
        private HHruAPI _api;
        private IGraduateRepository _repository;

        public GraduateDBUpdater(HHruAPI api, IGraduateRepository repository)
        {
            _api = api;
            _repository = repository;
        }

        public async Task Update()
        {
            var hrefList = await _api.GetProfileHrefs();
            foreach (var href in hrefList)
            {
                if (!_repository.IsAlreadyExists(href))
                {
                    try
                    {
                        Console.WriteLine(href);
                        var graduate = await _api.GetGraduate(href);
                        if (graduate != null)
                        {
                            _repository.CreateGraduate(graduate);
                            Console.WriteLine($"{graduate.Vacation} added");
                        }
                    }
                    catch (Exception ex)
                    {
                        using (StreamWriter mainText = File.AppendText("DBErrors.txt"))
                        {
                            mainText.WriteLine(ex.Message);
                            if (ex.InnerException != null)
                                mainText.WriteLine(ex.InnerException);
                            mainText.WriteLine(href);
                            mainText.WriteLine();
                        }
                    }
                }
            }
        }

    }
}
