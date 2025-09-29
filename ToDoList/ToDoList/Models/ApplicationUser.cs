using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();


    }
}
