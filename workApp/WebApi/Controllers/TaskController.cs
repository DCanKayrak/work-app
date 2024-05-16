using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dto.Requests.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;

        public TaskController
            (
            ITaskService taskService
            )
        {
            _taskService = taskService;
        }

        [HttpPost("/collections")]
        public IActionResult CreateTaskCollection(CreateTaskCollectionRequest request)
        {
            return HandleResponse(_taskService.Create(request));
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
