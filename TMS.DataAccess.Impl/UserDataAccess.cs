using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.DataAccess.Impl
{
    public class UserDataAccess : DataAccessBase, IUserDataAccess
    {
        public UserDataAccess(DataContext context) : base(context) { }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUser(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUser(string UserName)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == UserName);
        }

        public async Task<UserEntity> GetUserById(long UserId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == UserId);
        }

        public async Task DeleteUser(long UserId)
        {
            var user = new UserEntity { Id = UserId };
            _context.Attach(user);
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(long UserId)
        {
            return await _context.Users.AnyAsync(x => x.Id == UserId);
        }

        public async Task<bool> UserExists(string UserName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == UserName);
        }
    }
}
