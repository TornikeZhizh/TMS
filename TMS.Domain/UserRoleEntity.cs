using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain
{
    public class UserRoleEntity
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long RoleId { get; set; }

        public UserEntity User { get; set; }
    }
}
