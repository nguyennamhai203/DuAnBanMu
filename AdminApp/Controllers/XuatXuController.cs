using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;
using System.Text;


namespace WebApp.Controllers
{
    public class XuatXuController : Controller
    {
        // GET: HomeController1
        public HttpClient httpClient { get; set; }
        public XuatXuController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public async Task<IActionResult> GetAllXuatXu()
        {
            var url = $"https://localhost:7050/api/XuatXu/GetAll";
            var response = await httpClient.GetAsync(url);
            var apiData = await response.Content.ReadAsStringAsync();
            var XuatXu= JsonConvert.DeserializeObject<List<XuatXu>>(apiData);
            return View(XuatXu);
        }

        // GET: HomeController1/Details/5
        public  IActionResult CreateXuatXu()
        {
            return View();
        }
        
        public async Task<IActionResult> CreateXuatXu(XuatXu a)
        {
            var url = $"https://localhost:7050/api/XuatXu/post-xuat-xu-param?guid={a.Guid}&maxuatxu={a.MaXuatXu}&tenxuatxu={a.TenXuatXu}&trangthai={a.TrangThai}";
            var response = await httpClient.PostAsync(url,null);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetAllXuatXu");
            return View();



        }

        

       
        

        // GET: HomeController1/Edit/5
        public async  Task<IActionResult> EditXuatXu(int trangthai)
        {
            var url = $"https://localhost:7050/api/XuatXu/Get-xuat-xu?status={trangthai}&page=1";
            var response= await httpClient.PostAsync(url,null);
            var apiData = await response.Content.ReadAsStringAsync();
            var XuatXu = JsonConvert.DeserializeObject<XuatXu>(apiData);
            return View(XuatXu);
        }

        
        public async Task<IActionResult> EditXuatXu(XuatXu a)
        {
            var url = $"https://localhost:7050/api/XuatXu/put-xuat-xu?id={a.Guid}&maxuatxu={a.MaXuatXu}&tenxuatxu={a.TenXuatXu}&trangthai={a.TrangThai}";
            var response = await httpClient.PostAsync(url, null);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetAllXuatXu");
            
            return View();
        }



        // GET: HomeController1/Delete/5
        public async Task<IActionResult> DeleteXuatXu(Guid id)
        {
            var url = $"https://localhost:7050/api/XuatXu/delete-xuat-xu?id={id}";
            var response= await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetAllXuatXu");

            return View();
        }

        
    }
}
