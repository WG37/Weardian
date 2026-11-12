using Weardian.Server.Application.DTOs.Account;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Application.Interfaces
{
    public interface IUserAccountService
    {
        public Task<UserAccountStatusResponseDto> UpdateUserPasswordAsync(string userId, UpdateUserPasswordRequestDto passwordReqDto);
    }
}
