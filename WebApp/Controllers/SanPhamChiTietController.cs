using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SanPhamChiTietController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
