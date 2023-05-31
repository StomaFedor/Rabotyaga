using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Umlaut.Database.Repositories.LocationStatisticRepository;

namespace Umlaut.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly ILocationStatisticRepository _locationStatisticRepository;

        public LocationsController(ILocationStatisticRepository locationStatisticRepository)
        {
            _locationStatisticRepository = locationStatisticRepository;
        }

        [HttpGet("{locationName}")]
        public async Task<IActionResult> GetLocations(string locationName) 
        {
            IActionResult response;
            try
            {
                var statistic = _locationStatisticRepository.GetStatisticsByName(locationName).ToJson();
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
