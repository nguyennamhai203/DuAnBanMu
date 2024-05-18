﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AdminApp.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly HttpClient httpClient;
        public SanPhamController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> GetAllSP()
        {
            var sp = JsonConvert.DeserializeObject<List<SanPham>>(await (await httpClient.GetAsync("https://localhost:7050/api/SanPham/GetAll")).Content.ReadAsStringAsync());
            return View(sp);
        }
        public async Task<IActionResult> DetailTH(Guid id)
        {
            // Make the HTTP GET request
                var response = await httpClient.GetAsync($"https://localhost:7050/api/SanPham/{id}");

                // Check if the request was successful
                if (!response.IsSuccessStatusCode)
                {
                    // Handle unsuccessful status code
                    return StatusCode((int)response.StatusCode, "Error fetching data from API");
                }

                // Read the response content as a string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON content to a SanPham object
                var sanPham = JsonConvert.DeserializeObject<SanPham>(content);

                // Check if the deserialized object is not null
                if (sanPham == null)
                {
                    // Handle null object
                    return NotFound("Product not found");
                }

                // Return the view with the product
                return View(sanPham);
           
        }

        public ActionResult CreateSP()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSP(SanPham sanPham)
        {
            // Serialize the Loai object to JSON
            var jsonContent = JsonConvert.SerializeObject(sanPham);

            // Create a StringContent with the JSON data
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the POST request with the content
            var response = await httpClient.PostAsync("https://localhost:7050/api/SanPham/CreateAsync", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSP");
            }
            else
            {
                return BadRequest(response);
            }
        }

        public async Task<IActionResult> DeleteSP(Guid id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"https://localhost:7050/api/SanPham/DeleteAsync?Id={id}");

                if (response.IsSuccessStatusCode)
                {
                    // Xóa thành công
                    return RedirectToAction("GetAllSP");
                }
                else
                {
                    // Xóa không thành công, in ra kết quả để debug
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Log hoặc in ra lỗi để kiểm tra
                    Console.WriteLine($"Error deleting SanPham: {responseContent}");

                    // Thêm thông báo lỗi vào ViewData hoặc TempData để hiển thị trong View
                    TempData["ErrorMessage"] = "Không thể xóa sản phẩm. Vui lòng thử lại.";

                    return RedirectToAction("GetAllSP");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public async Task<IActionResult> EditSP(Guid Id)
        {
            // Corrected URL with a slash before the Id
            var response = await httpClient.GetAsync($"https://localhost:7050/api/SanPham/{Id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var loai = JsonConvert.DeserializeObject<SanPham>(jsonString);

                if (loai != null)
                {
                    return View(loai);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSP(SanPham sanpham)
        {
            try
            {
                if (sanpham == null)
                {
                    return BadRequest("Dữ liệu sản phẩm không hợp lệ");
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7050/api/SanPham/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonContent = JsonConvert.SerializeObject(sanpham);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Gửi yêu cầu PUT đến API
                    var response = await client.PutAsync($"UpdateAsync", content);

                    // Xử lý phản hồi từ API
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllSP");
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return BadRequest(responseContent);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
