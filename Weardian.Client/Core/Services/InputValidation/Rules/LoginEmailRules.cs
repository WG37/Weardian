using System.Net.Mail;

namespace Weardian.Client.Core.Services.InputValidation.Rules
{
    public static class LoginEmailRules
    {
        public static void ValidateEmailName(string email, InputValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                result.Errors.Add("Invalid input: Email cannot be null, empty or whitespace");
                return;
            }

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                result.Errors.Add("Email is invalid: Incorrect format");
            }
        
            
        }
    }
}
