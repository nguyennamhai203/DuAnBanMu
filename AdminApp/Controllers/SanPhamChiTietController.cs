using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{
    public class SanPhamChiTietController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;


        public SanPhamChiTietController(IHttpClientFactory httpClientFactory, ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            _httpClientFactory = httpClientFactory;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
        }

        //public async Task<IActionResult> Index()
        //{

        //    var client = _httpClientFactory.CreateClient("BeHat");
        //    //var accessToken = Request.Cookies["account"];
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //    //string jwtToken = HttpContext.Session.GetString("AccessToken");
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        //    string result = await client.GetStringAsync($"/api/ChiTietSanPham/PGetProductDetail");
        //    return View(result);
        //}


        //public async Task<IActionResult> Index(int? page,int? PageSize)
        //{
        //    var client = _httpClientFactory.CreateClient("BeHat");
        //    string result = await client.GetStringAsync($"/api/ChiTietSanPham/PGetProductDetail");

        //    // Deserialize chuỗi JSON thành một danh sách SanPhamChiTietDto
        //    List<SanPhamChiTietDto> products = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(result);

        //    // Chuyển đổi danh sách sản phẩm thành một chuỗi HTML
        //    //string productListHtml = RenderPartialViewToString("_ProductListPartial", products);

        //    // Truyền chuỗi HTML vào ViewDataDictionary
        //    //ViewData["ProductListHtml"] = productListHtml;

        //    // Trả về view
        //    return View(products);
        //}

        //private string RenderPartialViewToString(string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = ControllerContext.ActionDescriptor.ActionName;
        //    ViewData.Model = model;

        //    using (var sw = new StringWriter())
        //    {
        //        ViewEngineResult viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

        //        ViewContext viewContext = new ViewContext(
        //        ControllerContext,
        //            viewResult.View,
        //            ViewData,
        //            new TempDataDictionary(ControllerContext.HttpContext, _tempDataProvider),
        //            sw,
        //            new HtmlHelperOptions()
        //        );

        //        viewResult.View.RenderAsync(viewContext);
        //        return sw.ToString();
        //    }
        //}



        //public async Task<IActionResult> Index(/*int? page, int? pagesize*/)
        //{
        //    return View();
        //}

        public async Task<IActionResult> _ProductListPartial()
        {
            //ViewData["IdChatLieu"] = new SelectList(await _sanPhamChiTietService.GetListModelChatLieuAsync(), "IdChatLieu", "TenChatLieu");
            //ViewData["IdKieuDeGiay"] = new SelectList(await _sanPhamChiTietService.GetListModelKieuDeGiayAsync(), "IdKieuDeGiay", "TenKieuDeGiay");
            //ViewData["IdLoaiGiay"] = new SelectList(await _sanPhamChiTietService.GetListModelLoaiGiayAsync(), "IdLoaiGiay", "TenLoaiGiay");
            //ViewData["IdSanPham"] = new SelectList(await _sanPhamChiTietService.GetListModelSanPhamAsync(), "IdSanPham", "TenSanPham");
            //ViewData["IdThuongHieu"] = new SelectList(await _sanPhamChiTietService.GetListModelThuongHieuAsync(), "IdThuongHieu", "TenThuongHieu");
            //ViewData["IdXuatXu"] = new SelectList(await _sanPhamChiTietService.GetListModelXuatXuAsync(), "IdXuatXu", "Ten");
            return PartialView();
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetDanhSachSanPham(int? pageSize)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                string response = await client.GetStringAsync($"/api/ChiTietSanPham/PGetProductDetail?PageSize={pageSize}");
                if (response != null)
                {
                    // Parse JSON string to object
                    var productList = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(response);

                    // Return JSON data
                    return Json(productList);
                }
                return NoContent(); // If response is null, return 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //public async Task<IActionResult> GetDanhSachSanPham(int? pageSize)
        //{
        //    try
        //    {
        //        var client = _httpClientFactory.CreateClient("BeHat");
        //        string response = await client.GetStringAsync($"/api/ChiTietSanPham/PGetProductDetail?PageSize={pageSize}");
        //        if (response != null)
        //        {
        //            // Trả về dữ liệu dưới dạng JsonResult
        //            return Json(response);
        //        }
        //        return Content(response, "application/json");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //}


    }
}
