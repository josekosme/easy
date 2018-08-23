using EasyWeb.Domain.Core.Commands;
using MediatR;
using System.Runtime.Serialization;

namespace Visitante.Domain.Commands
{
    public class RegisterAccountCommand : IRequest<bool>
    {
        [DataMember]
        public string Identificador { get; private set; }
        [DataMember]
        public string Senha { get; private set; }
        [DataMember]
        public string Email { get; private set; }

        public RegisterAccountCommand(string identificador, string senha, string email)
        {
            Identificador = identificador;
            Senha = senha;
            Email = email;
        }

        //public override bool IsValid()
        //{
        //    return true;
        //}
    }
}
