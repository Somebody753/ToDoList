using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{

    /*
      Creating own User that extends Identity User, to create relations in database.
    Each user can have multiple groups and messages
     */

    public class ApplicationUser : IdentityUser
    {
        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>(); // cross table 
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();


    }
}
