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
        public DbSet<ToDoList.Models.ChatMessage> ChatMessages { get; set; }
        public DbSet<ToDoList.Models.ChatGroup> ChatGroup { get; set; } = default!;
        public DbSet<ToDoList.Models.GroupUser> GroupUser { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define composite key for GroupUser
            builder.Entity<GroupUser>()
                .HasKey(gu => new { gu.UserId, gu.ChatGroupId });

            // Relationships
            builder.Entity<GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.GroupUsers)
                .HasForeignKey(gu => gu.UserId);

            builder.Entity<GroupUser>()
                .HasOne(gu => gu.ChatGroup)
                .WithMany(g => g.GroupUsers)
                .HasForeignKey(gu => gu.ChatGroupId);

        }









    }
}
