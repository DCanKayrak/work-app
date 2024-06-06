using Business.Abstract;
using Business.Constants;
using Business.DependencyResolvers.Mapper;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto.Requests.Pomodoro;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        private IPomodoroService _pomodoroService;

        public PomodoroController
            (
            IPomodoroService pomodoroService
            ) 
        {
            this._pomodoroService = pomodoroService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return HandleResponse(_pomodoroService.GetAll(null));
        }

        [HttpGet("users/{userId}/pomodoros/{date}")]
        public IActionResult GetAllWithUserAndDate(int userId, DateTime date)
        {
            return HandleResponse(_pomodoroService.GetAllWithUserAndDate(userId,date));
        }

        [HttpGet("totalTime")]
        public IActionResult GetTotalTime()
        {
            return HandleResponse(_pomodoroService.GetTotalPomodoroDuration());
        }
        
        [HttpGet("pomodoros/from/{startDate}/to/{endDate}")]
        public IActionResult GetAllWithUserAndDate(DateTime startDate, DateTime endDate)
        {
            return HandleResponse(_pomodoroService.GetAllBetweenTwoDates(startDate,endDate));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return HandleResponse(_pomodoroService.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePomodoroRequest req)
        {
            return HandleResponse(_pomodoroService.Create(MapperHelper<CreatePomodoroRequest,Pomodoro>.Map(req)));
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
