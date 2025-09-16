using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<ToDoList.Models.ToDoTask> ToDoTask { get; set; } = default!;
        public DbSet<ToDoList.Models.ChatMessage> ChatMessages { get; set; }
    }
}
