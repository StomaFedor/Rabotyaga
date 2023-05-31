using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Umlaut.Database.Repositories.SpecializationStatisticRepository;

namespace Umlaut.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationsController : Controller
    {
        private readonly ISpecializationStatisticRepository _SpecializationStatisticRepository;

        public SpecializationsController(ISpecializationStatisticRepository specializationStatisticRepository)
        {
            _SpecializationStatisticRepository = specializationStatisticRepository;
        }

        [HttpGet("{specializationName}/salary2experience")]
        public async Task<IActionResult> GetSalaryToExperienceRatio(string specializationName) 
        {
            IActionResult response;
            try
            {
                var statistic = _SpecializationStatisticRepository.GetExperienceSalaryByName(specializationName).ToJson();
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

        [HttpGet("{specializationName}")]
        public async Task<IActionResult> GetAverageValuesForCurrentSpecialization(string specializationName) 
        {
            IActionResult response;
            try
            {
                var statistic = _SpecializationStatisticRepository.GetStatisticByName(specializationName).ToJson();
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

        [HttpGet("{specializationName}/faculties")]
        public async Task<IActionResult> GetFacultiesForCurrentSpecialization(string specializationName)
        {
            IActionResult response;
            try
            {
                var statistic = _SpecializationStatisticRepository.GetFacultiesStatisticByName(specializationName).ToJson();
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
