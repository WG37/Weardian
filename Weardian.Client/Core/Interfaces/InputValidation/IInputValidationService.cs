using Weardian.Client.Core.Services.InputValidation;

namespace Weardian.Client.Core.Interfaces.InputValidation
{
    public interface IInputValidationService
    {
        public InputValidationResult ValidateEncryptedPassword(string keyName, string password);

        public InputValidationResult ValidateRegisterUser(string email, string password);
        public InputValidationResult ValidateLogin(string email, string password);
    }
}
