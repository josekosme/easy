using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visitante.API.Application.ViewModels
{
    public class AccountViewModel
    {
        public AccountViewModel(Guid accountId, string identificador, string senha, string email)
        {
            AccountId = accountId;
            Identificador = identificador;
            Senha = senha;
            Email = email;
        }

        public Guid AccountId { get; set; }
        public string Identificador { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
    }
}
