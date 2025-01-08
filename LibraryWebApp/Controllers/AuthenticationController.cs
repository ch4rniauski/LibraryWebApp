using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<RegisterUserRecord> _validator;
        private readonly IMapper _mapper;

        public AuthenticationController(IUnitOfWork uow, IValidator<RegisterUserRecord> validator, IMapper mapper)
        {
            _uow = uow;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            await _uow.AuthenticationRepository.RegisterUser(request);

            await _uow.Save();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LogInResponseRecord>> LogIn([FromBody] LogInRequest request)
        {
            var result = await _validator.ValidateAsync(_mapper.Map<RegisterUserRecord>(request));

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            var response = await _uow.AuthenticationRepository.LogInUser(request, HttpContext);

            await _uow.Save();

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
            await _uow.AuthenticationRepository.DeleteUser(id);

            await _uow.Save();

            return Ok();
        }

        [HttpGet("relogin")]
        public async Task<ActionResult<string>> UpdateAccessToken(Guid id)
        {
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out string? refreshToken);

            if (refreshToken is null)
                return BadRequest("Refresh token doesn't exist");

            var accessToken = await _uow.AuthenticationRepository.UpdateAccessToken(id, refreshToken);

            HttpContext.Response.Cookies.Append("accessToken", accessToken);
            return Ok();
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
