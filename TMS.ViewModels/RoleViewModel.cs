using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.ViewModels
{
    public class RoleViewModel
    {
        public long Id { get; set; }

        [Required]
        [MinLength(4)]
        public string RoleName { get; set; }
    }
}
