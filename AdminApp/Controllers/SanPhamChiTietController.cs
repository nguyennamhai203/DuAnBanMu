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

        public SanPhamChiTietController(ILogger<SanPhamChiTietController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
        }

        public async Task<IActionResult> _ProductListPartial()
        {
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



        public async Task<IActionResult> DanhSachSanPhamDangKinhDoanh(int status = 1)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                string response = await client.GetStringAsync($"/api/ChiTietSanPham/GetAll?status={status}");
                if (!string.IsNullOrEmpty(response))
                {
                    var productList = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(response);
                    return View(productList);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login","Home");
            }
        }
        public async Task<IActionResult> DanhSachSanPhamNgungKinhDoanh(int status = 0)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                string response = await client.GetStringAsync($"/api/ChiTietSanPham/GetAll?status={status}");
                if (!string.IsNullOrEmpty(response))
                {
                    var productList = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(response);
                    return View(productList);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
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

            var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/Get-List-RelatedProduct?sumGuild={sumGuid}");
            //ViewData["MauSac"] = new SelectList(lstRelatedProducts!.Select(x => x.TenMauSac).Distinct());
            //if (!string.IsNullOrEmpty(mauSac))
            //{
            //    ViewData["ValueMauSac"] = mauSac;
            //    lstRelatedProducts = lstRelatedProducts!.Where(it => it.TenMauSac!.ToLower() == mauSac.ToLower()).ToList();
            //}

            ViewBag.SumGuid = sumGuid;

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
        public async Task<IActionResult?> LoadPartialView(Guid idSanPhamChiTiet) // Detail sản phẩm chi tiết
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

        public async Task<IActionResult?> _SanPhamUpdatePartialView(Guid idSanPhamChiTiet)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var lstRelatedProducts = await client.GetAsync($"/api/ChiTietSanPham/DetailSanPhamChiTietDto?Id={idSanPhamChiTiet}");
            if (lstRelatedProducts.IsSuccessStatusCode)
            {


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

                var jsonResponse = await lstRelatedProducts.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<SanPhamChiTietDto>(jsonResponse);
                // Lấy số lượng bản ghi từ phản hồi và truyền nó qua ViewBag

                return PartialView("/Views/SanPhamChiTiet/_SanPhamUpdatePartialView.cshtml", content);
            }

            else
            {
                return StatusCode((int)lstRelatedProducts.StatusCode, lstRelatedProducts.ReasonPhrase);
            }

        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(SanPhamChiTietDto productRequest, bool TrangThaiKhuyenMai, string editor, [FromForm] List<IFormFile> formFiles)
        {
            ChiTietSanPham productDetail = new ChiTietSanPham()
            {
                Id = productRequest.Id,
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
            var response = await client.PutAsJsonAsync($"/api/ChiTietSanPham/UpdateAsync", productDetail);

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
                var resultdto = JsonConvert.DeserializeObject<ResponseDto>(result);
                HttpContext.Session.SetString("ResultUpdate", resultdto.Message);

                return Content(result, "application/json");
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var resultdto = JsonConvert.DeserializeObject<ResponseDto>(result);
                HttpContext.Session.SetString("ResultUpdate", resultdto.Message);

                return Content(result, "application/json");
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                var response = await client.DeleteAsync($"/api/ChiTietSanPham/Ngung-kinh-doanh-san-pham/{id}");
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Not IsSuccessStatusCode");
            }
        } 
        
        public async Task<IActionResult> KhoiPhuc(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                var response = await client.DeleteAsync($"/api/ChiTietSanPham/Khoi-phuc-kinh-doanh-san-pham/{id}");
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Not IsSuccessStatusCode");
            }
        }

    }
}
