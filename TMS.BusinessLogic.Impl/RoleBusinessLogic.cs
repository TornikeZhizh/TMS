using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.DataAccess;
using TMS.Domain;

namespace TMS.BusinessLogic.Impl
{
    public class RoleBusinessLogic : BusinessLogicBase, IRoleBusinessLogic
    {
        private readonly IRoleDataAccess _roleDataAccess;

        public RoleBusinessLogic(IRoleDataAccess roleDataAccess)
        {
            _roleDataAccess = roleDataAccess;
        }

        public async Task<IEnumerable<RoleEntity>> GetAll()
        {
            return await _roleDataAccess.GetAll();
        }

        public async Task AddPermissionToRole(RolePermissionEntity entity)
        {
            await _roleDataAccess.AddPermissionToRole(entity);
        }

        public async Task AddRole(RoleEntity roleEntity)
        {
            await _roleDataAccess.AddRole(roleEntity);
        }

        public async Task AddRoleToUser(UserRoleEntity entity)
        {
            await _roleDataAccess.AddRoleToUser(entity);
        }

        public async Task<IEnumerable<UserRoleEntity>> GetUserRoles(long UserId)
        {
            return await _roleDataAccess.GetUserRoles(UserId);
        }

        public async Task<IEnumerable<RolePermissionEntity>> GetRolePermissions(long RoleId)
        {
            return await _roleDataAccess.GetRolePermissions(RoleId);
        }

        public async Task DeleteRole(long Id)
        {
            await _roleDataAccess.DeleteRole(Id);
        }
        
        public async Task<bool> RoleExists(long RoleId)
        {
            return await _roleDataAccess.RoleExists(RoleId);
        }

        public async Task<bool> RoleExists(string RoleName)
        {
            return await _roleDataAccess.RoleExists(RoleName);
        }

        public async Task<bool> PermissionExists(string PermissionName)
        {
            return await _roleDataAccess.PermissionExists(PermissionName);
        }

        public async Task<bool> RoleAlreadyHasPermission(long RoleId, string PermissionName)
        {
            return await _roleDataAccess.RoleAlreadyHasPermission(RoleId, PermissionName);
        }

        public async Task<bool> UserAlreadyHasRole(long UserId, long RoleId)
        {
            return await _roleDataAccess.UserAlreadyHasRole(UserId, RoleId);
        }
    }
}
