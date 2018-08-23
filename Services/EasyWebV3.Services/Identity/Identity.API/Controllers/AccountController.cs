using Email.API.Infrastructure;
using Identity.API.Model;
using JwtFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMessageService _MessageService;
        private readonly ITokenGenerator _TokenGenerator;

        public AccountController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IMessageService messageService,
            ITokenGenerator tokenGenerator)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._MessageService = messageService;
            this._TokenGenerator = tokenGenerator;
        }

        [HttpPost, Route("Register")]  
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody]TokenViewModel tokenViewModel)
        {
            if (tokenViewModel.Password != tokenViewModel.Repassword)
            {
                ModelState.AddModelError(string.Empty, "Password don't match");
                return View();
            }

            var newUser = new IdentityUser
            {
                UserName = tokenViewModel.Email,
                Email = tokenViewModel.Email
            };

            var userCreationResult = await _userManager.CreateAsync(newUser, tokenViewModel.Password);
            if (!userCreationResult.Succeeded)
            {
                foreach (var error in userCreationResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View();
            }

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var tokenVerificationUrl = Url.Action("VerifyEmail", "Account", new { id = newUser.Id, token = emailConfirmationToken }, Request.Scheme);

            await _MessageService.SendEmail(tokenViewModel.Email, "Verify your email", $"Click <a href=\"{tokenVerificationUrl}\">here</a> to verify your email");

            return Content("Check your email for a verification link");
        }

        public async Task<IActionResult> VerifyEmail(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException();

            var emailConfirmationResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!emailConfirmationResult.Succeeded)
                return Content(emailConfirmationResult.Errors.Select(error => error.Description).Aggregate((allErrors, error) => allErrors += ", " + error));

            return Content("Email confirmed, you can now log in");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return View();
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Confirm your email first");
                return View();
            }

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, password, isPersistent: rememberMe, lockoutOnFailure: false);
            if (!passwordSignInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return View();
            }

            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Content("Check your email for a password reset link");

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetUrl = Url.Action("ResetPassword", "Account", new { id = user.Id, token = passwordResetToken }, Request.Scheme);

            await _MessageService.SendEmail(email, "Password reset", $"Click <a href=\"" + passwordResetUrl + "\">here</a> to reset your password");

            return Content("Check your email for a password reset link");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id, string token, string password, string repassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException();

            if (password != repassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match");
                return View();
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPasswordResult.Succeeded)
            {
                foreach (var error in resetPasswordResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View();
            }

            return Content("Password updated");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        [HttpPost, Route("createToken")]
        public async Task<IActionResult> createToken()
        {
            bool commandResult = false;
            string requestResult = string.Empty;

            User usuario = new User("cosme@gmail.com", "AISEJHDFIWJNECVWNO");
            TokenConfigurations tokenConfigurations = new TokenConfigurations();
            tokenConfigurations.Audience = "EVERYONE";
            tokenConfigurations.Issuer = "EASY3.0";
            tokenConfigurations.Seconds = 31622400;

            try
            {
                var token = this._TokenGenerator.generate(usuario, tokenConfigurations);
                requestResult = JsonConvert.SerializeObject(token);
                commandResult = true;
            }
            catch (Exception)
            {
                commandResult = false;
            }


            return commandResult ? (IActionResult)Ok(requestResult) : (IActionResult)BadRequest();
        }

        [Authorize("Bearer")]
        [HttpGet, Route("Teste")]
        public async Task<IActionResult> Teste()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}