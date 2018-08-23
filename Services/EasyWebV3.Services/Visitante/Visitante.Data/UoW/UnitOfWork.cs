using EasyWeb.Domain.Core.Interfaces;
using Visitante.Data.Context;

namespace Visitante.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EasyVisitanteContext _context;

        public UnitOfWork(EasyVisitanteContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
