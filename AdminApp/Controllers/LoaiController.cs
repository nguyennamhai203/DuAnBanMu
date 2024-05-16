using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AdminApp.Controllers
{
    public class LoaiController : Controller
    {
        public HttpClient httpClient { get; set; }
        public LoaiController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IActionResult> GetAllLoai()
        {
            var loais = JsonConvert.DeserializeObject<List<Loai>>(await (await httpClient.GetAsync("https://localhost:7050/api/Loai")).Content.ReadAsStringAsync());
            return View(loais);
        }
        public async Task<IActionResult> DetailLoai(Guid id)
        {

            var loais = (JsonConvert.DeserializeObject<List<Loai>>(await (await httpClient.GetAsync("https://localhost:7050/api/Loai")).Content.ReadAsStringAsync())).FirstOrDefault(x => x.Id == id);
            return View(loais);

        }
        public ActionResult CreateLoai()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> CreateLoai(Loai loai)
		{
            // Tiếp tục gửi yêu cầu HTTP đến endpoint API nếu không có lỗi
            var json = JsonConvert.SerializeObject(loai);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:7050/api/Loai/CreateAsync", data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllLoai");
            }
            else
            {
                return BadRequest(response);
            }

        }


        public async Task<IActionResult> DeleteLoai(Guid id)
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7050/api/Loai/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Xóa thành công
                return RedirectToAction("GetAllLoai");
            }
            else
            {
                // Xóa không thành công, in ra kết quả để debug
                var responseContent = await response.Content.ReadAsStringAsync();
                // Log hoặc in ra lỗi để kiểm tra
                Console.WriteLine($"Error deleting Loai: {responseContent}");

                // Thêm thông báo lỗi vào ViewData hoặc TempData để hiển thị trong View
                TempData["ErrorMessage"] = "Không thể xóa loại. Vui lòng thử lại.";

                return RedirectToAction("GetAllLoai");
            }
        }


        public async Task<IActionResult> EditLoai(Guid Id)
        {
            var response = await httpClient.GetAsync($"https://localhost:7050/api/Loai/{Id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var loai = JsonConvert.DeserializeObject<Loai>(jsonString);

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
        public async Task<IActionResult> EditLoai(Loai loai)
        {
            var apiUrl = $"https://localhost:7050/api/Loai/{loai.Id}";

            var jsonContent = JsonConvert.SerializeObject(loai);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllLoai");
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
