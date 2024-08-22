using Application.Features.AuthFeature;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApiBatch2.Controllers
{
    public class AuthController(AuthFeature authFeature) : Controller
    {
        [HttpPost("login")]
        public IActionResult login([FromBody] LoginParamsDTO loginModel)
        {
            if (loginModel.Username == "string" && loginModel.Password == "string")
            {
                var token = authFeature.GenerateToken(loginModel.Username);

                var finalToken = new
                {
                    Token = token
                };
                return Ok(finalToken);
            }
            return Unauthorized();
        }
    }
}
