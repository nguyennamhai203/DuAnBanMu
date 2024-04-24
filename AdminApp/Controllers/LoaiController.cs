using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;
using System.Data.Entity;
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
        public async Task<IActionResult> CreateLoai(Loai l)
        {
            // Serialize the Loai object to JSON
            var jsonContent = JsonConvert.SerializeObject(l);

            // Create a StringContent with the JSON data
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the POST request with the content
            var response = await httpClient.PostAsync("https://localhost:7050/api/Loai/CreateAsync", content);

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
            await httpClient.DeleteAsync($"https://localhost:7050/api/Loai/{id}");
            return RedirectToAction("GetAllLoai");
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
