using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using WebApp.Models;
using Shop_Models.Dto;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> DanhSachTongQuanSanPham([FromBody] ParametersTongQuanDanhSach parameters)
        //{
        //    try
        //    {
        //        //var accessToken = HttpContext.Session.GetString("AccessToken");
        //        //var token = HttpContext.Session.GetString("TokenCheck");
        //        var client = _httpClientFactory.CreateClient("BeHat");
        //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        var response = await client.PostAsJsonAsync("/api/ChiTietSanPham/GetFilteredDaTaDSTongQuanAynsc", parameters);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var jsonResponse = await response.Content.ReadAsStringAsync();

        //            var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
        //            var content = JsonConvert.DeserializeObject<List<SPDanhSachViewModel>>(respone.Content.ToString());
        //            // Lấy số lượng bản ghi từ phản hồi và truyền nó qua ViewBag
        //            ViewBag.TotalRecords = respone.Count;
        //            ViewBag.TotalPage = respone.TotalPage;
        //            return PartialView("/Views/Home/_DanhSachTongQuanSanPham.cshtml", content);
        //        }

        //        else
        //        {
        //            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //    //return PartialView("_");
        //}

        [HttpGet]
        public async Task<IActionResult> DanhSachTongQuanSanPham(string s)
        {
            try
            {
                //var accessToken = HttpContext.Session.GetString("AccessToken");
                //var token = HttpContext.Session.GetString("TokenCheck");
                var client = _httpClientFactory.CreateClient("BeHat");
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("/api/ChiTietSanPham/GetItemShopViewModelAsync2?Id=123");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
                    var content = JsonConvert.DeserializeObject<List<ItemShopViewModel>>(respone.Content.ToString());
                    // Lấy số lượng bản ghi từ phản hồi và truyền nó qua ViewBag
                    ViewBag.TotalRecords = respone.Count;
                    ViewBag.TotalPage = respone.TotalPage;
                    return PartialView("/Views/Home/_DanhSachTongQuanSanPham.cshtml", content);
                }

                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
            //return PartialView("_");
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