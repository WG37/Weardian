using Weardian.Client.Core.Interfaces.InputValidation;
using Weardian.Client.Core.Services.InputValidation.Rules;

namespace Weardian.Client.Core.Services.InputValidation
{
    public class InputValidationService : IInputValidationService
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
            var results = new InputValidationResult();

            LoginEmailRules.ValidateEmailName(email, results);
            StrongPasswordRules.ValidatePassword(password, results);

            return results;
        }

        public InputValidationResult ValidateRegisterUser(string email, string username, string password)
        {
            throw new NotImplementedException();
        }

        
    }
}
