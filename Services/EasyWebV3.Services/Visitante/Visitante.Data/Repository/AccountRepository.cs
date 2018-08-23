using Microsoft.EntityFrameworkCore;
using System;
using Visitante.Data.Context;
using Visitante.Domain.Interfaces;
using Visitante.Domain.Models;

namespace Visitante.Data.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(EasyVisitanteContext context)
          : base(context)
        {

        }
    }
}
