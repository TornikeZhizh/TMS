using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.ViewModels
{
    public class TaskViewModel
    {

        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        public string ShortDescrip { get; set; }
        public string LongDescrip { get; set; }

        public List<IFormFile> Attachments { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
