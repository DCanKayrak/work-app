using Business.Abstract;
using Business.BusinessAspects;
using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SecuredOperation("USER")]
    public class UserRatingController : ControllerBase
    {
        private readonly IUserRatingService _userRating;

        public UserRatingController
            (
                IUserRatingService userRating
            )
        {
            _userRating = userRating;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return HandleResponse(_userRating.Get(id));
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
