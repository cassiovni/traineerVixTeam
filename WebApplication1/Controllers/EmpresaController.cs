using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
