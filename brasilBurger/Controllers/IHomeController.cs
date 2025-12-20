using Microsoft.AspNetCore.Mvc;

namespace brasilBurger.Controllers
{
    public interface IHomeController
    {
        IActionResult Index();
        IActionResult Privacy();
        IActionResult Error();
    }
}
