using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.ViewModels
{
    public class UserRoleViewModel
    {
        [Required]
        public long RoleId { get; set; }
    }
}
