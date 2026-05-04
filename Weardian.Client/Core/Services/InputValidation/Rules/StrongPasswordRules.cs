namespace Weardian.Client.Core.Services.InputValidation.Rules
{
    public static class StrongPasswordRules
    {
        public static void ValidatePassword(string password, InputValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                result.Errors.Add("Password input cannot be null, empty or whitespace.");
                return;    
            }

            if (password.Length < 8)
            {
                result.Errors.Add("Password must have a length of 8 or more characters.");
            }

            var hasUpper = password.Any(char.IsUpper);
            var hasLower = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);

            if (!hasUpper || !hasLower || !hasDigit)
            {
                result.Errors.Add("Password must contain at least one upper, lower and a digit.");
            }
        }
    }
}
