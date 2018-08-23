using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.API.Infrastructure
{
    public class TesteMessageService : IMessageService
    {
        public Task SendEmail(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
