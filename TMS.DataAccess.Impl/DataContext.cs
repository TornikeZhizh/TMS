using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.DataAccess.Impl
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TaskEntity> Tasks { get; set; }

        public DbSet<TaskAttachmentEntity> TaskAttachments { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<PermissionEntity> Permissions { get; set; }

        public DbSet<UserRoleEntity> UserRoles { get; set; }

        public DbSet<RolePermissionEntity> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>(entity => {
                entity.HasIndex(e => e.UserName).IsUnique();
            });
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<TaskEntity>().ToTable("Tasks", "dbo");
        //    builder.Entity<TaskEntity>().ToTable("Users", "dbo");
        //}
    }
}
