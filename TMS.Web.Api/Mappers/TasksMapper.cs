using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMS.Domain;
using TMS.ViewModels;

namespace TMS.Web.Api.Mappers
{
    public static class TasksMapper
    {
        public static TaskResponseViewModel ConvertTaskEntityToTaskResponseViewModel(TaskEntity taskEntity, string UserName, string FolderPath)
        {
            return new TaskResponseViewModel
            {
                Id = taskEntity.Id,
                Title = taskEntity.Title,
                ShortDescrip = taskEntity.ShortDescription,
                LongDescrip = taskEntity.LongDescription,
                UserName = UserName,
                Attachments = taskEntity.TaskAttachments?.Select(x => x.FileUid).ToList()
            };
        }


        public static TaskEntity ConvertTaskViewModelToTaskEntity(TaskViewModel taskViewModel, IEnumerable<(string uid, string filename)> attachments)
        {
            return new TaskEntity
            {
                Id = taskViewModel.Id,
                Title = taskViewModel.Title,
                ShortDescription = taskViewModel.ShortDescrip,
                LongDescription = taskViewModel.LongDescrip,
                UserId = taskViewModel.UserId,
                TaskAttachments = attachments?.Select(a => new TaskAttachmentEntity
                {
                    TaskId = taskViewModel.Id,
                    FileName = a.filename,
                    FileUid = a.uid
                }).ToList()
            };
        }
    }
}
