using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{
    public class SanPhamChiTietController : Controller
    {
        private readonly ILogger<SanPhamChiTietController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly ICompositeViewEngine _viewEngine;
        //private readonly ITempDataProvider _tempDataProvider;


        public SanPhamChiTietController(ILogger<SanPhamChiTietController> logger, IConfiguration config, IHttpClientFactory httpClientFactory/*, ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider*/)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
            //_viewEngine = viewEngine;
            //_tempDataProvider = tempDataProvider;
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
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
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

        public IActionResult ChiTietSanPham()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DanhSachTongQuanSanPham([FromBody] ParametersTongQuanDanhSach parameters)
        {
            try
            {
                var accessToken = HttpContext.Session.GetString("AccessToken");
                var token = HttpContext.Session.GetString("TokenCheck");
                var client = _httpClientFactory.CreateClient("BeHat");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsJsonAsync("/api/ChiTietSanPham/GetFilteredDaTaDSTongQuanAynsc", parameters);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var respone = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
                    var content = JsonConvert.DeserializeObject<List<SPDanhSachViewModel>>(respone.Content.ToString());
                    // Lấy số lượng bản ghi từ phản hồi và truyền nó qua ViewBag
                    ViewBag.TotalRecords = respone.Count;
                    ViewBag.TotalPage = respone.TotalPage;
                    return PartialView("/Views/SanPhamChiTiet/_DanhSachTongQuanSanPham.cshtml", content);
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

        [HttpGet("GetRelatedProducts")]
        public async Task<IActionResult> GetRelatedProducts(string sumGuid)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            //var response = await client.PostAsJsonAsync("/api/ChiTietSanPham/GetFilteredDaTaDSTongQuanAynsc", parameters);

            var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/Get-List-RelatedProduct?sumGuild={sumGuid}");
            //ViewData["MauSac"] = new SelectList(lstRelatedProducts!.Select(x => x.TenMauSac).Distinct());
            //if (!string.IsNullOrEmpty(mauSac))
            //{
            //    ViewData["ValueMauSac"] = mauSac;
            //    lstRelatedProducts = lstRelatedProducts!.Where(it => it.TenMauSac!.ToLower() == mauSac.ToLower()).ToList();
            //}
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
        public async Task<IActionResult?> LoadPartialView(Guid idSanPhamChiTiet)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/DetailSanPhamChiTietDto?Id={idSanPhamChiTiet}");
            if (lstRelatedProducts.IsSuccessStatusCode)
            {
                var jsonResponse = await lstRelatedProducts.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<SanPhamChiTietDto>(jsonResponse);
                // Lấy số lượng bản ghi từ phản hồi và truyền nó qua ViewBag

                return PartialView("/Views/SanPhamChiTiet/_DetailSanPhamChiTietDto.cshtml", content);
            }

            else
            {
                return StatusCode((int)lstRelatedProducts.StatusCode, lstRelatedProducts.ReasonPhrase);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Createaa(SanPhamChiTietDto productRequest, string editor, [FromForm] List<IFormFile> formFiles)
        {
            ChiTietSanPham productDetail = new ChiTietSanPham()
            {
                Id = Guid.NewGuid(),
                MaSanPham = productRequest.MaSanPhamChiTiet,
                GiaNhap = (float)productRequest.GiaNhap,
                GiaBan = (float)productRequest.GiaBan,
                SoLuongTon = productRequest.SoLuongTon,
                //TrangThaiKhuyenMai = productRequest.TrangThaiKhuyenMai,
                //Mota = editor,
                TrangThai = 1,
                SanPhamId = productRequest.SanPhamId,
                MauSacId = productRequest.MauSacId == Guid.Empty ? null : productRequest.MauSacId,
                ThuongHieuId = productRequest.ThuongHieuId == Guid.Empty ? null : productRequest.ThuongHieuId,
                XuatXuId = productRequest.XuatXuId == Guid.Empty ? null : productRequest.XuatXuId,
                ChatLieuId = productRequest.ChatLieuId == Guid.Empty ? null : productRequest.ChatLieuId,
                LoaiId = productRequest.LoaiId == Guid.Empty ? null : productRequest.LoaiId,

            };

            var client = _httpClientFactory.CreateClient("BeHat");
            var accessToken = HttpContext.Session.GetString("TokenCheck");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.PostAsJsonAsync($"/api/ChiTietSanPham/CreateAsync", productDetail);

            //string message = await response.Content.ReadAsStringAsync(); // Lấy thông báo từ nội dung
            using (var formData = new MultipartFormDataContent())
            {
                foreach (var file in formFiles)
                {
                    formData.Add(new StreamContent(file.OpenReadStream())
                    {
                        Headers =
            {
                ContentLength = file.Length,
                ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType)
            }
                    }, "files", file.FileName);
                }

                // Add other data if needed
                formData.Add(new StringContent(productDetail.Id.ToString()), "ProductId");

                // Send the request
                var resultImage = await client.PostAsync("/uploadManyProductDetailImages", formData);
            }


            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                return Content(result, "application/json");
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();

                return Content(result, "application/json");
            }
        }
        public async Task<IActionResult> CreatePartialView()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                string getChatLieu = await client.GetStringAsync($"/api/ChatLieu/GetAll");
                ViewBag.GetChatLieu = JsonConvert.DeserializeObject<List<ChatLieu>>(getChatLieu);
                string getLoai = await client.GetStringAsync($"/api/Loai");
                ViewBag.GetLoai = JsonConvert.DeserializeObject<List<Loai>>(getLoai);
                string getColor = await client.GetStringAsync($"/Get-All-MauSac");
                ViewBag.GetColor = JsonConvert.DeserializeObject<List<MauSac>>(getColor);
                //listColor = JsonConvert.DeserializeObject<List<Color>>(getColor);
                string getSanPham = await client.GetStringAsync($"/api/SanPham/GetAll");
                ViewBag.GetSanPham = JsonConvert.DeserializeObject<List<SanPham>>(getSanPham);
                string getThuongHieu = await client.GetStringAsync($"/api/ThuongHieu");
                ViewBag.GetThuongHieu = JsonConvert.DeserializeObject<List<ThuongHieu>>(getThuongHieu);
                //listCpu = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);
                string getXuatXu = await client.GetStringAsync($"/api/XuatXu/GetAll");
                ViewBag.GetXuatXu = JsonConvert.DeserializeObject<List<XuatXu>>(getXuatXu);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }
        }
    }
}
