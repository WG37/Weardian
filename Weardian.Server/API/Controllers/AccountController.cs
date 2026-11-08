using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Weardian.Server.Application.DTOs.Account;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/accounts/user/{Id}")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly IAccountService _AccService;

        public AccountController(
            UserManager<ApplicationUser> user, 
            IAccountService accService)
        {
            _user = user;
            _AccService = accService;
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UpdateUserPasswordRequestDto passwordReqDto)
        {
            if (passwordReqDto == null)
                return BadRequest();

            var result = await _AccService.UpdateUserPasswordAsync(passwordReqDto);
        }
    }
}
