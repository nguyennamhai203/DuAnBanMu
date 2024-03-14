using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shop_Models.Dto;
using System.Net.Http;

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
        public async Task<IActionResult> GetTongQuanDanhSach([FromBody] ParametersTongQuanDanhSach parameters)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                var response = await client.PostAsJsonAsync("/api/SanPhamChiTiet/GetFilteredDaTaDSTongQuanAynsc", parameters);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Content(jsonResponse, "application/json");
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
        }


        public async Task<IActionResult> DanhSachTongQuanSanPham()
        {
            //ViewData["IdChatLieu"] = new SelectList(await _sanPhamChiTietService.GetListModelChatLieuAsync(), "IdChatLieu", "TenChatLieu");
            //ViewData["IdKieuDeGiay"] = new SelectList(await _sanPhamChiTietService.GetListModelKieuDeGiayAsync(), "IdKieuDeGiay", "TenKieuDeGiay");
            //ViewData["IdLoaiGiay"] = new SelectList(await _sanPhamChiTietService.GetListModelLoaiGiayAsync(), "IdLoaiGiay", "TenLoaiGiay");
            //ViewData["IdSanPham"] = new SelectList(await _sanPhamChiTietService.GetListModelSanPhamAsync(), "IdSanPham", "TenSanPham");
            //ViewData["IdThuongHieu"] = new SelectList(await _sanPhamChiTietService.GetListModelThuongHieuAsync(), "IdThuongHieu", "TenThuongHieu");
            //ViewData["IdXuatXu"] = new SelectList(await _sanPhamChiTietService.GetListModelXuatXuAsync(), "IdXuatXu", "Ten");
            return PartialView();
        }

    }

   
}
