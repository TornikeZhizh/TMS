using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.BusinessLogic;
using TMS.Commons;
using TMS.Domain;
using TMS.ViewModels;
using TMS.Web.Api.Mappers;

namespace TMS.Web.Api.Controllers
{
    
    public class RolesController : BaseController
    {
        private readonly IRoleBusinessLogic _roleBusinessLogic;
        private readonly IUserBusinessLogic _userBusinessLogic;


        public RolesController(
            IRoleBusinessLogic roleBusinessLogic,
            IUserBusinessLogic userBusinessLogic,
            IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _roleBusinessLogic = roleBusinessLogic;
            _userBusinessLogic = userBusinessLogic;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _roleBusinessLogic.GetAll();
            return Ok(RolesMapper.ConvertRoleEntityToRoleViewModel(list));
        }

        [Authorize]
        [HttpGet("~/api/user/{userId}/roles", Name = "GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(long userId)
        {
            var userRoles = await _roleBusinessLogic.GetUserRoles(userId);
            var roles = await _roleBusinessLogic.GetAll();
            var result = RolesMapper.ConvertUserRoleEntityToRolesViewModel(userRoles, roles);
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}/permissions", Name = "GetRolePermissions")]
        public async Task<IActionResult> GetRolePermissions(long Id)
        {
            var rolePermissions = await _roleBusinessLogic.GetRolePermissions(Id);
            var roles = await _roleBusinessLogic.GetAll();
            var result = RolesMapper.ConvertRolePermissionEntityToRolePermissionViewModel(rolePermissions, roles);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (!IsAdmin)
                return BadRequest(Constants.NOT_ADMIN);

            if (await _roleBusinessLogic.RoleExists(model.RoleName))
                return BadRequest($"This role : {model.RoleName} already exists!");

            var roleEntity = new RoleEntity
            {
                Name = model.RoleName
            };
            await _roleBusinessLogic.AddRole(roleEntity);
            model.Id = roleEntity.Id;
            return CreatedAtRoute("", new { }, model);
        }

        [Authorize]
        [HttpPost("{id}/permission")]
        public async Task<IActionResult> AddPermissionToRole(long id, RolePermissionViewModel model)
        {
            if (!IsAdmin)
                return BadRequest(Constants.NOT_ADMIN);

            if (! await _roleBusinessLogic.RoleExists(id))
                return BadRequest($"Role with Id : {id} does not Exists");

            if (! await _roleBusinessLogic.PermissionExists(model.PermissionName))
                return BadRequest($"Permission : {model.PermissionName} does not Exists");

            if (await _roleBusinessLogic.RoleAlreadyHasPermission(id, model.PermissionName))
                return BadRequest("This role already has this permission!");

            var rolePermissionEntity = new RolePermissionEntity
            {
                RoleId = id,
                PermissionName = model.PermissionName
            };

            await _roleBusinessLogic.AddPermissionToRole(rolePermissionEntity);
            return CreatedAtRoute("GetRolePermissions", new { id = rolePermissionEntity.RoleId }, rolePermissionEntity);
        }

        [Authorize]
        [HttpPost("~/api/user/{userId}/roles")]
        public async Task<IActionResult> AddRoleToUser(long userId, UserRoleViewModel model)
        {
            if (!IsAdmin)
                return BadRequest(Constants.NOT_ADMIN);

            if (! await _roleBusinessLogic.RoleExists(model.RoleId))
                return BadRequest($"Role with Id : {model.RoleId} does not Exists");

            if(! await _userBusinessLogic.UserExists(userId))
                return BadRequest($"User with Id : {userId} does not Exists");

            if (await _roleBusinessLogic.UserAlreadyHasRole(userId, model.RoleId))
                return BadRequest($"User with Id : {userId} already has Role with Id : {model.RoleId}");


            var userRoleEntity = new UserRoleEntity
            {
                RoleId = model.RoleId,
                UserId = userId
            };

            await _roleBusinessLogic.AddRoleToUser(userRoleEntity);
            return CreatedAtRoute("GetUserRoles", new { userId = userRoleEntity.UserId }, userRoleEntity);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(long Id)
        {
            if (!IsAdmin)
                return BadRequest(Constants.NOT_ADMIN);

            if (! await _roleBusinessLogic.RoleExists(Id))
                return BadRequest($"Role with Id : {Id} does not Exists");

            await _roleBusinessLogic.DeleteRole(Id);
            return Ok();
        }
    }
}
