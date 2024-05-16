using Business.Abstract;
using Entities.Concrete.Dto.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
            var result = _authService.CreateAccessToken(registerUser.Data);
            if (!registerUser.Success)
            {
                return BadRequest(registerUser.Message);
            }
            return Ok(result.Data);

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
