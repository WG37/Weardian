using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weardian.Client.Core.DTOs.Auth.Requests
{
    public sealed record UpdatePasswordRequestDto(
        string OldPassword,
        string NewPassword);
}
