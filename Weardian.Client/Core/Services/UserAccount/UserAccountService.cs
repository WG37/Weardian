using Weardian.Client.Core.DTOs.Auth.Requests;
using Weardian.Client.Core.DTOs.Auth.Responses;
using Weardian.Client.Core.Interfaces.APIClient.UserAccount;
using Weardian.Client.Core.Interfaces.InputValidation;
using Weardian.Client.Core.Interfaces.UserAccount;

namespace Weardian.Client.Core.Services.UserAccount
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IInputValidationService _inputValidation;
        private readonly IUserAccountAPIClient _userAccountAPIClient;
        public UserAccountService(
            IInputValidationService inputValidation, 
            IUserAccountAPIClient userAccountAPIClient)
        {
            _inputValidation = inputValidation;
            _userAccountAPIClient = userAccountAPIClient;
        }
        public async Task<UpdatePasswordResponseDto> UpdateUserAccountPasswordAsync(string oldPassword, string newPassword)
        {
            var results = _inputValidation.ValidatePasswordChange(oldPassword, newPassword);

            if (!results.IsValid)
                throw new InvalidOperationException(string.Join('\n', results.Errors));

            var updatePasswordRequestDto = new UpdatePasswordRequestDto(
                OldPassword: oldPassword,
                NewPassword: newPassword);

            var response = await _userAccountAPIClient.SendUpdatePasswordRequestAsync(updatePasswordRequestDto);

            return response;
        }
    }
}
