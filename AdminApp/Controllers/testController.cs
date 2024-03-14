using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace AdminApp.Controllers
{
    public class testController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;


        public testController(IHttpClientFactory httpClientFactory, ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            _httpClientFactory = httpClientFactory;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DanhSachTongQuanSanPham([FromBody] ParametersTongQuanDanhSach parameters)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                var response = await client.PostAsJsonAsync("/api/ChiTietSanPham/GetFilteredDaTaDSTongQuanAynsc", parameters);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
                    var content = JsonConvert.DeserializeObject<List<SPDanhSachViewModel>>(respone.Content.ToString());

                    return PartialView("/Views/test/_DanhSachTongQuanSanPham.cshtml", content);
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
            return PartialView("_");
        }

    }

   
}
