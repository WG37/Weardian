using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weardian.Client.Core.Services.InputValidation.Rules
{
    public static class PasswordsMustDifferRule
    {
        public static void ValidatePasswords(
            string oldPassword, 
            string newPassword, 
            InputValidationResult results)
        {
            if (string.Equals(oldPassword, newPassword))
            {
                results.Errors.Add("New password must be unique");
            }
        }
    }
}
