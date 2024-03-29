using AdminApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Diagnostics;
using System.Text;

namespace AdminApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        Uri _url = new Uri("https://localhost:7050");
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
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
               
                return View("Login");
            }
        }

        public IActionResult Login(/*string ReturnUrl = "/"*/)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(accessToken))
            {
                return View("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Logout(/*string ReturnUrl = "/"*/)
        {
            HttpContext.Session.Remove("AccessToken");
            HttpContext.Session.Remove("Result");
            //  await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithJWT(string username, string password)
        {
            LoginDto login = new LoginDto();
            login.NameAccount = username;
            login.Password = password;

            var apiUrl = $"/api/Account/Login";
            var httpclient = _httpClientFactory.CreateClient("BeHat");
            var requestdata = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var respone = await httpclient.PostAsync(apiUrl, requestdata);
            var jsonRespone = await respone.Content.ReadAsStringAsync();
            var info = JsonConvert.DeserializeObject<LoginResponseDto>(jsonRespone);

            if (respone.IsSuccessStatusCode)
            {
                var profileUser = await httpclient.GetStringAsync($"https://localhost:7050/api/Account/FindProfileOfUser?userName={username}");
              var profileUserJson =   JsonConvert.DeserializeObject<ProfileOfUserDto>(profileUser);

                HttpContext.Session.SetString("AccessToken", jsonRespone);
                HttpContext.Session.SetString("TokenCheck", info.Token);
                HttpContext.Session.SetString("Result", info.Role);

                HttpContext.Session.SetString("TenNguoiDung", profileUserJson.TenNguoiDung);
                HttpContext.Session.SetString("UserName", profileUserJson.TenTaiKhoan);
                HttpContext.Session.SetString("soDienThoai", profileUserJson.soDienThoai);
                HttpContext.Session.SetString("DiaChi", profileUserJson.DiaChi);
                HttpContext.Session.SetString("Email", profileUserJson.Email);
                HttpContext.Session.SetString("GioiTinh", profileUserJson.GioiTinh.ToString());
               
                return RedirectToAction("Index", "Home");
            }
            else {
              
                HttpContext.Session.SetString("ThongBao", info.Message);
                var error = HttpContext.Session.GetString("ThongBao");
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}