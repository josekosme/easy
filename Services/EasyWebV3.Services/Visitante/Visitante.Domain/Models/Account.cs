using System;

namespace Visitante.Domain.Models
{
    public class Account
    {
        public Account(Guid accountId, string identificador, string senha, string email)
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
