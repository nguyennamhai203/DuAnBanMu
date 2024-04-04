using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;
using Shop_Models.ViewModels.ChatLieu;

namespace AdminApp.Controllers
{
    public class ChatLieuController : Controller
    {
        private readonly HttpClient _httpClient;
        public ChatLieuController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            if (id != Guid.Empty)
            {
                var chatLieu = await _httpClient.GetFromJsonAsync<ChatLieu>($"https://localhost:7050/api/ChatLieu/GetById/{id}");
                List<ChatLieu> c = new List<ChatLieu>();
                c.Add(chatLieu);
                ViewData["ChatLieu"] = c;
            }
            var chatLieus = await _httpClient.GetFromJsonAsync<IEnumerable<ChatLieu>>("https://localhost:7050/api/ChatLieu/GetAll");
            return View(chatLieus);
        }
        public async Task<IActionResult> GetAll()
        {
            var chatLieus = await _httpClient.GetFromJsonAsync<IEnumerable<ChatLieu>>("https://localhost:7050/api/ChatLieu/GetAll");
            return Json(chatLieus);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            return RedirectToAction("Index", new { id = id });
        }
        public async Task<IActionResult> Create(CreateChatLieu chatLieu)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7050/api/ChatLieu/post-chatlieu", chatLieu);
            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }
        public async Task<IActionResult> Update(Guid id, ChatLieu chatLieu)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7050/api/ChatLieu/Put/{id}", chatLieu);
            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7050/api/ChatLieu/Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

    }
}
