using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Hubs
{
    
    public class ChatHub: Hub
    {
        private readonly ApplicationDbContext _context;


        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        public async Task SendMessage(string message)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = Context.User?.Identity?.Name;

            var chatMessage = new ChatMessage
            {
                AuthorName = userName,
                MessageText = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();





            
            Clients.All.SendAsync("ReceiveMessage", userName, message, DateTime.Now.ToString("HH:mm"));
            
        }

    }
}
