using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Visitante.Domain.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, bool>
    {
        public Task<bool> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
