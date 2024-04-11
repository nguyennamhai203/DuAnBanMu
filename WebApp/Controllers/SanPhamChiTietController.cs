using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Data.SqlTypes;
using System.Net.Http;

namespace WebApp.Controllers
{
    public class SanPhamChiTietController : Controller
    {
        private readonly ILogger<SanPhamChiTietController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public SanPhamChiTietController(ILogger<SanPhamChiTietController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? sumguid)
        {

            //var client = _httpClientFactory.CreateClient("BeHat");

            //var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/GetItemShopViewModelAsync?Id="+ sumguid);
            //if (lstRelatedProducts.IsSuccessStatusCode)
            //{
            //    var jsonResponse = await lstRelatedProducts.Content.ReadAsStringAsync();
            //    var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
            //    var content = JsonConvert.DeserializeObject<List<SPDanhSachViewModel>>(respone.Content.ToString());

            //    //var content2 = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(jsonResponse.ToString());   
            //    var data = content.FirstOrDefault();
            //    return View(content);
            //}
            //return View();

            var client = _httpClientFactory.CreateClient("BeHat");

            var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/GetItemDetailViewModelAynsc?Id=" + sumguid);
            if (lstRelatedProducts.IsSuccessStatusCode)
            {
                var jsonResponse = await lstRelatedProducts.Content.ReadAsStringAsync();
                var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
                var content = JsonConvert.DeserializeObject<ItemDetailViewModel>(respone.Content.ToString());
                ViewData["SumGuid"] = sumguid;
                //var content2 = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(jsonResponse.ToString());   
                return View(content);
            }

            return View();


        }
        [HttpGet]
        public async Task<IActionResult> SanPhamDetailPatialView(string? sumguid)
        {
            //var client = _httpClientFactory.CreateClient("BeHat");

            //var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/GetItemShopViewModelAsync?Id="+ sumguid);
            //if (lstRelatedProducts.IsSuccessStatusCode)
            //{
            //    var jsonResponse = await lstRelatedProducts.Content.ReadAsStringAsync();
            //    var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
            //    var content = JsonConvert.DeserializeObject<List<SPDanhSachViewModel>>(respone.Content.ToString());

            //    //var content2 = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(jsonResponse.ToString());   
            //    var data = content.FirstOrDefault();
            //    return View(content);
            //}
            //return View();

            var client = _httpClientFactory.CreateClient("BeHat");

            var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/GetItemDetailViewModelAynsc?Id=" + sumguid);

            var jsonResponse = await lstRelatedProducts.Content.ReadAsStringAsync();
            var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
            var content = JsonConvert.DeserializeObject<ItemDetailViewModel>(respone.Content.ToString());

            //var content2 = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(jsonResponse.ToString());   
            return PartialView("/Views/SanPhamChiTiet/_SanPhamDetailPatialView.cshtml", content);


        }

        public async Task<IActionResult> GetItemDetailViewModelWhenSelectColor([FromQuery] string id, [FromQuery] string mauSac)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var response = await client.GetStringAsync($"/api/ChiTietSanPham/Get-ItemDetailViewModel/{id}/{mauSac}");
            var content = JsonConvert.DeserializeObject<ItemDetailViewModel>(response.ToString());

            return Ok(content);
        }


        //[HttpPost]
        //public async Task<IActionResult> Details([FromBody] ParametersTongQuanDanhSach? parameters)
        //{

        //    var client = _httpClientFactory.CreateClient("BeHat");

        //    var lstRelatedProducts = await client.PostAsJsonAsync($"/api/ChiTietSanPham/GetFilteredDaTaDSTongQuanAynsc", parameters);
        //    if (lstRelatedProducts.IsSuccessStatusCode)
        //    {
        //        var jsonResponse = await lstRelatedProducts.Content.ReadAsStringAsync();
        //        var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
        //        var content = JsonConvert.DeserializeObject<List<SPDanhSachViewModel>>(respone.Content.ToString());

        //        //var content2 = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(jsonResponse.ToString());
        //        var data = content.FirstOrDefault();
        //        return View(content);
        //    }
        //    return View();


        //}



        [HttpGet("GetRelatedProducts")]
        public async Task<IActionResult> GetRelatedProducts(string id)
        {
            var client = _httpClientFactory.CreateClient("BeHat");

            var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/Get-List-RelatedProduct?sumGuild={id}");
            //ViewData["MauSac"] = new SelectList(lstRelatedProducts!.Select(x => x.TenMauSac).Distinct());
            //if (!string.IsNullOrEmpty(mauSac))
            //{
            //    ViewData["ValueMauSac"] = mauSac;
            //    lstRelatedProducts = lstRelatedProducts!.Where(it => it.TenMauSac!.ToLower() == mauSac.ToLower()).ToList();
            //}

            ViewBag.SumGuid = id;

            if (lstRelatedProducts.IsSuccessStatusCode)
            {
                var jsonResponse = await lstRelatedProducts.Content.ReadAsStringAsync();

                //var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
                var content = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(jsonResponse.ToString());
                // Lấy số lượng bản ghi từ phản hồi và truyền nó qua ViewBag

                return PartialView("/Views/SanPhamChiTiet/_getRelatedProduct.cshtml", content!);
            }

            else
            {
                return StatusCode((int)lstRelatedProducts.StatusCode, lstRelatedProducts.ReasonPhrase);
            }
        }
    }
}
