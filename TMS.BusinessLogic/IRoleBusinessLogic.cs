using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.BusinessLogic
{
    public interface IRoleBusinessLogic
    {
        Task<IEnumerable<RoleEntity>> GetAll();
        Task AddRole(RoleEntity roleEntity);

        Task AddPermissionToRole(RolePermissionEntity entity);

        Task AddRoleToUser(UserRoleEntity entity);

        Task<IEnumerable<UserRoleEntity>> GetUserRoles(long UserId);

        Task<IEnumerable<RolePermissionEntity>> GetRolePermissions(long RoleId);

        Task DeleteRole(long Id);

        Task<bool> RoleExists(long RoleId);

        Task<bool> RoleExists(string RoleName);
        Task<bool> PermissionExists(string PermissionName);

        Task<bool> RoleAlreadyHasPermission(long RoleId, string PermissionName);

        Task<bool> UserAlreadyHasRole(long UserId, long RoleId);
    }
}
