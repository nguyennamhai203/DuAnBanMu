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
                var th = JsonConvert.DeserializeObject<List<ThuongHieu>>(await (await httpClient.GetAsync("https://localhost:7050/api/ThuongHieu")).Content.ReadAsStringAsync());
                return View(th);
            }
            public async Task<IActionResult> DetailTH(Guid id)
            {

                var th = (JsonConvert.DeserializeObject<List<ThuongHieu>>(await (await httpClient.GetAsync("https://localhost:7050/api/ThuongHieu")).Content.ReadAsStringAsync())).FirstOrDefault(x => x.Guid == id);
                return View(th);

            }
            public ActionResult CreateTH()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> CreateTH(ThuongHieu Th)
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

            public async Task<IActionResult> DeleteTH(Guid id)
            {
                await httpClient.DeleteAsync($"https://localhost:7050/api/ThuongHieu/{id}");
                return RedirectToAction("GetAllTH");
            }

            public async Task<IActionResult> EditTH(Guid Id)
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

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditTH(ThuongHieu th)
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

        }
    }


