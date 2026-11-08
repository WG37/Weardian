using Weardian.Server.Application.DTOs.Account;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Application.Interfaces
{
    public interface IAccountRepository
    {
        public Task<bool> UpdatePasswordAsync(ApplicationUser user);
    }
}
