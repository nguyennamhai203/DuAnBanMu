using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;
using Shop_Models.ViewModels.Vouchers;

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
        public async Task<IActionResult> GetAllVoucher()
        {
            var vouchers = await _httpClient.GetFromJsonAsync<IEnumerable<Voucher>>("https://localhost:7050/api/Voucher/getAllVoucher");
            return Json(vouchers);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            return RedirectToAction("Index", new { id = id });
        }
        public async Task<IActionResult> Create(CreateVoucher voucher)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7050/api/Voucher/post-voucher", voucher);
            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }
        public async Task<IActionResult> Update(Guid id, Voucher voucher)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7050/api/Voucher/put-voucher/{id}", voucher);
            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }
        public async Task<IActionResult> Delete(Guid id)
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
    }
}
