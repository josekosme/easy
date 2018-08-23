using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Email.API.Infrastructure
{
    public class EmailMessageService : IMessageService
    {
        public Task SendEmail(string email, string subject, string message)
        {
            MailjetClient client = new MailjetClient("4b32ae6c7e59763818734d8ef7485137", "dd64861be62fc383493727aa857cc322")
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", "interacao@interacao.com.br"},
                  {"Name", "Mailjet Pilot"}
                  }},
                 { "To", new JArray {
                  new JObject {
                   {"Email", "cosme@plannerti.com.br"},
                   {"Name", "passenger 1"}
                   }
                  }},
                 {"Subject", "Your email flight plan!"},
                 {"TextPart", "Dear passenger 1, welcome to Mailjet! May the delivery force be with you!"},
                 {"HTMLPart", "<h3>Dear passenger 1, welcome to Mailjet!</h3><br />May the delivery force be with you!"}
                 }
                   });
            MailjetResponse response = client.PostAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

            return Task.FromResult(0);
        }
    }
}
