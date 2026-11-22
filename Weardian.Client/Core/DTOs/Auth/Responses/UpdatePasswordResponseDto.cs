using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weardian.Client.Core.DTOs.Auth.Responses
{
    public sealed record UpdatePasswordResponseDto(
        bool IsSuccessful,
        string? Error);
}
