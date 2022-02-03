using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.DataAccess.Impl
{
    public class RoleDataAccess : DataAccessBase, IRoleDataAccess
    {
        public RoleDataAccess(DataContext context) : base(context) { }
        
        public async Task<IEnumerable<RoleEntity>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }
        
        public async Task AddPermissionToRole(RolePermissionEntity entity)
        {
            await _context.RolePermissions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRole(RoleEntity roleEntity)
        {
            roleEntity.Name = roleEntity.Name.ToLower();
            await _context.Roles.AddAsync(roleEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRoleToUser(UserRoleEntity entity)
        {
            await _context.UserRoles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserRoleEntity>> GetUserRoles(long UserId)
        {
            return await _context.UserRoles.Where(x => x.UserId == UserId).ToListAsync();
        }

        public async Task<IEnumerable<RolePermissionEntity>> GetRolePermissions(long RoleId)
        {
            return await _context.RolePermissions.Where(x => x.RoleId == RoleId).ToListAsync();
        }
        
        public async Task DeleteRole(long Id)
        {
            var role = new RoleEntity { Id = Id };
            _context.Attach(role);
            _context.Remove(role);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> RoleExists(long RoleId)
        {
            return await _context.Roles.AnyAsync(x => x.Id == RoleId);
        }

        public async Task<bool> RoleExists(string RoleName)
        {
            return await _context.Roles.AnyAsync(x => x.Name == RoleName);
        }

        public async Task<bool> PermissionExists(string PermissionName)
        {
            return await _context.Permissions.AnyAsync(x => x.Name == PermissionName.ToLower());
        }

        public async Task<bool> RoleAlreadyHasPermission(long RoleId, string PermissionName)
        {
            return await _context.RolePermissions.AnyAsync(x => x.RoleId == RoleId && x.PermissionName == PermissionName);
        }

        public async Task<bool> UserAlreadyHasRole(long UserId, long RoleId)
        {
            return await _context.UserRoles.AnyAsync(x => x.RoleId == RoleId && x.UserId == UserId);
        }

    }
}
