using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserCommunicatingService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.UserCommunicatingService
{
    public interface IUserCommunicatingService
    {
        Task<OperationDetails> CreateDialogAsync(DialogCreatingDTO newDialog);
        Task<DialogDTO> GetDialogByIdAsync(string dialogId);
        Task<IEnumerable<MessageDTO>> LoadPreviousMessagesAsync(string dialogId, int loadedMessagesCount);
        Task<OperationDetails> AddMemberToDialogAsync(string dialogId, string username);
        Task<OperationDetails> AddMessageToDialogAsync(MessageDTO messageDTO);
        Task<(IEnumerable<DialogPreview> dialogList, int pageCount)> GetDialogListAsync(DialogCriterion criterion);
        Task ReadMessageAsync(string messageId, string username);
    }
}
