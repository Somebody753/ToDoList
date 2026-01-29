using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
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
        public async Task SendMessage(string message, string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

            //taking Id and name of logged User
            //ID to create new Message object and name to display it on a list
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = Context.User?.Identity?.Name;

            var chatMessage = new ChatMessage
            {
                AuthorId = userId,
                GroupId = groupId,
                MessageText = message,
                Timestamp = DateTime.UtcNow,
            };

            //Adding message to database and saving it
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            //sending all clients in the group
            await Clients.Group(groupId).SendAsync("ReceiveMessage", userName, message, DateTime.Now.ToString("HH:mm"));


        }


        public async Task JoinGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }





    }
}
