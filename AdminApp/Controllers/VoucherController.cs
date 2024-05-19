using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;
using Shop_Models.ViewModels.Vouchers;
using System.Net.Http;

namespace AdminApp.Controllers
{
    public class VoucherController : Controller
    {
        private readonly HttpClient _httpClient;
        public VoucherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                if (id != Guid.Empty)
                {
                    var voucher = await _httpClient.GetFromJsonAsync<Voucher>($"https://localhost:7050/api/Voucher/get-byid-voucher/{id}");
                    List<Voucher> c = new List<Voucher>();
                    c.Add(voucher);
                    ViewData["Voucher"] = c;
                }
                var vouchers = await _httpClient.GetFromJsonAsync<IEnumerable<Voucher>>("https://localhost:7050/api/Voucher/getAllVoucher");
                return View(vouchers);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }
        public async Task<IActionResult> GetAllVoucher()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var vouchers = await _httpClient.GetFromJsonAsync<IEnumerable<Voucher>>("https://localhost:7050/api/Voucher/getAllVoucher");
                return Json(vouchers);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                return RedirectToAction("Index", new { id = id });
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<IActionResult> Create(CreateVoucher voucher)
        {


            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7050/api/Voucher/post-voucher", voucher);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<IActionResult> Update(Guid id, Voucher voucher)
        {



            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7050/api/Voucher/put-voucher/{id}", voucher);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7050/api/Voucher/removeVoucher/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
    }
}
