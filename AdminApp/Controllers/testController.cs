using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.SqlTypes;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static System.Net.Mime.MediaTypeNames;

namespace AdminApp.Controllers
{
    public class testController : Controller
    {
        private readonly ILogger<testController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private string? urlApi;
        public testController(ILogger<testController> logger, IConfiguration config, IHttpClientFactory httpClientFactory, ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            _httpClientFactory = httpClientFactory;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;

            _logger = logger;
            _config = config;
            urlApi = _config.GetSection("UrlApiAdmin").Value;
        }
        public IActionResult testChiTietSanPham()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Home");
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
                    return PartialView("/Views/test/_testDanhSachTongQuanSanPham.cshtml", content);
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

        [HttpGet("testGetRelatedProducts")]
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

                return PartialView("/Views/test/_testgetRelatedProduct.cshtml", content!);
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

                return PartialView("/Views/test/_testDetailSanPhamChiTietDto.cshtml", content);
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
                //Description = editor,
                TrangThai = 1,
                SanPhamId = productRequest.SanPhamId,
                MauSacId = productRequest.MauSacId == Guid.Empty ? null : productRequest.MauSacId,
                ThuongHieuId = productRequest.ThuongHieuId == Guid.Empty ? null : productRequest.ThuongHieuId,
                XuatXuId = productRequest.XuatXuId == Guid.Empty ? null : productRequest.XuatXuId,
                ChatLieuId = productRequest.ChatLieuId == Guid.Empty ? null : productRequest.ChatLieuId,
                LoaiId = productRequest.LoaiId == Guid.Empty ? null : productRequest.LoaiId,

            };

            var client = _httpClientFactory.CreateClient("BeHat");
            //var accessToken = Request.Cookies["account"];
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                return View("Login");
            }       
        }

    }


}
