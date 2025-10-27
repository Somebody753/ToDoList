using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<ToDoList.Models.ToDoTask> ToDoTask { get; set; } = default!;
        public DbSet<ToDoList.Models.ChatMessage> ChatMessages { get; set; } = default!;
        public DbSet<ToDoList.Models.ChatGroup> ChatGroup { get; set; } = default!;
        public DbSet<ToDoList.Models.GroupUser> GroupUser { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //GroupUser Relations
            //Create PK from 2 FK
            builder.Entity<GroupUser>()
                .HasKey(gu => new { gu.UserId, gu.ChatGroupId });

            
            builder.Entity<GroupUser>()
                .HasOne(u => u.User)
                .WithMany(gu => gu.GroupUsers)
                .HasForeignKey(u => u.UserId);

            builder.Entity<GroupUser>()
                .HasOne(cg => cg.ChatGroup)
                .WithMany(gu => gu.GroupUsers)
                .HasForeignKey(cg => cg.ChatGroupId);

            //ChatMessage Relations

            builder.Entity<ChatMessage>()
                .HasOne(a => a.Author)
                .WithMany(cm => cm.ChatMessages)
                .HasForeignKey(a => a.AuthorId);

            builder.Entity<ChatMessage>()
                .HasOne(g => g.Group)
                .WithMany(gm => gm.ChatMessages)
                .HasForeignKey(g => g.GroupId);

            //ToDoTask Relations
            builder.Entity<ToDoTask>()
                .HasOne(cg => cg.ChatGroup)
                .WithMany(td => td.ToDoTasks)
                .HasForeignKey(cg => cg.ChatGroupId);

        }









    }
}
