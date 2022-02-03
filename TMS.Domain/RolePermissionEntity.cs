using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain
{
    public class RolePermissionEntity
    {
        public long Id { get; set; }

        public long RoleId { get; set; }

        public string PermissionName { get; set; }

        public RoleEntity Role { get; set; }
    }
}
