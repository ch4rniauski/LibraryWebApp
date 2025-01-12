using Application.Abstractions.Records;
using Application.Abstractions.Services;
using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<UserInfoResponse>> GetUserInfo(Guid id)
        {
            var user = await _userService.GetUserInfo(id);

            return Ok(user);
        }

        [HttpPost("borrow")]
        [Authorize]
        public async Task<ActionResult> BorrowBook([FromBody] BorrowBookRequest request)
        {
            await _userService.BorrowBook(request.UserId, request.BookId);

            return Ok();
        }
    }
}
