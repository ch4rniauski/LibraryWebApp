﻿using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWorkAccounts _uof;
        private readonly IValidator<UserRecord> _validator;

        public AuthenticationController(IUnitOfWorkAccounts uof, IValidator<UserRecord> validator)
        {
            _uof = uof;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            var isRegistered = await _uof.AuthenticationRepository.RegisterUser(request);

            if (isRegistered == false)
                return BadRequest("User wasn't registered");

            _uof.Save();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LogInResponseRecord>> LogIn([FromBody] UserRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            var response = await _uof.AuthenticationRepository.LogInUser(request);

            if (response is null)
                return BadRequest("Incorrect data");

            _uof.Save();

            return Ok(response);
        }
    }
}
