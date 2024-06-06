using Business.Abstract;
using Business.DependencyResolvers.Mapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.Concrete.Dto.Requests.Follower;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly IFollowerService _followerService;

        public FollowerController
        (
            IFollowerService followerService
        )
        {
            _followerService = followerService;
        }

        [HttpGet]
        public IActionResult GetFollowers()
        {
            return HandleResponse(_followerService.GetAll(null));
        }

        [HttpGet("requests")]
        public IActionResult GetFollowerRequests()
        {
            return HandleResponse(_followerService.GetFollowerRequests());
        }

        [HttpPost("request/{id}/respond/{response}")]
        public IActionResult Respond(int id, bool response)
        {

            return HandleResponse(_followerService.RespondRequest(id, response));
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateFollowerRequest request)
        {
            return HandleResponse(_followerService.Create(MapperHelper<CreateFollowerRequest,Follower>.Map(request)));
        }

        public IActionResult HandleResponse<T>(IDataResult<T> result)
        {

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        
        private IActionResult HandleResponse(IResult result)
        {
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
