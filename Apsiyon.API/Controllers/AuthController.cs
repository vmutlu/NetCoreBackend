using Apsiyon.Business.Abstract;
using Apsiyon.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Apsiyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);

            if (userToLogin.Success is false)
                return BadRequest(userToLogin);

            var result = _authService.CreateAccessToken(userToLogin.Data);

            if (result.Success is false)
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExitst = _authService.UserExists(userForRegisterDto.Email);

            if (userExitst.Success is false)
                return BadRequest(userExitst);

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);

            if (result.Success is false)
                return BadRequest(result);

            else
                return Ok(result);
        }

    }
}
