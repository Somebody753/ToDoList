using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();


    }
}
