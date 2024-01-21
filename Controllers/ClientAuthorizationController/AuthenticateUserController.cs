using Microsoft.AspNetCore.Mvc;

namespace bike_mind_quest.Controllers.ClientAuthorizationController
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticateUserController : ControllerBase
    {
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] AuthenticateRequest request)
        {
            bool success = ConfirmAuthenticationController.SignUp(request.Username, request.Password);
            return success ? Ok(new { Message = "User signed up successfully." }) : BadRequest(new { Message = "Username already exists." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateRequest request)
        {
            bool success = ConfirmAuthenticationController.Login(request.Username, request.Password);
            return success ? Ok(new { Message = "User logged in successfully." }) : Unauthorized(new { Message = "Invalid username or password." });
        }
    }

    public class AuthenticateRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
