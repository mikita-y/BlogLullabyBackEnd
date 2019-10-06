using BlogLullaby.BLL.UserCommunicatingService;
using BlogLullaby.WEB_API.Infrastructure;
using BlogLullaby.WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BlogLullaby.WEB_API.Hubs
{
    [Authorize]
    public class DialogHub : Hub
    {
        private IUserCommunicatingService _communicatingService;
        public DialogHub(IUserCommunicatingService service)
        {
            _communicatingService = service;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Connecting", $"{Context.ConnectionId} вошел в чат");
            await base.OnConnectedAsync();
        }

        public async Task ConnectToDialogue(string dialogId)
        {
            /*var username = await Context.GetHttpContext().GetUserNameAsync();
            if (username == null)
                return;*/
            await Groups.AddToGroupAsync(Context.ConnectionId, dialogId);
            var dialog = await _communicatingService.GetDialogByIdAsync(dialogId);
            await Clients.Caller.SendAsync("GetDialog", dialog);
        }

        public async Task LoadPreviousMessages(string dialogId, int loadedMessagesCount)
        {
            var messages = await _communicatingService.LoadPreviousMessagesAsync(dialogId, loadedMessagesCount);
            await Clients.Caller.SendAsync("LoadPreviousMessages", messages);
        }

        public async Task SendMessage(CreatingMessageModel message)
        {
            var username = await Context.GetHttpContext().GetUserNameAsync();
            if (username == null)
                return;
            var messageDto = message.MapToDTO(username);
            await Clients.Group(message.DialogId).SendAsync("ReceiveMessage", messageDto);

            var result = await _communicatingService.AddMessageToDialogAsync(messageDto);
            // добавить логику по определению совпадения Id или обраки ошибки
        }

        public async Task ReadMessage(string messageId)
        {
            var username = await Context.GetHttpContext().GetUserNameAsync();
            if (username == null)
                return;
            //await Clients.Group(_dialogId).SendAsync("ReadMessage", messageId);

            await _communicatingService.ReadMessageAsync(messageId, username);
        }

    }
}
