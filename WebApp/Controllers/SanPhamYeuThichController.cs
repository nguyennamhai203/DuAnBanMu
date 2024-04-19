using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SanPhamYeuThichController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}