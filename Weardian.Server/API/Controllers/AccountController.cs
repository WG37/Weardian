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
        private readonly IUserAccountService _accService;

        public AccountController(
            IUserAccountService accService)
        {
            _accService = accService;
        }

        [HttpPatch("password")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UpdateUserPasswordRequestDto passwordReqDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (passwordReqDto == null)
                return BadRequest("Request cannot be null");

            var response = await _accService.UpdateUserPasswordAsync(userId, passwordReqDto);

            if (!response.IsSuccessful)
                return Unauthorized(new UserAccountResponseDto(
                    IsSuccessful: false,
                    Error: response.Error));

            return Ok(new UserAccountResponseDto(
                IsSuccessful: true,
                Error: null));
        }
    }
}
