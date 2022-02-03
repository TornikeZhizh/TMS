using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.ViewModels
{
    public class RolePermissionViewModel
    {
        public string RoleName { get; set; }

        [Required]
        [MinLength(7)]
        public string PermissionName { get; set; }
    }
}
