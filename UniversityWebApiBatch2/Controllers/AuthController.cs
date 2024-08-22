using Application.Features.AuthFeature;
using AutoMapper;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApiBatch2.Controllers
{
    public class AuthController(AuthFeature authFeature, IMapper mapper) : Controller
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

        [HttpPost("generatepublictoken")]
        public IActionResult generatePublic([FromHeader] string ClientId, [FromHeader] string ClientSecret)
        {
            try
            {
                var token = authFeature.GeneratePublicToken(Request);

                if (token != null) { 
                    var finalToken = new
                    {
                        Token = token
                    };
                    return Ok(finalToken);
                }
                return Unauthorized();
            }
            catch (Exception ex)
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

        [HttpPost("changerole")]
        public IActionResult changeRole(UsersRoleParamsDTO userRoleParam)
        {
            try
            {
                var result = authFeature.ChangeRole(userRoleParam);

                if (result)
                {
                    var userRoleDTO = mapper.Map<UsersRoleDTO>(userRoleParam);
                    var token = authFeature.GenerateToken(userRoleParam.Username, userRoleDTO);

                    var finalToken = new
                    {
                        Token = token
                    };
                    return Ok(finalToken);
                }

                return BadRequest("Gagal mengganti Role");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
