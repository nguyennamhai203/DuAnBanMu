using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Net.Http;
using System.Text;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        Uri _url = new Uri("https://localhost:7050");
        public LoginController(ILogger<LoginController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithJWT(string username, string password)
        {
            LoginDto login = new LoginDto();
            login.NameAccount = username;
            login.Password = password;
            if (username == null || password == null)
            {
                HttpContext.Session.SetString("ThongBao", "Vui Lòng Nhập Tài Khoản Mật Khẩu");
                return RedirectToAction("Index", "Login");
            }
            var apiUrl = $"/api/Account/Login";
            var httpclient = _httpClientFactory.CreateClient("BeHat");
            var requestdata = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var respone = await httpclient.PostAsync(apiUrl, requestdata);
            var jsonRespone = await respone.Content.ReadAsStringAsync();
            var info = JsonConvert.DeserializeObject<LoginResponseDto>(jsonRespone);

            if (respone.IsSuccessStatusCode)
            {
                var profileUser = await httpclient.GetStringAsync($"https://localhost:7050/api/Account/FindProfileOfUser?userName={username}");
                var profileUserJson = JsonConvert.DeserializeObject<ProfileOfUserDto>(profileUser);

                HttpContext.Session.SetString("AccessToken", jsonRespone);
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("TokenCheck", info.Token);
                HttpContext.Session.SetString("Result", info.Role);

                if (profileUserJson != null)
                {
                    if (profileUserJson.TenNguoiDung != null)
                    {
                        HttpContext.Session.SetString("TenNguoiDung", profileUserJson.TenNguoiDung);
                    }

                    if (profileUserJson.TenTaiKhoan != null)
                    {
                        HttpContext.Session.SetString("UserName", profileUserJson.TenTaiKhoan);
                    }

                    if (profileUserJson.soDienThoai != null)
                    {
                        HttpContext.Session.SetString("soDienThoai", profileUserJson.soDienThoai);
                    }

                    if (profileUserJson.DiaChi != null)
                    {
                        HttpContext.Session.SetString("DiaChi", profileUserJson.DiaChi);
                    }

                    if (profileUserJson.Email != null)
                    {
                        HttpContext.Session.SetString("Email", profileUserJson.Email);
                    }

                    if (profileUserJson.GioiTinh != null)
                    {
                        HttpContext.Session.SetString("GioiTinh", profileUserJson.GioiTinh.ToString());
                    }
                }


                return RedirectToAction("Index", "Home");
            }
            else
            {

                HttpContext.Session.SetString("ThongBao", info.Message);
                var error = HttpContext.Session.GetString("ThongBao");
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        public async Task<IActionResult> SignUpWithJWT(string SDT, string email, string tennguoidung, string username, string password, string repeatPassword)
        {
            SignUpDto login = new SignUpDto();
            login.UserName = username;
            login.PassWord = password;
            login.ConfirmPassWord = repeatPassword;
            login.TenNguoiDung = tennguoidung;
            login.Email = email;
            login.SDT = SDT;

            var apiUrl = $"/api/Account/SignUpKhacHangAsync";
            var httpclient = _httpClientFactory.CreateClient("BeHat");
            var requestdata = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var respone = await httpclient.PostAsync(apiUrl, requestdata);

            var jsonRespone = await respone.Content.ReadAsStringAsync();
          
            if (respone.IsSuccessStatusCode)
            {
                return Content(jsonRespone,"application/json");
            }
            return Content(jsonRespone, "application/json");
        }

    }
}
