using Business.Abstract;
using Business.DependencyResolvers.Mapper;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto.Requests.Task;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskCollectionService _taskCollectionService;
        private readonly ITaskService _taskService;

        public TaskController
        (
            ITaskService taskService,
            ITaskCollectionService taskCollectionService
        )
        {
            _taskService = taskService;
            _taskCollectionService = taskCollectionService;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return HandleResponseWithData(_taskService.GetAll(null));
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskWithId(int id)
        {
            return HandleResponseWithData(_taskService.Get(id));
        }

        [HttpPost]
        public IActionResult CreateTask(CreateTaskRequest request)
        {
            return HandleResponseWithData(_taskService.Create(MapperHelper<CreateTaskRequest, TaskItem>.Map(request)));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaskWithId(int id)
        {
            return HandleResponse(_taskService.Delete(id));
        }

        [HttpPost("changeStatus/{id}")]
        public IActionResult ChangeTaskStatus(int id)
        {
            return HandleResponse(_taskService.ChangeTaskStatus(id));
        }

        /* Collections */

        [HttpGet("collections")]
        public IActionResult GetAllTaskCollections()
        {
            return HandleResponseWithData( _taskCollectionService.GetAll(null));
        }

        [HttpGet("collections/{id}")]
        public IActionResult GetTaskCollection(int id)
        {
            return HandleResponseWithData(_taskCollectionService.Get(id));
        }

        [HttpPost("collections")]
        public IActionResult CreateTaskCollection(CreateTaskCollectionRequest request)
        {
            return HandleResponseWithData(_taskCollectionService.Create(MapperHelper<CreateTaskCollectionRequest, TaskCollection>.Map(request)));
        }

        private IActionResult HandleResponse(IResult result)
        {
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        private IActionResult HandleResponseWithData<T>(IDataResult<T> result)
        {
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
