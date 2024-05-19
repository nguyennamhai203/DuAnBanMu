using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;
using System.Text;

namespace AdminApp.Controllers
{
    public class ThuonghieuController : Controller
    {

        public HttpClient httpClient { get; set; }
        public ThuonghieuController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IActionResult> GetAllTH()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var th = JsonConvert.DeserializeObject<List<ThuongHieu>>(await (await httpClient.GetAsync("https://localhost:7050/api/ThuongHieu")).Content.ReadAsStringAsync());
                return View(th);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<IActionResult> DetailTH(Guid id)
        {

           
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var th = (JsonConvert.DeserializeObject<List<ThuongHieu>>(await (await httpClient.GetAsync("https://localhost:7050/api/ThuongHieu")).Content.ReadAsStringAsync())).FirstOrDefault(x => x.Guid == id);
                return View(th);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateTH()
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
        [HttpPost]
        public async Task<IActionResult> CreateTH(ThuongHieu Th)
        {
           

            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                // Serialize the Loai object to JSON
                var jsonContent = JsonConvert.SerializeObject(Th);

                // Create a StringContent with the JSON data
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send the POST request with the content
                var response = await httpClient.PostAsync("https://localhost:7050/api/ThuongHieu", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllTH");
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }

        public async Task<IActionResult> DeleteTH(Guid id)
        {
         
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var response = await httpClient.DeleteAsync($"https://localhost:7050/api/ThuongHieu/{id}");
                if (response.IsSuccessStatusCode)
                {
                    // Xóa thành công
                    return RedirectToAction("GetAllTH");
                }
                else
                {
                    // Xóa không thành công, in ra kết quả để debug
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Log hoặc in ra lỗi để kiểm tra
                    Console.WriteLine($"Error deleting Loai: {responseContent}");

                    // Thêm thông báo lỗi vào ViewData hoặc TempData để hiển thị trong View
                    TempData["ErrorMessage"] = "Không thể xóa loại. Vui lòng thử lại.";

                    return RedirectToAction("GetAllTH");
                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<IActionResult> EditTH(Guid Id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var response = await httpClient.GetAsync($"https://localhost:7050/api/ThuongHieu/{Id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var loai = JsonConvert.DeserializeObject<ThuongHieu>(jsonString);

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
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTH(ThuongHieu th)
        {          
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var apiUrl = $"https://localhost:7050/api/ThuongHieu/{th.Guid}";

                var jsonContent = JsonConvert.SerializeObject(th);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllTH");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }

    }
}


