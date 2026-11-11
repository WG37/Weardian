using Microsoft.AspNetCore.Identity;
using Weardian.Server.Application.DTOs.Account;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Application.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly ITokenService _tokenService;
        public AccountService(
            UserManager<ApplicationUser> user, 
            ITokenService tokenService)
        {
            _user = user;
            _tokenService = tokenService;
        }

        public async Task<AccountStatusResponseDto> UpdateUserPasswordAsync(
            string userId, 
            UpdateUserPasswordRequestDto passwordReqDto)
        {
            if (passwordReqDto == null)
                return new AccountStatusResponseDto(
                    IsSuccessful: false,
                    Error: "UpdatePassword request cannot be null",
                    ChangedOn: null);

            var user = await _user.FindByIdAsync(userId);
            if (user == null)
                return new AccountStatusResponseDto(
                    IsSuccessful: false,
                    Error: "User does not exist",
                    ChangedOn: null);

            var result = await _user.ChangePasswordAsync(user, passwordReqDto.CurrentPassword, passwordReqDto.NewPassword);
            if (!result.Succeeded)
                return new AccountStatusResponseDto(
                    IsSuccessful: false,
                    Error: string.Join('\n', result.Errors.Select(e => e.Description)),
                    ChangedOn: null);

            // need to handle if revoke method fails
            await _tokenService.RevokeUserTokensAsync(user);

            return new AccountStatusResponseDto(
                IsSuccessful: true,
                Error: null,
                ChangedOn: DateTime.UtcNow);
        }
    }
}
