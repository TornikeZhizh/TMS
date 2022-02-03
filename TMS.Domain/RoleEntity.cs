using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain
{
    public class RoleEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<RolePermissionEntity> RolePermissions { get; set; }
    }
}
