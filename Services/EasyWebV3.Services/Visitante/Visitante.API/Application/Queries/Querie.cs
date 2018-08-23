using Microsoft.EntityFrameworkCore;
using Visitante.Data.Context;

namespace Visitante.API.Application.Queries
{
    public class Querie<TEntity> : IQuerie<TEntity> where TEntity : class
    {
        protected readonly EasyVisitanteQuerieContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Querie(EasyVisitanteQuerieContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

    }
}
