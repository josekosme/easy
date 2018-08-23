using Visitante.API.Application.ViewModels;

namespace Visitante.API.Application.Queries
{
    public interface IAccountQueries
    {
        AccountViewModel getByEmail(string email);
        AccountViewModel getByIdentificador(string identificador);
    }
}
