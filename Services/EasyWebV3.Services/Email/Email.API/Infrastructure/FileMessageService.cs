using System.IO;
using System.Threading.Tasks;

namespace Email.API.Infrastructure
{
    public class FileMessageService : IMessageService
    {
        public Task SendEmail(string email, string subject, string message)
        {
            var emailMessage = $"To: {email}\nSubject: {subject}\nMessage: {message}\n\n";

            File.AppendAllText("emails.txt", emailMessage);

            return Task.FromResult(0);
        }
    }
}
