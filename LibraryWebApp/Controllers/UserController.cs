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
        private readonly IUnitOfWork _uow;
        private readonly IValidator<RegisterUserRecord> _validator;

        public UserController(IUnitOfWork uow, IValidator<RegisterUserRecord> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<UserInfoResponse>> GetUserInfo(Guid id)
        {
            var user = await _uow.UserRepository.GetUserInfo(id);

            return Ok(user);
        }

        [HttpPost("borrow")]
        [Authorize]
        public async Task<ActionResult> BorrowBook([FromBody] BorrowBookRequest request)
        {
            await _uow.UserRepository.BorrowBook(request.UserId, request.BookId);

            await _uow.Save();

            return Ok();
        }
    }
}
