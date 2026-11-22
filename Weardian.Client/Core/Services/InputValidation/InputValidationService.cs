using Weardian.Client.Core.Interfaces.InputValidation;
using Weardian.Client.Core.Services.InputValidation.Rules;

namespace Weardian.Client.Core.Services.InputValidation
{
    public sealed class InputValidationService : IInputValidationService
    {
        public InputValidationResult ValidateEncryptedPassword(string keyName, string password)
        {
            var result = new InputValidationResult();

            KeyNameRules.ValidateKeyName(keyName, result);
            StrongPasswordRules.ValidatePassword(password, result);

            return result;
        }

        public InputValidationResult ValidateLogin(string email, string password)
        {
            var result = new InputValidationResult();

            LoginEmailRules.ValidateEmailName(email, result);
            StrongPasswordRules.ValidatePassword(password, result);

            return result;
        }

        public InputValidationResult ValidateRegisterUser(string email, string password)
        {
            var result = new InputValidationResult();

            LoginEmailRules.ValidateEmailName(email, result);
            StrongPasswordRules.ValidatePassword(password, result);

            return result;
        }

        public InputValidationResult ValidatePasswordChange(string oldPassword, string newPassword)
        {
            var result = new InputValidationResult();

            StrongPasswordRules.ValidatePassword(newPassword, result);
            PasswordsMustDifferRule.ValidatePasswords(oldPassword, newPassword, result);

            return result;
        }
    }
}
