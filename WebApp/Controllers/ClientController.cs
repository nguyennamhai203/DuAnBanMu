﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Text;

namespace WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public ClientController(ILogger<ClientController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(token))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");

        }
        public async Task<IActionResult> CapNhatThongTin(string userName)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            var client = _httpClientFactory.CreateClient("BeHat");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var getUser = await client.GetAsync($"/api/Account/FindProfileOfUser?userName={userName}");
                if (getUser.IsSuccessStatusCode)
                {
                    var jsonResponse = await getUser.Content.ReadAsStringAsync();

                    var content = JsonConvert.DeserializeObject<ProfileOfUserDto>(jsonResponse.ToString());

                    return View(content);
                }

                else
                {
                    return StatusCode((int)getUser.StatusCode, getUser.ReasonPhrase);
                }
            }
            else
            {

                return RedirectToAction("Index", "Login");
            }

        }
        [HttpPut]
        public async Task<IActionResult> CapNhatSDT_DiaChi(string username, string SDT, string diaChi)
        {
            userSDTDiaChi user = new userSDTDiaChi();
            user.userName = username;
            user.SDT = SDT;
            user.DiaChi = diaChi;

            var apiUrl = $"/api/Account/CapNhatSDTDiaChi";
            var httpclient = _httpClientFactory.CreateClient("BeHat");
            var requestdata = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var respone = await httpclient.PutAsync(apiUrl, requestdata);


            if (respone.IsSuccessStatusCode)
            {
                var jsonRespone = await respone.Content.ReadAsStringAsync();
                var info = JsonConvert.DeserializeObject<ResponseDto>(jsonRespone);

                //HttpContext.Session.SetString("AccessToken", jsonRespone);
                //HttpContext.Session.SetString("TokenCheck", info.Token);
                //HttpContext.Session.SetString("Result", info.Role);

                //HttpContext.Session.SetString("TenNguoiDung", profileUserJson.TenNguoiDung);
                //HttpContext.Session.SetString("UserName", profileUserJson.TenTaiKhoan);
                //HttpContext.Session.SetString("soDienThoai", profileUserJson.soDienThoai);
                //HttpContext.Session.SetString("DiaChi", profileUserJson.DiaChi);
                //HttpContext.Session.SetString("Email", profileUserJson.Email);
                //HttpContext.Session.SetString("GioiTinh", profileUserJson.GioiTinh.ToString());

                return Content(jsonRespone, "application/json");
            }
            else
            {
                var jsonRespone = await respone.Content.ReadAsStringAsync();
                //var error = HttpContext.Session.GetString("ThongBao");
                return Content(jsonRespone, "application/json");
            }
        }



        public async Task<IActionResult> DoiMatKhauView(string userName)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            var userSeesion = HttpContext.Session.GetString("UserName");
            var client = _httpClientFactory.CreateClient("BeHat");


            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" && userName == userSeesion || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien" && userName == userSeesion)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Index", "Login");
            }

        }


        [HttpPut]
        public async Task<IActionResult> CapNhatMatKhau(string userName, string oldPass, string newPass, string remindNewPass)
        {
            DoiMatKhauDto doiMatKhauDto = new DoiMatKhauDto();
            doiMatKhauDto.UserName = userName;
            doiMatKhauDto.OldPass = oldPass;
            doiMatKhauDto.NewPass = newPass;
            doiMatKhauDto.RemindNewPass = remindNewPass;

            var apiUrl = $"/api/Account/CapNhatMatKhau";
            var httpclient = _httpClientFactory.CreateClient("BeHat");
            var requestdata = new StringContent(JsonConvert.SerializeObject(doiMatKhauDto), Encoding.UTF8, "application/json");
            var respone = await httpclient.PutAsync(apiUrl, requestdata);


            if (respone.IsSuccessStatusCode)
            {
                var jsonRespone = await respone.Content.ReadAsStringAsync();
                var info = JsonConvert.DeserializeObject<ResponseDto>(jsonRespone);

                //HttpContext.Session.SetString("AccessToken", jsonRespone);
                //HttpContext.Session.SetString("TokenCheck", info.Token);
                //HttpContext.Session.SetString("Result", info.Role);

                //HttpContext.Session.SetString("TenNguoiDung", profileUserJson.TenNguoiDung);
                //HttpContext.Session.SetString("UserName", profileUserJson.TenTaiKhoan);
                //HttpContext.Session.SetString("soDienThoai", profileUserJson.soDienThoai);
                //HttpContext.Session.SetString("DiaChi", profileUserJson.DiaChi);
                //HttpContext.Session.SetString("Email", profileUserJson.Email);
                //HttpContext.Session.SetString("GioiTinh", profileUserJson.GioiTinh.ToString());

                return Content(jsonRespone, "application/json");
            }
            else
            {
                var jsonRespone = await respone.Content.ReadAsStringAsync();
                //var error = HttpContext.Session.GetString("ThongBao");
                return Content(jsonRespone, "application/json");
            }
        }
    }
}