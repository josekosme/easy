using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace JwtFactory
{
    public class TokenGenerator : ITokenGenerator
    {
        private SigningConfigurations _SigningConfigurations;

        public TokenGenerator(SigningConfigurations signingConfigurations)
        {
            _SigningConfigurations = signingConfigurations;
        }

        public object generate(User usuario, TokenConfigurations tokenConfigurations)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                   new GenericIdentity(usuario.UserID, "Login"),
                   new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserID)
                   }
               );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
            securityTokenDescriptor.Issuer = tokenConfigurations.Issuer;
            securityTokenDescriptor.Audience = tokenConfigurations.Audience;
            securityTokenDescriptor.SigningCredentials = _SigningConfigurations.SigningCredentials;
            securityTokenDescriptor.Subject = identity;
            securityTokenDescriptor.NotBefore = dataCriacao;
            securityTokenDescriptor.Expires = dataExpiracao;
            var securityToken = handler.CreateToken(securityTokenDescriptor);
            var token = handler.WriteToken(securityToken);

            return new
            {
                authenticated = true,
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
