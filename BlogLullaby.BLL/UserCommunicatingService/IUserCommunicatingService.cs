using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserCommunicatingService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.UserCommunicatingService
{
    public interface IUserCommunicatingService
    {
        Task<OperationDetails> CreateDialogAsync(DialogCreatingDTO newDialog);
        Task<DialogDTO> GetDialogByIdAsync(string id);
        Task<OperationDetails> AddMemberToDialogAsync(string dialogId, string username);
        Task<OperationDetails> AddMessageToDialogAsync(MessageDTO messageDTO);
        Task<IEnumerable<DialogView>> GetDialogListByUserNameAsync(string username, DialogCriterion criterion);
    }
}
