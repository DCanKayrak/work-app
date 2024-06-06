using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController
            (
                INotificationService notificationService
            )
        {
            _notificationService = notificationService;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return HandleResponse(_notificationService.GetAll(null));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return HandleResponse(_notificationService.Get(id));
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
