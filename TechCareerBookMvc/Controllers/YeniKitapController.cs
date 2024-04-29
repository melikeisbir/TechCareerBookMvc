using Microsoft.AspNetCore.Mvc;

namespace TechCareerBookMvc.Controllers
{
    public class YeniKitapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
