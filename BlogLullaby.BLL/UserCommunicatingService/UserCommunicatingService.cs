using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserCommunicatingService.DTO;
using BlogLullaby.DAL.DataStore.Entities;
using BlogLullaby.DAL.DataStore.Interfaces;

namespace BlogLullaby.BLL.UserCommunicatingService
{
    public class UserCommunicatingService: IUserCommunicatingService
    {
        private IDataStore _dataStore;
        public UserCommunicatingService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<OperationDetails> AddMessageToDialogAsync(MessageDTO messageDTO)
        {
            if (!messageDTO.IsValid())
            {
                return messageDTO.GetValidateError();
            }
            var dialog = await _dataStore.Dialogs.GetByIdAsync(messageDTO.DialogId);
            if (dialog == null)
                return new OperationDetails(false, new string[] { "Dialog not found." });
            var profile = await _dataStore.UserProfiles.GetByNameAsync(messageDTO.Sender.Username);
            if(profile == null)
                return new OperationDetails(false, new string[] { "Message sender not found." });
            var message = messageDTO.MapToEntity(profile, dialog);
            var savedMessage = await _dataStore.Messages.CreateAsync(message);
            if (savedMessage == null)
                return new OperationDetails(false, new string[] { "Message not send." });
            /// add message to unreadMessages
            var recipients = dialog.DialogMembers.Select(x => x.UserProfile).Where(x => x.Id != profile.Id);
            foreach (var x in recipients)
                await _dataStore.NotReadMessages.CreateByCompositeKeyAsync(savedMessage.Id, x.Id);
            ////
            return new OperationDetails(true, new string[] { "Message send." });
        }

        public async Task<OperationDetails> AddMemberToDialogAsync(string dialogId, string userName)
        {
            if(String.IsNullOrEmpty(dialogId) && String.IsNullOrEmpty(userName))
                return new OperationDetails(false, new string[] { $"Not correct datas." });
            var profile = await _dataStore.UserProfiles.GetByNameAsync(userName);
            if(profile == null)
            {
                return new OperationDetails(false, new string[] { $"Username '{userName}' not found." });
            }
            var dialog = await _dataStore.Dialogs.GetByIdAsync(dialogId);
            if (dialog == null)
            {
                return new OperationDetails(false, new string[] { "Not existing dialog." });
            }
            object newDialogMember = null;//await _dataStore.DialogMembers.CreateByCompositeKeyAsync(dialogId, profile.Id);
            if(newDialogMember == null)
                return new OperationDetails(false, new string[] { $"User '{userName}' not added to dialog" });
            return new OperationDetails(true, new string[] { $"User '{userName}' was added to dialog" });
        }
        
        public async Task<OperationDetails> CreateDialogAsync(DialogCreatingDTO newDialog)
        {
            var dialogMembers = newDialog.Members
                .Select(x => _dataStore.UserProfiles.GetByNameAsync(x).Result)
                .Where(x => x != null);
            var dialog = newDialog.GetDialogEntity(dialogMembers);

            var savedDialog = await _dataStore.Dialogs.CreateAsync(dialog);
            if (savedDialog == null)
                return new OperationDetails(false, new string[] { "Dialog not created." });
            var details = new OperationDetails(true, new List<string>());
            ((List<string>)details.Descriptions).Add("Dialog was created.");
            
            var systemMessage = new Message
            {
                Dialog = savedDialog,
                //Sender = await _dataStore.UserProfiles.GetByNameAsync("System"),
                Body = "Dialog Created",
                Date = DateTime.Now
            };
            await _dataStore.Messages.CreateAsync(systemMessage);
            return details;
        }

        public async Task<DialogDTO> GetDialogByIdAsync(string dialogId)
        {
            var notReadMessages = _dataStore.NotReadMessages.GetAll();
            var dialogEntity = await _dataStore.Dialogs.GetByIdAsync(dialogId);
            var dialogDto = dialogEntity.MapToDTO(notReadMessages);
            return dialogDto;
        }

        public async Task<(IEnumerable<DialogPreview> dialogList, int pageCount)> GetDialogListAsync(DialogCriterion criterion)
        {
            IEnumerable<DialogPreview> dialogViews = null;
            var pageCount = 0;
            var userProfile = await _dataStore.UserProfiles.GetByNameAsync(criterion.Username);
            if(userProfile != null)
            {
                dialogViews = userProfile.DialogMembers
                    .Select(x => x.Dialog.MapToPreview(_dataStore.NotReadMessages.GetAll()))
                    .OrderByDescending(x => x.LastMessage.Date)
                    .Paging(ref pageCount, criterion.PageNumber, criterion.PageSize);
            }
            return (dialogViews, pageCount);
        }

        //send to metod last unRead message, and all unread message before this will isRead.
        public async Task ReadMessageAsync(string messageId, string username)
        {
            var user = await _dataStore.UserProfiles.GetByNameAsync(username);
            var dialog = _dataStore.Dialogs.GetAll().FirstOrDefault(x => x.Messages.FirstOrDefault(y => y.Id == messageId) != null);
            var messages = dialog.Messages.OrderByDescending(x => x.Date);
            var readMessage = messages.First(x => x.Id == messageId);
            foreach(var mess in messages)
            {
                if(mess.Date < readMessage.Date)
                {
                    if (_dataStore.NotReadMessages.Find(mess.Id, user.Id) != null)
                        await _dataStore.NotReadMessages.DeleteByCompositeKeyAsync(mess.Id, user.Id);
                    else
                        break;
                }
            }
            await _dataStore.NotReadMessages.DeleteByCompositeKeyAsync(messageId, user.Id);
        }
    }
}
