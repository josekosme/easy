using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public TokenController(SignInManager<IdentityUser> signInManager)
        {
            this._signInManager = signInManager;
        }

        [Route("/token")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(string username, string password)
        {
            bool commandResult = false;
            string requestResult = string.Empty;

            //if (IsValidUserAndPasswordCombination(username, password))
            //    return new ObjectResult(GenerateToken(username));

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(username, password, isPersistent: true, lockoutOnFailure: false);
            if (!passwordSignInResult.Succeeded)
            {
                var teste = new ObjectResult(GenerateToken(username));
            }

            return commandResult ? (IActionResult)Ok(requestResult) : (IActionResult)BadRequest();
        }

        private object GenerateToken(string username)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EASY3.0 Token Factory"));

            var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, "John"),
                    new Claim(JwtRegisteredClaimNames.Email, "john.doe@blinkingcaret.com")
                    };

            var token = new JwtSecurityToken(
                            issuer: "EASY3.0",
                            audience: "the client of your app",
                            claims: claims,
                            notBefore: DateTime.Now,
                            expires: DateTime.Now.AddDays(28),
                            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                        );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}