using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System;
using System.Data.Entity;

namespace WebApp.Controllers
{
    public class SanPhamYeuThichController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public SanPhamYeuThichController(IHttpClientFactory httpClient)
        {
            _clientFactory = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            string requestURL =
                $"https://localhost:7050/api/SanPhamYeuThich/get-spyt";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestURL); // lấy response
            // Đọc từ response chuỗi Json là kết quả của phép trả về
            string apiData = await response.Content.ReadAsStringAsync();
            // Có data rồi thì ta sẽ convert về dữ liệu mình cần để đưa sang view
            var spyts = JsonConvert.DeserializeObject<List<SanPhamYeuThich>>(apiData);
            return View(spyts);
        }

        // GET: SanPhamYeuThichController/Details/5
        // Người dùng xem danh sách yêu thích
        public async Task<IActionResult> Details()
        {

            var token = HttpContext.Session.GetString("AccessToken");
            var idnguoidung = HttpContext.Session.GetString("IdNguoiDung");

            if (string.IsNullOrEmpty(token) && string.IsNullOrEmpty(idnguoidung))
            {
                return RedirectToAction("SignUp", "Login");
            }
            else
            {

                /// api / SanPhamYeuThich / Nguoi - dung - xem - danh - sach - san - pham - yeu - thich ? userId = 3b82fb0f - 6626 - 4223 - 3d33 - 08dc744a1c7a
                string requestURL = $"https://localhost:7050/api/SanPhamYeuThich/Nguoi-dung-xem-danh-sach-san-pham-yeu-thich?userId={Guid.Parse(idnguoidung)}";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(requestURL); // lấy response
                                                                      // Đọc từ response chuỗi Json là kết quả của phép trả về
                string apiData = await response.Content.ReadAsStringAsync();
                // Có data rồi thì ta sẽ convert về dữ liệu mình cần để đưa sang view
                var spyts = JsonConvert.DeserializeObject<List<SanPhamYeuThichViewModel>>(apiData);
                return View(spyts);
            }






        }

        // GET: SanPhamYeuThichController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanPhamYeuThichController/Create
        // Người dùng thêm sản phẩm vào danh sách yêu thích
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAddProduct(/*Guid userId,*/string productId/*,SanPhamYeuThich spyt*/)
        {
            try
            {
                var userName = HttpContext.Session.GetString("username");
                var idnguoidung = HttpContext.Session.GetString("IdNguoiDung");
                if (!string.IsNullOrEmpty(idnguoidung))
                {
                    // Cách dùng obj
                    string requestURL =
                        $"https://localhost:7050/api/SanPhamYeuThich/Them-mot-san-pham-vao-danh-sach-yeu-thich-cua-nguoi-dung?userId={Guid.Parse(idnguoidung)}&productId={Guid.Parse(productId)}"; // truyền bằng object
                    var httpClient = new HttpClient();
                    //var obj = JsonConvert.SerializeObject(spyt);
                    var response = await httpClient.PostAsync(requestURL, null); // lấy response
                    var jsondata = await response.Content.ReadAsStringAsync();                                                           // Đọc từ response chuỗi Json là kết quả của phép trả về
                    if (response.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("Index");

                        return Content(jsondata, "application/json");
                    }
                    else /*return BadRequest(response);*/ return Content(jsondata, "application/json");
                }
                else return Json(new { code = 400, message = "Bạn cần phải đăng nhập !" });

            }
            catch
            {
                return View();
            }
        }

        // GET: SanPhamYeuThichController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SanPhamYeuThichController/Delete/5
        // Người dùng xóa sản phẩm khỏi danh sách yêu thích
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDeleteSPYT(string spytId)
        {
            try
            {
                string requestURL =
                $"https://localhost:7050/api/SanPhamYeuThich/Xoa-mot-san-pham-khoi-danh-sach-yeu-thich?id={Guid.Parse(spytId)}";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(requestURL); // lấy response
                                                                      // Đọc từ response chuỗi Json là kết quả của phép trả về
                string apiData = await response.Content.ReadAsStringAsync();
                // Có data rồi thì ta sẽ convert về dữ liệu mình cần để đưa sang view
                if (response.IsSuccessStatusCode)
                {
                    //return RedirectToAction("Index");
                    return Content(apiData, "application/json");
                }
                else /*return BadRequest(response);*/ return Content(apiData, "application/json");

            }
            catch
            {
                return View();
            }
        }
    }
}
