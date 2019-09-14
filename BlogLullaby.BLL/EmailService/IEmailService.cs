using BlogLullaby.BLL.Infrastructure;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.EmailService
{
    public interface IEmailService
    {
        OperationDetails EmailConfirm(string userEmail);
        Task SendEmailAsync(string email, string subject, string text, string personName);
    }
}



//37421er54@@lullaby