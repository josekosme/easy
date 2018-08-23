using Microsoft.EntityFrameworkCore;
using System.Linq;
using Visitante.API.Application.ViewModels;
using Visitante.Data.Context;
using Visitante.Domain.Models;

namespace Visitante.API.Application.Queries
{
    public class AccountQueries : Querie<Account>, IAccountQueries
    {
        public AccountQueries(EasyVisitanteQuerieContext context)
       : base(context)
        {

        }
        public AccountViewModel getByEmail(string email)
        {
            var linq = DbSet.AsNoTracking().Where(x => x.Email.Trim().Equals(email)).Select(x => x);
            return MapToViewModel(linq.FirstOrDefault());
        }

        public AccountViewModel getByIdentificador(string identificador)
        {
            var linq = DbSet.AsNoTracking().Where(x => x.Identificador.Trim().Equals(identificador)).Select(x => x);
            return MapToViewModel(linq.FirstOrDefault());
        }

        private AccountViewModel MapToViewModel(Account account)
        {
            AccountViewModel accountViewModel = null;

            if (account != null)
            {
                accountViewModel = new AccountViewModel(
                    account.AccountId,
                    account.Identificador,
                    account.Senha,
                    account.Email
                    );
            }

            return accountViewModel;
        }
    }
}
