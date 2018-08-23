using EasyWeb.Domain.Core.Events;
using System;
using System.Collections.Generic;

namespace Visitante.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}
