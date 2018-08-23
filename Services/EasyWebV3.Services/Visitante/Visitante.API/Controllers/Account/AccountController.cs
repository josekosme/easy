using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Visitante.API.Application.Queries;
using Visitante.API.Application.ViewModels;
using Visitante.Domain.Commands;

namespace Visitante.API.Controllers.Usuario
{
    //[Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAccountQueries _accountQueries;

        public AccountController(
            IMediator mediator, 
            IAccountQueries accountQueries)
        {
            _mediator = mediator;
            _accountQueries = accountQueries;
        }

        [Route("register")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> registerAccount([FromBody]RegisterAccountCommand command)
        {
            bool commandResult = false;

            try
            {
                RegisterUser teste = new RegisterUser();
                await _mediator.Send(command);
                commandResult = true;
            }
            catch (System.Exception ex)
            {

                throw;
            }

            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        }

        [HttpGet, Route("getByEmail")]
        [ProducesResponseType(typeof(AccountViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> getByEmail(string email)
        {
            bool commandResult = false;
            AccountViewModel usuarioViewmodel = null;
            try
            {
                usuarioViewmodel = _accountQueries.getByIdentificador(email);
            }
            catch (System.Exception ex)
            {
                throw;
            }

            commandResult = true;

            return commandResult ? (IActionResult)Ok(usuarioViewmodel) : (IActionResult)BadRequest();
        }

        [Route("login")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> loginAccount([FromBody]RegisterAccountCommand command)
        {
            bool commandResult = false;

            try
            {
                commandResult = true;
            }
            catch (System.Exception ex)
            {

                throw;
            }

            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        }

        [HttpGet, Route("Teste")]
        public async Task<IActionResult> Teste()
        {
            //await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}