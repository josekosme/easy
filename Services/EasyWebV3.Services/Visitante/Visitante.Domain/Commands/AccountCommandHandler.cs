using EasyWeb.Domain.Core.Bus;
using EasyWeb.Domain.Core.CommandHandlers;
using EasyWeb.Domain.Core.Interfaces;
using EasyWeb.Domain.Core.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Visitante.Domain.Events;
using Visitante.Domain.Interfaces;
using Visitante.Domain.Models;

namespace Visitante.Domain.Commands
{
    //    public class AccountCommandHandler :  IRequestHandler<RegisterAccountCommand, bool>
    public class AccountCommandHandler : CommandHandler, IRequestHandler<RegisterAccountCommand, bool>
    {
        private readonly IAccountRepository _userRepository;
        private readonly IMediatorHandler Bus;

        public AccountCommandHandler(IAccountRepository userRepository,
                                  IUnitOfWork uow,
                                  IMediatorHandler bus,
                                  INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterAccountCommand message, CancellationToken cancellationToken)
        {
            //if (!message.IsValid())
            //{
            //    // NotifyValidationErrors(message);
            //    return Unit.Task;
            //}

            var login = new Account(Guid.NewGuid(), message.Identificador, message.Senha, message.Email);

            //if (_userRepository.GetByIdentificador(login.Identificador) != null)
            //{
            //    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
            //    return Unit.Task;
            //}

            _userRepository.Add(login);

            if (Commit())
            {
                Bus.RaiseEvent(new AccountRegisteredEvent(login.AccountId, login.Identificador, login.Senha));
            }

            return Task.FromResult(true); 
        }
    }
}
