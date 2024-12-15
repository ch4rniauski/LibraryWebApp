using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Library.DataContext.UnitsOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkAccounts _uof;
        private readonly IValidator<UserRecord> _validator;

        public UserController(IUnitOfWorkAccounts uof, IValidator<UserRecord> validator)
        {
            _uof = uof;
            _validator = validator;
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<UserRecord?>> GetUsetInfo(Guid id)
        {
            var user = await _uof.UserRepository.GetUserInfo(id);

            if (user is null)
                return BadRequest("User with that ID wasn't found");
            return Ok(user);
        }
    }
}
