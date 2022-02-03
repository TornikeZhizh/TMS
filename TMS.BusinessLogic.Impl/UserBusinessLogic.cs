using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.DataAccess;
using TMS.Domain;

namespace TMS.BusinessLogic.Impl
{
    public class UserBusinessLogic : BusinessLogicBase, IUserBusinessLogic
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserBusinessLogic(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _userDataAccess.GetAll();
        }
        public async Task AddUser(UserEntity user)
        {
            await _userDataAccess.AddUser(user);
        }

        public async Task<UserEntity> GetUser(string UserName)
        {
            return await _userDataAccess.GetUser(UserName);
        }

        public async Task<UserEntity> GetUserById(long UserId)
        {
            return await _userDataAccess.GetUserById(UserId);
        }

        public async Task DeleteUser(long UserId)
        {
            await _userDataAccess.DeleteUser(UserId);
        }

        public async Task<bool> UserExists(long UserId)
        {
            return await _userDataAccess.UserExists(UserId);
        }

        public async Task<bool> UserExists(string UserName)
        {
            return await _userDataAccess.UserExists(UserName);
        }
    }
}
