using Microsoft.AspNetCore.Mvc;

namespace VollMed.Web.Controllers
{
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
