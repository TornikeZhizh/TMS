using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain
{
    public class TaskEntity
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public IEnumerable<TaskAttachmentEntity> TaskAttachments { get; set; }

        public long UserId { get; set; }
    }
}
