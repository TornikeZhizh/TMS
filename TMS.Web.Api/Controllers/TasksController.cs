using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TMS.BusinessLogic;
using TMS.Commons;
using TMS.Domain;
using TMS.ViewModels;
using TMS.Web.Api.Mappers;

namespace TMS.Web.Api.Controllers
{
    
    public class TasksController : BaseController
    {
        private readonly ITaskBusinessLogic _taskBusinessLogic;
        private readonly IUserBusinessLogic _userBusinessLogic;

        public TasksController(
            ITaskBusinessLogic taskBusinessLogic,
            IOptions<AppSettings> appSettings,
            IUserBusinessLogic userBusinessLogic) : base(appSettings)
        {
            _taskBusinessLogic = taskBusinessLogic;
            _userBusinessLogic = userBusinessLogic;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allTask = await _taskBusinessLogic.GetAll();

            if (allTask != null && allTask.ToList().Count > 0)
            {
                var res = new List<TaskResponseViewModel>();
                foreach(var task in allTask)
                {
                    var user = await _userBusinessLogic.GetUserById(task.UserId);

                    var taskModel = TasksMapper.ConvertTaskEntityToTaskResponseViewModel(task, user?.UserName, _appSettings.AttachmentsLocation);

                    if (taskModel.Attachments != null && taskModel.Attachments.Count > 0)
                        taskModel.Attachments = createActionLinks(taskModel.Attachments);

                    res.Add(taskModel);
                }
                return Ok(res);
            }
            return Ok(allTask);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetTask")]
        public async Task<IActionResult> GetTask(long Id)
        {
            if (!await hasPermission(UserName, "task_view"))
                return BadRequest("You Don't have permission to View tasks!");

            if (!await _taskBusinessLogic.TaskAlreadyExists(Id))
                return BadRequest($"Task with Id: {Id} Does Not Exists!");

            var taskEntity = await _taskBusinessLogic.GetTask(Id);

            var user = await _userBusinessLogic.GetUserById(taskEntity.UserId);

            var task = TasksMapper.ConvertTaskEntityToTaskResponseViewModel(taskEntity, user?.UserName, _appSettings.AttachmentsLocation);
            if (task.Attachments != null && task.Attachments.Count > 0)
                task.Attachments = createActionLinks(task.Attachments);
            return Ok(task);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTask([FromForm]TaskViewModel task)
        {
            if (!await hasPermission(UserName, "task_add"))
                return BadRequest("You Don't have permission to Add tasks!");

            if (!await _userBusinessLogic.UserExists(task.UserId))
                return BadRequest("This User Does Not Exists!");

            var attachments = task.Attachments?.Count > 0 ?
                await FilesHelper.SaveAsync(task.Attachments, _appSettings.AttachmentsLocation) : null;

            var taskEntity = TasksMapper.ConvertTaskViewModelToTaskEntity(task, attachments);

            await _taskBusinessLogic.AddTask(taskEntity);

            var user = await _userBusinessLogic.GetUserById(taskEntity.UserId);

            var taskResponse = TasksMapper.ConvertTaskEntityToTaskResponseViewModel(taskEntity, user.UserName, _appSettings.AttachmentsLocation);

            return CreatedAtRoute("GetTask", new { Id = taskEntity.Id }, taskResponse);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditTask([FromForm]TaskViewModel task)
        {
            if (!await hasPermission(UserName, "task_edit"))
                return BadRequest("You Don't have permission to Edit tasks!");

            if (!await _taskBusinessLogic.TaskAlreadyExists(task.Id))
                return BadRequest("Task Does Not Exists!");

            if (!await _userBusinessLogic.UserExists(task.UserId))
                return BadRequest("This User Does Not Exists!");

            var taskAttachments = await _taskBusinessLogic.GetAttachmentsForTask(task.Id);
            var newAttachments = new List<IFormFile>();

            if (task.Attachments != null)
            {
                foreach (var file in task.Attachments)
                {
                    if (taskAttachments.Any(x => x.FileName == file.FileName))
                        continue;
                    newAttachments.Add(file);
                }
            }

            IEnumerable<(string uid, string filename)> lst = null;

            if (newAttachments.Count > 0)
                lst = await FilesHelper.SaveAsync(newAttachments, _appSettings.AttachmentsLocation);
            
            var l = lst != null? lst.ToList() : new List<(string, string)>();

            foreach(var file in taskAttachments)
            {
                if(task.Attachments != null && task.Attachments.Any(x => x.FileName == file.FileName))
                {
                    l.Add((file.FileUid, file.FileName));
                }
            }

            var taskEntity = TasksMapper.ConvertTaskViewModelToTaskEntity(task, l);

            await _taskBusinessLogic.EdiTTask(taskEntity);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(long Id)
        {
            if (!await hasPermission(UserName, "task_delete"))
                return BadRequest("You Don't have permission to Delete tasks!");
            if (! await _taskBusinessLogic.TaskAlreadyExists(Id))
                return BadRequest($"Task with Id : {Id} Does Not Exists!");
            await _taskBusinessLogic.DeleteTask(Id);
            return Ok();
        }

        private async Task<bool> hasPermission(string UserName, string Permission)
        {
            return await _taskBusinessLogic.HasPermission(UserName, Permission);
        }

        private List<string> createActionLinks(List<string> attachments)
        {
            return attachments?.Select(t => t = Url.ActionLink("Get", "Assets", new { uid = t })).ToList();
        }
    }
}
