using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime Timestamp { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public string GroupId { get; set; }

        public ChatGroup Group { get; set; }


        public ChatMessage()
        {

        }
    }
}
