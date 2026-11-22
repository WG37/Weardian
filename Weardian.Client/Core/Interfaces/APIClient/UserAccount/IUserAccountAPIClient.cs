using Weardian.Client.Core.DTOs.Auth.Requests;
using Weardian.Client.Core.DTOs.Auth.Responses;

namespace Weardian.Client.Core.Interfaces.APIClient.UserAccount
{
    public interface IUserAccountAPIClient
    {
        public Task<UpdatePasswordResponseDto> SendUpdatePasswordRequestAsync(UpdatePasswordRequestDto updatePasswordRequestDto);
    }
}
