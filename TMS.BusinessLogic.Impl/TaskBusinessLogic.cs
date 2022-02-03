using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.DataAccess;
using TMS.Domain;

namespace TMS.BusinessLogic.Impl
{
    public class TaskBusinessLogic : BusinessLogicBase, ITaskBusinessLogic
    {
        private readonly ITaskDataAccess _taskDataAccess;

        public TaskBusinessLogic(ITaskDataAccess taskDataAccess)
        {
            _taskDataAccess = taskDataAccess;
        }

        public async Task<IEnumerable<TaskEntity>> GetAll()
        {
            return await _taskDataAccess.GetAll();
        }

        public async Task<TaskEntity> GetTask(long Id)
        {
            return await _taskDataAccess.GetTask(Id);
        }

        public async Task AddTask(TaskEntity task)
        {
            await _taskDataAccess.AddTask(task);
        }

        public async Task EdiTTask(TaskEntity task)
        {
            await _taskDataAccess.EdiTTask(task);
        }

        public async Task DeleteTask(long Id)
        {
            await _taskDataAccess.DeleteTask(Id);
        }

        public async Task<IEnumerable<TaskAttachmentEntity>> GetAttachmentsForTask(long TaskId)
        {
            return await _taskDataAccess.GetAttachmentsForTask(TaskId);

        }

        public async Task<bool> TaskAlreadyExists(long Id)
        {
            return await _taskDataAccess.TaskAlreadyExists(Id);
        }

        public async Task<bool> HasPermission(string UserName, string Action)
        {
            return await _taskDataAccess.HasPermission(UserName, Action);
        }

    }
}
