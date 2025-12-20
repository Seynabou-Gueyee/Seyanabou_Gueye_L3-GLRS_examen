using Microsoft.AspNetCore.Mvc;

namespace brasilBurger.Controllers
{
    public interface IAccountController
    {
        Task<IActionResult> Login();
        Task<IActionResult> Login(string email, string password);
        Task<IActionResult> Register();
        Task<IActionResult> Register(string nom, string prenom, string telephone, string email, string password);
        IActionResult Logout();
    }
}
