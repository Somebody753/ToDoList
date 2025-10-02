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


        public async Task JoinGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        [Authorize]
        public async Task SendMessage(string message, string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);


            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = Context.User?.Identity?.Name;

            var chatMessage = new ChatMessage
            {
                AuthorId = userId,
                GroupId = groupId,
                MessageText = message,
                Timestamp = DateTime.UtcNow,
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            //await Clients.All.SendAsync("ReceiveMessage", userName, message, DateTime.Now.ToString("HH:mm"));
            await Clients.Group(groupId).SendAsync("ReceiveMessage", userName, message, DateTime.Now.ToString("HH:mm"));


        }

    }
}
