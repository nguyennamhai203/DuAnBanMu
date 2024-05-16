using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http;

namespace WebApp.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly ILogger<HoaDonController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public HoaDonController(ILogger<HoaDonController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ShowBill(string? invoiceCode)
        {

            try
            {

                using (var client = _httpClientFactory.CreateClient("BeHat"))
                {
                    HttpResponseMessage response = await client.GetAsync($"/PGetBillByInvoiceCode?invoiceCode={invoiceCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                        var billS = JsonConvert.DeserializeObject<HoaDonDto>(resultResponse.Content.ToString());
                        if (billS != null)
                        {
                            ViewBag.Bill = billS;
                            ViewBag.ListBillItem = billS.BillDetail;
                        }
                        else
                        {
                            ViewBag.ListBillItem = null;
                        }
                    }
                    else
                    {
                        ViewBag.ListBillItem = null;
                    }
                    return View();
                }

            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        public async Task<ActionResult<List<HoaDon>>> GetCustomerPurchaseHistory(string customerId)
        {
            // Tạo một HTTP client từ factory
            var client = _httpClientFactory.CreateClient();

            // Gọi endpoint của controller MVC
            var response = await client.GetAsync($"https://localhost:7050/GetPurchaseHistory?customerId={customerId}");

            if (response.IsSuccessStatusCode)
            {
                var purchaseHistory = await response.Content.ReadAsStringAsync(); // Sử dụng phương thức mở rộng ReadFromJsonAsync()

                var jsondata = JsonConvert.DeserializeObject<List<HoaDon>>(purchaseHistory.ToString());
                return View(jsondata);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Không thể lấy dữ liệu lịch sử mua hàng.");
            }
        }
    }
}
