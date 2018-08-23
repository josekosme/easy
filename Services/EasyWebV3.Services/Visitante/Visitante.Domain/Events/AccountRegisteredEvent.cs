using EasyWeb.Domain.Core.Events;
using System;

namespace Visitante.Domain.Events
{
    public class AccountRegisteredEvent : Event
    {
        public AccountRegisteredEvent(Guid Loginid, string identificador, string senha)
        {
            LoginId = Loginid;
            Identificador = identificador;
            Senha = senha;
        }
        public Guid LoginId { get; set; }

        public string Identificador { get; private set; }

        public string Senha { get; private set; }
    }
}

