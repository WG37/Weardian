using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weardian.Client.Core.DTOs.MessageHandler.HandleUserAccount
{
    public sealed record UpdateUserPasswordRequest(
        string CurrentPassword,
        string NewPassword);
}
