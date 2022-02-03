using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.DataAccess
{
    public interface IUserDataAccess
    {
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> GetUser(string UserName);

        Task<UserEntity> GetUserById(long UserId);

        Task AddUser(UserEntity user);

        Task DeleteUser(long UserId);

        Task<bool> UserExists(long UserId);

        Task<bool> UserExists(string UserName);
    }
}
