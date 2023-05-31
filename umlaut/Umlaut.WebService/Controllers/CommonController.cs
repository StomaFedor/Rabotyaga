using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Umlaut.Database.Repositories.CommonStatisticRepository;

namespace Umlaut.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : Controller
    {
        private readonly ICommonStatisticRepository _commonStatisticRepository;

        public CommonController(ICommonStatisticRepository commonStatisticRepository)
        {
            _commonStatisticRepository = commonStatisticRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommonStatistic()
        {
            IActionResult response;
            try
            {
                var statistic = _commonStatisticRepository.GetStatistics().ToJson();
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

        [HttpGet("faculties")]
        public async Task<IActionResult> GetFacultiesStatistic()
        {
            IActionResult response;
            try
            {
                var statistic = _commonStatisticRepository.GetFacultiesStatistics().ToJson();
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

        [HttpGet("specializations")]
        public async Task<IActionResult> GetSpecializationsStatistic()
        {
            IActionResult response;
            try
            {
                var statistic = _commonStatisticRepository.GetSpecializationsStatistics().ToJson();
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

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocationsStatistic()
        {
            IActionResult response;
            try
            {
                var statistic = _commonStatisticRepository.GetLocationsStatistics().ToJson();
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
