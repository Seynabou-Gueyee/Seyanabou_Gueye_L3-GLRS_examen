using Microsoft.AspNetCore.Mvc;
using brasilBurger.Models;
using brasilBurger.Services;

namespace brasilBurger.Controllers.impl
{
    [Route("Account")]
    public class AccountController : Controller, IAccountController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("Login")]
        public Task<IActionResult> Login()
        {
            return Task.FromResult<IActionResult>(View("~/Views/Account/Login.cshtml"));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var client = await _authService.AuthenticateAsync(email, password);
            
            if (client == null)
            {
                ViewBag.Error = "Email ou mot de passe incorrect";
                return View("~/Views/Account/Login.cshtml");
            }

            // Stocker l'ID du client en session
            HttpContext.Session.SetInt32("ClientId", client.Id);
            HttpContext.Session.SetString("ClientNom", $"{client.Prenom} {client.Nom}");
            HttpContext.Session.SetString("ClientEmail", client.Email);

            return RedirectToAction("Catalogue", "Client");
        }

        [HttpGet]
        [Route("Register")]
        public Task<IActionResult> Register()
        {
            return Task.FromResult<IActionResult>(View("~/Views/Account/Register.cshtml"));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(string nom, string prenom, string telephone, string email, string password)
        {
            // Vérifier si l'email existe déjà
            if (await _authService.EmailExistsAsync(email))
            {
                ViewBag.Error = "Cet email est déjà utilisé";
                return View("~/Views/Account/Register.cshtml");
            }

            var client = new Client
            {
                Nom = nom,
                Prenom = prenom,
                Telephone = telephone,
                Email = email,
                MotDePasse = password
            };

            await _authService.RegisterAsync(client);

            // Connexion automatique après inscription
            HttpContext.Session.SetInt32("ClientId", client.Id);
            HttpContext.Session.SetString("ClientNom", $"{client.Prenom} {client.Nom}");
            HttpContext.Session.SetString("ClientEmail", client.Email);

            return RedirectToAction("Catalogue", "Client");
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
