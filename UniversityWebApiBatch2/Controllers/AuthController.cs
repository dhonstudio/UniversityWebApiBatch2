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
            try
            {
                if (authFeature.Login(loginModel))
                {
                    var token = authFeature.GenerateToken(loginModel.Username);

                    var finalToken = new
                    {
                        Token = token
                    };
                    return Ok(finalToken);
                }
                return Unauthorized();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult createUser(UsersParamsDTO userParam)
        {
            try
            {
                var result = authFeature.CreateUser(userParam);

                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create/role")]
        public IActionResult createUserRole(UsersRoleParamsDTO userRoleParam)
        {
            try
            {
                var result = authFeature.CreateUserRole(userRoleParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
