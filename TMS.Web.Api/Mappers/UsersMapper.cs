using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Domain;
using TMS.ViewModels;

namespace TMS.Web.Api.Mappers
{
    public static class UsersMapper
    {

        public static UserResponseViewModel ConvertUserEntityToUserResponseViewModel(UserEntity userEntity)
        {
            return new UserResponseViewModel
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName
            };
        }
    }
}
