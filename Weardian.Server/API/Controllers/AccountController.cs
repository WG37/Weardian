using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Weardian.Server.Application.DTOs.Account;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/accounts/user")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _AccService;

        public AccountController(
            IAccountService accService)
        {
            _AccService = accService;
        }
        [HttpPatch("password")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UpdateUserPasswordRequestDto passwordReqDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (passwordReqDto == null)
                return BadRequest();

            var response = await _AccService.UpdateUserPasswordAsync(userId, passwordReqDto);

        }
    }
}
