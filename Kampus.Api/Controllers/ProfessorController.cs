using Microsoft.AspNetCore.Mvc;

namespace Kampus.Api.Controllers
{
    public class ProfessorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
