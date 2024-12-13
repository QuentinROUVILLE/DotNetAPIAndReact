using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[ApiController, Route("api/v1/auth")]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    public ActionResult<LoginResponse> Login(LoginModel credentials)
    {
        if (credentials is not {Login: "admin", Password: "admin"}) return Unauthorized();
        
        var token = AuthHelpers.GenerateToken();
        return Ok(new LoginResponse {Token = token});
    }
}