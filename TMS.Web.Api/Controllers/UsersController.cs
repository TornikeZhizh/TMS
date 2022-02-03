using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessLogic;
using TMS.Commons;
using TMS.Domain;
using TMS.ViewModels;
using TMS.Web.Api.Mappers;

namespace TMS.Web.Api.Controllers
{
    
    
    public class UsersController : BaseController
    {
        private readonly IUserBusinessLogic _userBusinessLogic;
        private readonly ITokenService _tokenService;

        public UsersController(
            IUserBusinessLogic userBusinessLogic, 
            ITokenService tokenService, 
            IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _userBusinessLogic = userBusinessLogic;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!IsAdmin)
                return BadRequest("Only Admin can see Users");

            var users = await _userBusinessLogic.GetAll();

            var usersViewModel = new List<UserResponseViewModel>();
            foreach(var user in users)
            {
                usersViewModel.Add(UsersMapper.ConvertUserEntityToUserResponseViewModel(user));
            }

            return Ok(usersViewModel);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long Id)
        {
            if (!IsAdmin)
                return BadRequest("Only Admin can see User");
            if (!await _userBusinessLogic.UserExists(Id))
                return BadRequest($"User with Id : {Id} Does Not Exists!");
            
            var userEntity = await _userBusinessLogic.GetUserById(Id);
            var userViewModel = UsersMapper.ConvertUserEntityToUserResponseViewModel(userEntity);
            return Ok(userViewModel);
        }


        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserLoginViewModel userViewModel)
        {
            if(!IsAdmin)
                return BadRequest("Only Admin can register User");

            if (await UserExists(userViewModel.UserName))
                return BadRequest("This UserName already exists");

            using var hmac = new HMACSHA512();
            var user = new UserEntity
            {
                UserName = userViewModel.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userViewModel.Password)),
                PasswordSalt = hmac.Key
            };

            await _userBusinessLogic.AddUser(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            var user = await _userBusinessLogic.GetUser(userLoginViewModel.UserName);

            if (user == null)
                return Unauthorized("Invalid userName");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginViewModel.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }

            return Ok(new UserViewModel
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long Id)
        {
            if (!IsAdmin)
                return BadRequest("Only Admin can Delete User");

            if (!await _userBusinessLogic.UserExists(Id))
                return BadRequest($"User with Id : {Id} Does Not Exists!");
            await _userBusinessLogic.DeleteUser(Id);
            return Ok();
        }

        private async Task<bool> UserExists(string userName)
        {
            return await _userBusinessLogic.UserExists(userName);
        }
    }
}
