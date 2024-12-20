using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IValidator<RegisterUserRecord> _validator;

        public UserController(IUnitOfWork uof, IValidator<RegisterUserRecord> validator)
        {
            _uof = uof;
            _validator = validator;
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<RegisterUserRecord?>> GetUserInfo(Guid id)
        {
            var user = await _uof.UserRepository.GetUserInfo(id);

            if (user is null)
                return BadRequest("User with that ID wasn't found");
            return Ok(user);
        }

        [HttpPost("borrow")]
        [Authorize]
        public async Task<ActionResult> BorrowBook([FromBody] BorrowBookRequest request)
        {
            await _uof.UserRepository.BorrowBook(request.UserId, request.BookId);

            _uof.Save();

            return Ok();
        }
    }
}
