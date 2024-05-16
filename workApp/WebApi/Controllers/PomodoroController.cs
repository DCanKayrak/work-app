using Business.Abstract;
using Business.DependencyResolvers.Mapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto.Requests;
using Entities.Concrete.Dto.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return HandleResponse(_pomodoroService.GetAll());
        }

        [HttpGet("/users/{userId}/pomodoros/{date}")]
        public IActionResult GetAllWithUserAndDate(int userId, DateTime date)
        {
            return HandleResponse(_pomodoroService.GetAllWithUserAndDate(userId,date));
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
