using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class SanPhamYeuThichController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}