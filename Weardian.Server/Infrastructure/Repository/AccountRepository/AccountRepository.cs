using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Weardian.Server.Application.DTOs.Account;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Users;
using Weardian.Server.Infrastructure.Data;

namespace Weardian.Server.Infrastructure.Repository.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> UpdatePasswordAsync(ApplicationUser user)
        {
            var storedUser = await _db.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

            if (storedUser.Id != user.Id)
                return false;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
