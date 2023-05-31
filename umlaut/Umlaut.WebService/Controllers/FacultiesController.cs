using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Umlaut.Database.Repositories.FacultyStatisticRepository;

namespace Umlaut.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultiesController : Controller
    {
        private readonly IFacultyStatisticRepository _facultyStatisticRepository;

        public FacultiesController(IFacultyStatisticRepository facultyStatisticRepository)
        {
            _facultyStatisticRepository = facultyStatisticRepository;
        }

        [HttpGet("{facultyName}")]
        public async Task<IActionResult> GetAverageValuesForCurrentFaculty(string facultyName) 
        {
            IActionResult response;
            try
            {
                var statistic = _facultyStatisticRepository.GetStatisticByName(facultyName).ToJson();
                response = Ok(statistic);
            }
            catch (InvalidOperationException ex)
            {
                response = StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpGet("{facultyName}/specializations")]
        public async Task<IActionResult> GetSpecializationsForCurrentFaculty(string facultyName) 
        {
            IActionResult response;
            try
            {
                var statistic = _facultyStatisticRepository.GetSpecializationsByName(facultyName).ToJson();
                response = Ok(statistic);
            }
            catch (InvalidOperationException ex)
            {
                response = StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpGet("{facultyName}/salary2experience")]
        public async Task<IActionResult> GetSalaryToExperienceRatio(string facultyName) 
        {
            IActionResult response;
            try
            {
                var statistic = _facultyStatisticRepository.GetExperienceSalaryByName(facultyName).ToJson();
                response = Ok(statistic);
            }
            catch (InvalidOperationException ex)
            {
                response = StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpGet("{facultyName}/year_graduations")]
        public async Task<IActionResult> GetYearGraduations(string facultyName) 
        {
            IActionResult response;
            try
            {
                var statistic = _facultyStatisticRepository.GetGraduationYearsByName(facultyName).ToJson();
                response = Ok(statistic);
            }
            catch (InvalidOperationException ex)
            {
                response = StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }
    }
}
