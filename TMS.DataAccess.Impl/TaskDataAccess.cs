using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.DataAccess.Impl
{
    public class TaskDataAccess : DataAccessBase, ITaskDataAccess
    {
        public TaskDataAccess(DataContext context) : base(context) { }

        public async Task<IEnumerable<TaskEntity>> GetAll()
        {
            var tasks = await _context.Tasks.Include(t => t.TaskAttachments).ToListAsync();  
            return tasks;
        }

        public async Task<TaskEntity> GetTask(long Id)
        {
            var task = await _context.Tasks.Include(t => t.TaskAttachments).SingleOrDefaultAsync(x => x.Id == Id);
            return task;
        }

        public async Task AddTask(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task EdiTTask(TaskEntity task)
        {
            var existing = await GetTask(task.Id);

            existing.ShortDescription = task.ShortDescription;
            existing.LongDescription = task.LongDescription;
            existing.Title = task.Title;
            existing.UserId = task.UserId;
            existing.TaskAttachments = task.TaskAttachments;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(long Id)
        {
            var task = new TaskEntity { Id = Id };
            _context.Attach(task);
            _context.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskAttachmentEntity>> GetAttachmentsForTask(long TaskId)
        {
            return await _context.TaskAttachments.Where(x => x.TaskId == TaskId).ToListAsync();
           
        }

        public async Task<bool> TaskAlreadyExists(long Id)
        {
            return await _context.Tasks.AnyAsync(x => x.Id == Id);
        }

        public async Task<bool> HasPermission(string UserName, string Action)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == UserName);

            if (UserName == "admin")
                return true;

            var userRoles = await _context.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
            foreach (var userRole in userRoles)
            {
                var roleId = userRole.RoleId;
                var rolePermissions = await _context.RolePermissions.Where(x => x.RoleId == roleId).ToListAsync();
                if (rolePermissions.Any(x => x.PermissionName == Action))
                    return true;
            }
            return false;
        }

        
    }
}
