using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.UserUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IBorrowBookUseCase _borrowBookUseCase;
        private readonly IGetUserInfoUseCase _getUserInfoUseCase;

        public UserController(IBorrowBookUseCase borrowBookUseCase,
            IGetUserInfoUseCase getUserInfoUseCase)
        {
            _borrowBookUseCase = borrowBookUseCase;
            _getUserInfoUseCase = getUserInfoUseCase;
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<UserInfoResponse>> GetUserInfo(Guid id)
        {
            var user = await _getUserInfoUseCase.Execute(id);

            return Ok(user);
        }

        [HttpPost("borrow")]
        [Authorize]
        public async Task<ActionResult> BorrowBook([FromBody] BorrowBookRequest request)
        {
            await _borrowBookUseCase.Execute(request.UserId, request.BookId);

            return Ok();
        }
    }
}
