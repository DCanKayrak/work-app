using Business.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRatingService _userRatingService;
        public AuthController(
            IAuthService authService,
            IUserRatingService userRatingService
            )
        {
            _authService = authService;
            _userRatingService = userRatingService;
        }
        [HttpPost("Register")]
        public IActionResult Register(RegisterRequestDto req)
        {
            var userExist = _authService.UserExists(req.Email);

            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            var registerUser = _authService.Register(req, req.Password);
            _userRatingService.Create(new UserRating(registerUser.Data.Id));
            var result = _authService.CreateAccessToken(registerUser.Data);
            if (!registerUser.Success)
            {
                return BadRequest(registerUser.Message);
            }
            return Ok(result);

        }
        [HttpPost("Login")]
        public IActionResult Login(LoginRequestDto req)
        {
            var userToLogin = _authService.Login(req);

            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }
            var result = _authService.CreateAccessToken(userToLogin.Data);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }
    }
}
