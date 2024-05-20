
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;
using System.Text;


namespace AdminApp.Controllers
{
    public class XuatXuController : Controller
    {
        // GET: HomeController1
        public HttpClient httpClient { get; set; }
        public XuatXuController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public IActionResult Index()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

            public IActionResult Privacy()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public async Task<IActionResult> GetAllXuatXu()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/api/XuatXu/GetAll";
            var response = await httpClient.GetAsync(url);
            var apiData = await response.Content.ReadAsStringAsync();
            var XuatXu = JsonConvert.DeserializeObject<List<XuatXu>>(apiData);
                return View(XuatXu);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

            // GET: HomeController1/Details/5
            [HttpGet]
        public IActionResult Create()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(XuatXu a)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/api/XuatXu/post-xuat-xu-param?guid={a.Guid}&maxuatxu={a.MaXuatXu}&tenxuatxu={a.TenXuatXu}&trangthai={a.TrangThai}";
            var response = await httpClient.PostAsync(url, null);
            if (response.IsSuccessStatusCode) { return RedirectToAction("GetAllXuatXu"); }

            else return View();

            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }






        // GET: HomeController1/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditXuatXu(Guid id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/api/XuatXu/GetAll";
            var response = await httpClient.GetAsync(url);
            var apiData = await response.Content.ReadAsStringAsync();
            var XuatXu = JsonConvert.DeserializeObject<List<XuatXu>>(apiData).FirstOrDefault(x => x.Guid == id);
                return View(XuatXu);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

            [HttpPost]
        public async Task<IActionResult> EditXuatXu(XuatXu a)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/api/XuatXu/put-xuat-xu?id={a.Guid}&maxuatxu={a.MaXuatXu}&tenxuatxu={a.TenXuatXu}&trangthai={a.TrangThai}";
            var response = await httpClient.PutAsync(url, null);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetAllXuatXu");

            return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        // GET: HomeController1/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/api/XuatXu/delete-xuat-xu?id={id}";
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetAllXuatXu");

            return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


    }
}
