using Microsoft.AspNetCore.Mvc;

namespace GestaoPessoas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
