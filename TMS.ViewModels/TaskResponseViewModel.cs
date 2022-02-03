using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.ViewModels
{
    public class TaskResponseViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string ShortDescrip { get; set; }
        public string LongDescrip { get; set; }

        public List<string> Attachments { get; set; }

        
        public string UserName { get; set; }
    }
}
