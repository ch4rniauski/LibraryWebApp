﻿using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IValidator<RegisterUserRecord> _validator;

        public AuthenticationController(IUnitOfWork uof, IValidator<RegisterUserRecord> validator)
        {
            _uof = uof;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            await _uof.AuthenticationRepository.RegisterUser(request);

            _uof.Save();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LogInResponseRecord>> LogIn([FromBody] LogInRequest request)
        {
            var result = await _validator.ValidateAsync(request.Adapt<RegisterUserRecord>());

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            var response = await _uof.AuthenticationRepository.LogInUser(request, HttpContext);

            _uof.Save();

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
            await _uof.AuthenticationRepository.DeleteUser(id);

            _uof.Save();

            return Ok();
        }

        [HttpGet("relogin")]
        public async Task<ActionResult<string>> UpdateAccessToken(Guid id)
        {
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out string? refreshToken);

            if (refreshToken is null)
                return BadRequest("Refresh token doesn't exist");

            var accessToken = await _uof.AuthenticationRepository.UpdateAccessToken(id, refreshToken);

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
