using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Weardian.Server.Application.DTOs.Login;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(ITokenService service,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto req)
        {
            var user = new ApplicationUser
            {
                UserName = req.Email,
                Email = req.Email
            };

            var result = await _userManager.CreateAsync(user, req.Password);
            if (!result.Succeeded)
                return BadRequest(new RegistrationResponseDto(
                    IsSuccessful: false,
                    Error: string.Join(",", result.Errors.Select(e => e.Description))));

            return Ok(new RegistrationResponseDto(
                IsSuccessful: true,
                Error: null));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto req)
        {
            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
                return Unauthorized( new AuthTokenResponseDto(
                    Token: null,
                    IsSuccessful: false,
                    Error: "Invalid email or password"));

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, req.Password, true);
            if (!signInResult.Succeeded)
                return Unauthorized(new AuthTokenResponseDto(
                    Token: null,
                    IsSuccessful: false,
                    Error: "Invalid email or password"));

            var token = _service.GenerateAccessToken(user);

            return Ok(new AuthTokenResponseDto(
                Token: token,
                IsSuccessful: true,
                Error: null));
        }
    }
}
