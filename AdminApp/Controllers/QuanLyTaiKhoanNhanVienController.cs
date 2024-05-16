using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http;

namespace AdminApp.Controllers
{
    public class QuanLyTaiKhoanNhanVienController : Controller
    {

        private readonly ILogger<QuanLyTaiKhoanNhanVienController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        Uri _url = new Uri("https://localhost:7050");
        public QuanLyTaiKhoanNhanVienController(ILogger<QuanLyTaiKhoanNhanVienController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                var url = $"/api/Account/DanhSachNhanVien";
                var response = await client.GetAsync(url);
                var apiData = await response.Content.ReadAsStringAsync();
                var a = JsonConvert.DeserializeObject<List<NguoiDung>>(apiData);
                return View(a);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
           
        }

        public IActionResult CreateNhanVien()
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
        public async Task<IActionResult> CreateNhanVien(SignUpDto model, string code) /*SignUpDto model, string mail, string codeconfirm*/
        {
            var httpClient = _httpClientFactory.CreateClient("BeHat");

            var emailAdmin = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(emailAdmin))
            {
                var res = new ResponseDto()
                {
                    Message = "bạn không có quyền",
                    Code = 400,
                    IsSuccess = false
                };

                return Json(new { Message = "Bạn không có quyền", Code = 400 });
            }

            model.ConfirmPassWord = "string";
            //emailAdmin = "hainnph27584@fpt.edu.vn";
            var url = $"/api/Account/XacNhan?mail={emailAdmin}&codeconfirm={code}";
            var response = await httpClient.PostAsJsonAsync(url, model);
            //var response = await httpClient.PostAsync(url, null);
            var trave = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                var result = JsonConvert.DeserializeObject<ResponseDto>(await response.Content.ReadAsStringAsync());

                return Content(trave, "application/json");

            }

            return Content(trave, "application/json");



        }

        [HttpPost]
        public async Task<IActionResult> GuiMaXacNhan()
        {
            var httpClient = _httpClientFactory.CreateClient("BeHat");

            string mail = HttpContext.Session.GetString("Email");

            var url = $"/api/Account/MailToAdmin?mail={mail}";
            var response = await httpClient.PostAsync(url, null); var trave = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                var result = JsonConvert.DeserializeObject<ResponseDto>(trave);

                return Content(trave, "application/json");

            }

            return Content(trave, "application/json");



        }
    }
}
