using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;
using System.Text;


namespace AdminApp.Controllers
{
    public class MauSacController : Controller
    {
        // GET: HomeController1
        public HttpClient httpClient { get; set; }
        public MauSacController(HttpClient httpClient)
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
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> GetAll()
        {


            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/Get-All-MauSac";
                var response = await httpClient.GetAsync(url);
                var apiData = await response.Content.ReadAsStringAsync();
                var a = JsonConvert.DeserializeObject<List<MauSac>>(apiData);
                return View(a);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }

        // GET: HomeController1/Details/5
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
        public async Task<IActionResult> CreateMauSac(MauSac a)
        {


            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/Create-MauSac?MaMau={a.MaMauSac}&tenMau={a.TenMauSac}&TrangThai={a.TrangThai}";
                var response = await httpClient.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("GetAllMauSac");
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }



        public async Task<IActionResult> DetailMauSac(Guid a)
        {



            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/Get-MauSac?a={a}";
                var response = await httpClient.GetAsync(url);
                var apiData = await response.Content.ReadAsStringAsync();
                var MS = JsonConvert.DeserializeObject<MauSac>(apiData);
                return View(MS);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }


        // GET: HomeController1/Edit/5
        public async Task<IActionResult> EditMauSac(Guid a)
        {
         

            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/Get-MauSac?a={a}";
                var response = await httpClient.PostAsync(url, null);
                var apiData = await response.Content.ReadAsStringAsync();
                var MS = JsonConvert.DeserializeObject<MauSac>(apiData);
                return View(MS);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditMauSac(MauSac a)
        {
        


            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/Update-MS?id={a.Guid}&MaMau={a.MaMauSac}&tenMau={a.TenMauSac}&TrangThai={a.TrangThai}";
                var response = await httpClient.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("GetAllMauSac");

                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }



        // GET: HomeController1/Delete/5
        public async Task<IActionResult> DeleteMauSac(Guid id)
        {
            var url = $"https://localhost:7050/delete-MS?id={id}";
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetAllMauSac");

            return View();
        }


    }
}
