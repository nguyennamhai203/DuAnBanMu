using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public HoaDonController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task<IActionResult> DanhSachHoaDon()
        {
            //var response = await _httpClient.GetAsync("https://localhost:7050/api/Get-All-HoaDon");
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    var hoaDons = JsonConvert.DeserializeObject<List<HoaDon>>(content);
            //    return View(hoaDons);
            //}
            //else
            //{
            //    // Handle error
            //    return View(new List<HoaDon>());
            //}
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
            // Retrieve HoaDon details from API
            var response = await _httpClient.GetAsync($"https://localhost:7050/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var hoaDon = JsonConvert.DeserializeObject<HoaDon>(content);
                return View(hoaDon);
            }
            else
            {
                // Handle error
                return RedirectToAction("DanhSachHoaDon");
            }
        }

        public async Task<IActionResult> CancelOrder(Guid id)
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


        //[HttpPost("SendCancelOrderRequest")]
        public async Task<IActionResult> SendCancelOrderRequest(Guid id, string reason)
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

        public async Task<IActionResult> UpdateTrangThaiGiaoHangHoaDon(Guid id, Guid? idNguoiDung, int TrangThaigiaohang, string? Lido, DateTime? ngayCapNhatGanNhat)
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
