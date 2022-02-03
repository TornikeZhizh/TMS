using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.BusinessLogic
{
    public interface IUserBusinessLogic
    {
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> GetUser(string UserName);

        Task<UserEntity> GetUserById(long UserId);

        Task DeleteUser(long UserId);
        Task<bool> UserExists(long UserId);

        Task<bool> UserExists(string UserName);

        Task AddUser(UserEntity user);
    }
}
