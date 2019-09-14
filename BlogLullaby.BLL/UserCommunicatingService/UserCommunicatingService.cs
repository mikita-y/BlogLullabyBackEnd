using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserCommunicatingService.DTO;
using BlogLullaby.DAL.DataStore.Entities;
using BlogLullaby.DAL.DataStore.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var profile = await _dataStore.UserProfiles.GetByNameAsync(messageDTO.Owner.Username);
            if(profile == null)
                return new OperationDetails(false, new string[] { "Message sender not found." });
            var message = messageDTO.MapToEntity(profile, dialog);
            var savedMessage = await _dataStore.Messages.CreateAsync(message);
            if (savedMessage == null)
                return new OperationDetails(false, new string[] { "Message not send." });
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
            /*var AddingUsersdetails = new List<string>();

            await Task.Run(() =>
            {
                foreach (var userName in newDialog.Members)
                {
                    var detail = AddMemberToDialogAsync(savedDialog.Id, userName).Result;
                    ((List<string>)details.Descriptions).AddRange(detail.Descriptions);
                }
            });*/
            return details;
        }

        public async Task<DialogDTO> GetDialogByIdAsync(string id)
        {
            var dialogEntity = await _dataStore.Dialogs.GetByIdAsync(id);
            return dialogEntity.MapToDTO();
        }

        public async Task<IEnumerable<DialogView>> GetDialogListByUserNameAsync(string username, DialogCriterion criterion)
        {
            IEnumerable<DialogView> dialogViews = null;
            var userProfile = await _dataStore.UserProfiles.GetByNameAsync(username);
            var d = userProfile.DialogMembers.ToArray();//delete
            if(userProfile != null)
                dialogViews = userProfile.DialogMembers.Select(x => x.Dialog.MapToView());
            dialogViews.OrderBy(x => x.LastMessage.Date)
                .Paging(criterion.PageNumber, criterion.PageSize);
            return dialogViews;
        }
    }
}
