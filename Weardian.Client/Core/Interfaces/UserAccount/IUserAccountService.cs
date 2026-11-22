using Weardian.Client.Core.DTOs.Auth.Responses;

namespace Weardian.Client.Core.Interfaces.UserAccount
{
    public interface IUserAccountService
    {
        public Task<UpdatePasswordResponseDto> UpdateUserAccountPasswordAsync(string oldPassword, string newPassword);
    }
}
