using EasyWeb.Domain.Core.Commands;
using EasyWeb.Domain.Core.Events;
using System.Threading.Tasks;

namespace EasyWeb.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
