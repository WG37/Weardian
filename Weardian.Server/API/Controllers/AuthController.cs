using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Weardian.Server.Application.DTOs.Authentication;
using Weardian.Server.Application.DTOs.Login;
using Weardian.Server.Application.DTOs.Register;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _tokenService = tokenService;
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
                return Unauthorized(new AuthTokenResponseDto(
                    Token: null,
                    RefreshToken: null,
                    IsSuccessful: false,
                    Error: "Invalid email or password"));

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, req.Password, true);
            if (!signInResult.Succeeded)
                return Unauthorized(new AuthTokenResponseDto(
                    Token: null,
                    RefreshToken: null,
                    IsSuccessful: false,
                    Error: "Invalid email or password"));

            var token = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var hashedToken = _tokenService.GenerateHashForRefreshToken(refreshToken);

            await _tokenService.CreateRefreshTokenAsync(user, hashedToken);

            return Ok(new AuthTokenResponseDto(
                Token: token,
                RefreshToken: refreshToken,
                IsSuccessful: true,
                Error: null));
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshTokenAsync(RefreshTokenDto refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken.RefreshToken))
                return BadRequest("Refresh token is required");

            var hashedToken = _tokenService.GenerateHashForRefreshToken(refreshToken.RefreshToken);
            var tokenValidation = await _tokenService.ValidateRefreshTokenAsync(hashedToken);

            if (!tokenValidation.IsSuccessful)
                return Unauthorized(new AuthTokenResponseDto(
                    Token: null,
                    RefreshToken: null,
                    IsSuccessful: false,
                    Error: tokenValidation.Error));

            var user = await _userManager.FindByIdAsync(tokenValidation.UserId!);

            if (user == null)
                return Unauthorized(new AuthTokenResponseDto(
                    Token: null,
                    RefreshToken: null,
                    IsSuccessful: false,
                    Error: "User does not exist"));


        }
    }
}
