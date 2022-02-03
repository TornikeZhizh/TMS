using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Domain;
using TMS.ViewModels;

namespace TMS.Web.Api.Mappers
{
    public static class RolesMapper
    {
        public static  IEnumerable<RoleViewModel> ConvertRoleEntityToRoleViewModel(IEnumerable<RoleEntity> roles)
        {
            return roles.Select(x => new RoleViewModel { 
                Id = x.Id,
                RoleName = x.Name
            }).ToList();
        }

        public static IEnumerable<RolePermissionViewModel> ConvertRolePermissionEntityToRolePermissionViewModel(IEnumerable<RolePermissionEntity> rolePermissions, IEnumerable<RoleEntity> roles)
        {
            return  rolePermissions.Select(x => new RolePermissionViewModel
            {
                //RoleId = x.RoleId,
                PermissionName = x.PermissionName,
                RoleName = roles.FirstOrDefault(k => k.Id == x.RoleId).Name
            }).ToList();
        }

        public static IEnumerable<RoleViewModel> ConvertUserRoleEntityToRolesViewModel(IEnumerable<UserRoleEntity> userRoles, IEnumerable<RoleEntity> roles)
        {
            return userRoles.Select(x => new RoleViewModel
            {
                Id = x.RoleId,
                RoleName = roles.FirstOrDefault(i => i.Id == x.RoleId).Name
            }).ToList();
        }
    }
}
