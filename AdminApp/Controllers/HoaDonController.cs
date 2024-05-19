using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Api.Migrations;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Shop_Models.Heplers.TrangThai;

namespace AdminApp.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HoaDonController> _logger;

        public HoaDonController(HttpClient httpClient, ILogger<HoaDonController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IActionResult> DanhSachHoaDon()
        {
           
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
               var response = await _httpClient.GetAsync("https://localhost:7050/Get-All-HoaDon");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var hoaDons = JsonConvert.DeserializeObject<List<HoaDon>>(content);
                return View(hoaDons); // Trả về View với danh sách hoaDons
            }
            else
            {
                // Xử lý lỗi
                return View(new List<HoaDon>());
            }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }


        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HoaDon hoaDon)
        {
            // Xóa thuộc tính MaHoaDon để không gửi lên server
            hoaDon.MaHoaDon = null;
            // Chuyển đổi đối tượng HoaDon thành chuỗi JSON
            var json = JsonConvert.SerializeObject(hoaDon);
            // Tạo đối tượng StringContent từ chuỗi JSON
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Gửi yêu cầu POST đến API với nội dung là chuỗi JSON
            var response = await _httpClient.PostAsync("https://localhost:7050/Create-HoaDon", content);
            if (response.IsSuccessStatusCode)
            {
                // HoaDon created successfully, chuyển hướng đến action "DanhSachHoaDon"
                return RedirectToAction("DanhSachHoaDon");
            }
            else
            {
                // Xử lý lỗi nếu yêu cầu không thành công
                return View(response);
            }
        }




        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrieve HoaDon data from API for editing
            var response = await _httpClient.GetAsync($"https://localhost:7050/GetById?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var hoaDon = JsonConvert.DeserializeObject<HoaDon>(content);
                return View(hoaDon);
            }
            else
            {
                // Handle error or redirect to danh sach hoa don page
                return RedirectToAction("DanhSachHoaDon");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, HoaDon hoaDon)
        {
            // Implement logic to update HoaDon data to API
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7050/Update-HD?Id={id}", hoaDon);

            if (response.IsSuccessStatusCode)
            {
                // HoaDon updated successfully
                return RedirectToAction("DanhSachHoaDon");
            }
            else
            {
                // Handle error
                return View(hoaDon);
            }
        }


        public async Task<IActionResult> Details(Guid id)
        {           
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                try
                {
                    var hoaDonResponse = await _httpClient.GetAsync($"https://localhost:7050/GetById?id={id}");
                    if (!hoaDonResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("DanhSachHoaDon");
                    }

                    var hoaDonContent = await hoaDonResponse.Content.ReadAsStringAsync();
                    var hoaDon = JsonConvert.DeserializeObject<HoaDon>(hoaDonContent);

                    var chiTietResponse = await _httpClient.GetAsync($"https://localhost:7050/api/HoaDonCT?hoaDonId={id}");
                    if (!chiTietResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("DanhSachHoaDon");
                    }

                    var chiTietContent = await chiTietResponse.Content.ReadAsStringAsync();
                    var chiTietHoaDon = JsonConvert.DeserializeObject<List<HoaDonChiTiet>>(chiTietContent);

                    var viewModel = new Tuple<HoaDon, List<HoaDonChiTiet>>(hoaDon, chiTietHoaDon);


                    return View(viewModel);
                }
                catch (Exception ex)
                {
                    // Handle exception
                    return RedirectToAction("DanhSachHoaDon");
                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }



        }


        public async Task<IActionResult> CancelOrder(Guid id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/GetById?id={id}";
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<HoaDon>(jsonString);


                    return View(result);
                }
                else return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }


        }


        //[HttpPost("SendCancelOrderRequest")]
        public async Task<IActionResult> SendCancelOrderRequest(Guid id, string reason)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                // Gửi yêu cầu PUT đến API để hủy hóa đơn
                var response = await _httpClient.PostAsync($"https://localhost:7050/CancelOrder?id={id}&reason={reason}", null);

                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    // Hủy hóa đơn thành công, trả về một thông điệp thành công
                    return Content(result, "application/json");
                }
                else
                {
                    // Yêu cầu không thành công, trả về một thông điệp lỗi
                    //return Json(new { success = false,code=400, errorMessage = "Không thể hủy đơn hàng. Vui lòng thử lại sau." });
                    return Content(result, "application/json");

                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }


        }


        public async Task<IActionResult> UpdateNgayHoaDon(Guid idHoaDon, DateTime? NgayThanhToan, DateTime? NgayNhan, DateTime? NgayShip)
        {
            // Prepare data to be sent to the API
            var data = new
            {
                IdHoaDon = idHoaDon,
                NgayThanhToan = NgayThanhToan,
                NgayNhan = NgayNhan,
                NgayShip = NgayShip
            };

            // Send the data to the API to update NgayHoaDon
            var response = await _httpClient.PutAsJsonAsync("https://localhost:7050/UpdateNgayHoaDon", data);
            if (response.IsSuccessStatusCode)
            {
                // NgayHoaDon updated successfully
                return RedirectToAction("DanhSachHoaDon");
            }
            else
            {
                // Handle error
                return RedirectToAction("DanhSachHoaDon");
            }
        }
        public async Task<IActionResult> DanhSachHoaDonTheoTrangThai(int trangThai)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var url = $"https://localhost:7050/DanhSachTheoTrangThai?trangThai={trangThai}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var danhSachHoaDon = JsonConvert.DeserializeObject<List<HoaDon>>(jsonString);

                    return View(danhSachHoaDon);
                }
                else
                {
                    // Handle error
                    return View("Error");
                }

            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }

        public async Task<IActionResult> UpdateTrangThaiGiaoHangHoaDon(Guid id, Guid? idNguoiDung, int TrangThaigiaohang, string? Lido, DateTime? ngayCapNhatGanNhat)
        {


            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                try
                {
                    // Prepare data to be sent to the API
                    var data = new
                    {
                        Id = id,
                        IdNguoiDung = idNguoiDung,
                        TrangThaiGiaoHang = TrangThaigiaohang,
                        LiDoHuy = Lido,
                        NgayCapNhatGanNhat = ngayCapNhatGanNhat
                    };

                    // Send the data to the API to update TrangThaiGiaoHangHoaDon
                    var json = System.Text.Json.JsonSerializer.Serialize(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"https://localhost:7050/UpdateTrangThaiGiaoHangHoaDon?id={id}&TrangThai={TrangThaigiaohang}&Lido={Lido}&ngayCapNhatGanNhat={ngayCapNhatGanNhat}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // TrangThaiGiaoHangHoaDon updated successfully

                        // Check if the TrangThai is DaGiaoHang, then update ThanhToan
                        if (TrangThaigiaohang == (int)TrangThaiGiaoHang.DaGiaoHang)
                        {
                            // Prepare data to be sent to the API to update ThanhToan
                            var thanhToanData = new
                            {
                                Id = id,
                                TrangThaiThanhToan = 1 // Đánh dấu là đã thanh toán
                            };

                            var thanhToanJson = System.Text.Json.JsonSerializer.Serialize(thanhToanData);
                            var thanhToanContent = new StringContent(thanhToanJson, Encoding.UTF8, "application/json");

                            // Send the data to the API to update ThanhToan
                            var thanhToanResponse = await _httpClient.PostAsync("https://localhost:7050/UpdateThanhToan", thanhToanContent);

                            if (!thanhToanResponse.IsSuccessStatusCode)
                            {
                                // Handle error if updating ThanhToan fails
                                return RedirectToAction("DanhSachHoaDon");
                            }
                        }

                        return RedirectToAction("DanhSachHoaDon");
                    }
                    else
                    {
                        // Handle error
                        return RedirectToAction("DanhSachHoaDon");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
         
        }
        public async Task<IActionResult> DanhSachPhanTrangThai(int? trangThaigiaohang)
        {

            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                try
                {
                    if (trangThaigiaohang == null)
                    {
                        trangThaigiaohang = 0;
                    }
                    if (trangThaigiaohang.HasValue)
                    {
                        var responseAll = await _httpClient.GetAsync("https://localhost:7050/HoaDonStatus");
                        if (responseAll.IsSuccessStatusCode)
                        {
                            var contentAll = await responseAll.Content.ReadAsStringAsync();
                            var hoaDons = JsonConvert.DeserializeObject<List<HoaDon>>(contentAll);
                            return View(hoaDons);
                        }
                        else
                        {
                            _logger.LogError($"API returned error code: {responseAll.StatusCode}");
                            var errorMessage = "Đã xảy ra lỗi khi gọi API. Không thể lấy dữ liệu.";
                            return View();
                        }
                    }
                    else
                    {
                        var hoaDonStatus = (TrangThaiGiaoHang)trangThaigiaohang.Value;
                        var baseUrl = "https://localhost:7050/HoaDonStatus";
                        var url = new Uri($"{baseUrl}?HoaDonStatus={(int)hoaDonStatus}");

                        var response = await _httpClient.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var hoaDons = JsonConvert.DeserializeObject<List<HoaDon>>(content);
                            return View(hoaDons);
                        }
                        else
                        {
                            _logger.LogError($"API returned error code: {response.StatusCode}");
                            var errorMessage = "Đã xảy ra lỗi khi gọi API. Không thể lấy dữ liệu.";
                            return View("Error", new { error = errorMessage });
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred: {ex.Message}");
                    var errorMessage = "Đã xảy ra lỗi khi xử lý yêu cầu.";
                    return View("Error", new { error = errorMessage });
                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

          
        }



        public async Task<IActionResult> DanhSachPhanTrangThai2(int? trangThaigiaohang)
        {


            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                try
                {
                    if (trangThaigiaohang == null)
                    {
                        trangThaigiaohang = 1;
                    }
                    if (trangThaigiaohang.HasValue)
                    {
                        int hoaDonStatus = trangThaigiaohang.Value;

                        var baseUrl = "https://localhost:7050/HoaDonStatus";
                        var url = new Uri($"{baseUrl}?HoaDonStatus={hoaDonStatus}");

                        _logger.LogInformation($"Calling API with URL: {url}");

                        var response = await _httpClient.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var hoaDons = JsonConvert.DeserializeObject<List<HoaDon>>(content);

                            return PartialView("_DanhSachPhanTrangThaiPartial", hoaDons); // Trả về dữ liệu dưới dạng JSON
                        }
                        else
                        {
                            _logger.LogError($"API returned error code: {response.StatusCode}");

                            var errorMessage = "Đã xảy ra lỗi khi gọi API. Không thể lấy dữ liệu.";
                            ViewData["ErrorMessage"] = errorMessage;
                        }
                    }
                    else
                    {
                        var errorMessage = "Trạng thái không được chỉ định.";
                        ViewData["ErrorMessage"] = errorMessage;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi xảy ra khi xử lý yêu cầu DanhSachPhanTrangThai");

                    var errorMessage = "Đã xảy ra lỗi trong quá trình xử lý yêu cầu. Vui lòng thử lại sau.";
                    ViewData["ErrorMessage"] = errorMessage;
                }

                // Trả về view với danh sách hóa đơn trống
                return Json(null); // Trả về dữ liệu dưới dạng JSON
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public async Task<IActionResult> UpdateHoaDonStatus(Guid id, int hoaDonStatus)
        {
           



            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var apiUrl = $"https://localhost:7050/updateHoaDonStatus?id={id}&HoaDonStatus={hoaDonStatus}";

                // Tạo object chứa dữ liệu để gửi đến API
                var model = new { Id = id, HoaDonStatus = hoaDonStatus };

                // Chuyển object thành JSON string
                var jsonModel = JsonConvert.SerializeObject(model);

                // Tạo nội dung của yêu cầu
                var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT đến API
                var response = await _httpClient.PutAsync(apiUrl, null);
                var readRespnse = response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ResponseDto>(readRespnse.Result);
                // Xử lý kết quả từ API
                if (result.IsSuccess)
                {
                    //return Ok(); // Trả về 200 OK nếu yêu cầu thành công
                    return Ok(new { success = true });
                }
                else
                {
                    //return StatusCode((int)response.StatusCode); // Trả về mã lỗi từ API nếu yêu cầu gặp lỗi
                    return Ok(new { success = false, message = result.Message });
                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }





        // public async Task<IActionResult> UpdateTrangThaiGiaoHang(Guid idHoaDon, int TrangThai, string? Lido, DateTime? ngayCapNhatGanNhat)
        //{
        //    try
        //    {
        //        // Tạo đối tượng content để gửi dữ liệu
        //        var content = new StringContent(
        //            $"idHoaDon={idHoaDon}&TrangThai={TrangThai}&Lido={Lido}&ngayCapNhatGanNhat={ngayCapNhatGanNhat}",
        //            Encoding.UTF8,
        //            "application/x-www-form-urlencoded");

        //        // Gửi yêu cầu HTTP POST đến API controller
        //        var response = await _httpClient.PostAsync("/UpdateTrangThaiGiaoHangHoaDon", content);

        //        // Đảm bảo yêu cầu thành công
        //        response.EnsureSuccessStatusCode();

        //        // Đọc và trả về nội dung phản hồi từ API
        //        var responseContent = await response.Content.ReadAsStringAsync();

        //        return Ok(responseContent); // Hoặc xử lý phản hồi theo cách bạn muốn
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        return BadRequest($"Lỗi khi gửi yêu cầu HTTP: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
        //    }
        //}

    }
}
