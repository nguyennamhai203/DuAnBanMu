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
        public async Task<IActionResult> Detail(Guid id)
        {
            return RedirectToAction("Index", new { id = id });
        }
        public async Task<IActionResult> Create(CreateVoucher voucher)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7050/api/Voucher/post-voucher", voucher);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id, Voucher voucher)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7050/api/Voucher/put-voucher/{id}", voucher);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7050/api/Voucher/removeVoucher/{id}");
            return RedirectToAction("Index");
        }
    }
}
