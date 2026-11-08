using Microsoft.AspNetCore.Identity;
using Weardian.Server.Application.DTOs.Account;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Application.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _user;

        public AccountService(
            IAccountRepository accRepository,
            UserManager<ApplicationUser> user)
        {
            _accountRepository = accRepository;
            _user = user;
        }

        public async Task<AccountStatusResponseDto> UpdateUserPasswordAsync(UpdateUserPasswordRequestDto passwordReqDto)
        {
            
        }
    }
}
