using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly ILogInUserUseCase _logInUserUseCase;
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly IUpdateAccessTokenUseCase _updateAccessTokenUseCase;

        public AuthenticationController(IDeleteUserUseCase deleteUserUseCase,
            ILogInUserUseCase logInUserUseCase,
            IRegisterUserUseCase registerUserUseCase,
            IUpdateAccessTokenUseCase updateAccessTokenUseCase)
        {
            _deleteUserUseCase = deleteUserUseCase;
            _logInUserUseCase = logInUserUseCase;
            _registerUserUseCase = registerUserUseCase;
            _updateAccessTokenUseCase = updateAccessTokenUseCase;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserRecord request)
        {
            await _registerUserUseCase.Execute(request);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LogInResponseRecord>> LogIn([FromBody] LogInRequest request)
        {
            var response = await _logInUserUseCase.Execute(request);

            HttpContext.Response.Cookies.Append("accessToken", response.AccessToken);
            HttpContext.Response.Cookies.Append("refreshToken", response.RefreshToken);
            HttpContext.Response.Cookies.Append("admin", response.IsAdmin.ToString());

            return Ok(response);
        }

        [HttpGet("auth")]
        [Authorize]
        public ActionResult IsAuthorized()
        {
            return Ok();
        }

        [HttpGet("admin")]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult IsAdmin()
        {
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _deleteUserUseCase.Execute(id);

            return Ok();
        }

        [HttpGet("relogin")]
        public async Task<ActionResult<string>> UpdateAccessToken(Guid id)
        {
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out string? refreshToken);

            var accessToken = await _updateAccessTokenUseCase.Execute(id, refreshToken);

            HttpContext.Response.Cookies.Append("accessToken", accessToken);

            return Ok(accessToken);
        }

        [HttpGet("logout")]
        public ActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("accessToken");
            HttpContext.Response.Cookies.Delete("refreshToken");
            HttpContext.Response.Cookies.Delete("admin");

            return Ok();
        }
    }
}
