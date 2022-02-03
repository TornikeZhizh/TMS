using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.BusinessLogic
{
    public interface ITaskBusinessLogic
    {
        Task<IEnumerable<TaskEntity>> GetAll();

        Task<TaskEntity> GetTask(long Id);

        Task AddTask(TaskEntity task);

        Task EdiTTask(TaskEntity task);

        Task DeleteTask(long Id);

        Task<IEnumerable<TaskAttachmentEntity>> GetAttachmentsForTask(long TaskId);

        Task<bool> TaskAlreadyExists(long Id);

        Task<bool> HasPermission(string UserName, string Action);

        
    }
}
