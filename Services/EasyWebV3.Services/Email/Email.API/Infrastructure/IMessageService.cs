using System.Threading.Tasks;

namespace Email.API.Infrastructure
{
    public interface IMessageService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
