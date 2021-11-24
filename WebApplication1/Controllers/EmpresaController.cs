using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Codigo, Nome, NomeFantasia, CNPJ")] EmpresaModel empresaModel)
        {
            try
            {
                return View("~/Views/Home/Index.cshtml");
            }
            catch 
            {
                return View();
                
            }







        }
    }
}
