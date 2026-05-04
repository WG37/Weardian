namespace Weardian.Client.Core.Services.InputValidation.Rules
{
    public static class KeyNameRules
    {
        public static void ValidateKeyName(string keyName, InputValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                result.Errors.Add("Key name input is null, empty or whitespace.");
                return;
            }

            if (keyName.Length < 3 || keyName.Length > 12)
            {
                result.Errors.Add("KeyName must have a minimum of 3 and max of 12 characters.");
            }
        }
    }
}
