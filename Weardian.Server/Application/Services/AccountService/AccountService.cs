using Weardian.Server.Application.DTOs.Account;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Application.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accRepository)
        {
            _accountRepository = accRepository;
        }

        public async Task<AccountStatusResponseDto> UpdateUserPasswordAsync(UpdateUserPasswordRequestDto passwordReqDto)
        {
            
        }
    }
}
