using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _leagueService;

        public LeagueController
            (
                ILeagueService leagueService
            )
        {
            _leagueService = leagueService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return HandleResponse(_leagueService.GetAll(null));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return HandleResponse(_leagueService.Get(id));
        }
        
        public IActionResult HandleResponse<T>(IDataResult<T> result)
        {

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
