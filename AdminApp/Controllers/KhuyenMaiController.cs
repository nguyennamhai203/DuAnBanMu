using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{
    public class KhuyenMaiController : Controller
    {
        private readonly ILogger<KhuyenMaiController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public KhuyenMaiController(ILogger<KhuyenMaiController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
           

            var accessToken = HttpContext.Session.GetString("AccessToken");
            var accessRole = HttpContext.Session.GetString("Result");
            if (!string.IsNullOrEmpty(accessToken) && accessRole == "Admin" || !string.IsNullOrEmpty(accessToken) && accessRole == "NhanVien")
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                var url = $"/api/KhuyenMai/GetAll";
                var respone = client.GetAsync(url).Result;
                string data = await respone.Content.ReadAsStringAsync();
                var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
                var KhuyenMaiList = JsonConvert.DeserializeObject<List<Khuyenmai>>(responsedata.Content.ToString());
                return View(KhuyenMaiList);
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }
       
        public async Task<IActionResult> LstSaleAsync(string trangThaiSale, string loaiHinhKM/*, string tenKM*/)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/KhuyenMai/GetAll";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
            var KhuyenMaiList = JsonConvert.DeserializeObject<List<Khuyenmai>>(responsedata.Content.ToString());
            if (!string.IsNullOrEmpty(trangThaiSale))
            {
                KhuyenMaiList = KhuyenMaiList.Where(x => x.TrangThai == Convert.ToInt32(trangThaiSale)).ToList();
            }
            if (!string.IsNullOrEmpty(loaiHinhKM))
            {
                KhuyenMaiList = KhuyenMaiList.Where(x => x.LoaiHinhKhuyenMai == loaiHinhKM.ToString()).ToList();
            }
            //if (!string.IsNullOrEmpty(tenKM))
            //{
            //    KhuyenMaiList = KhuyenMaiList.Where(x => x.TenKhuyenMai.ToUpper().Contains(tenKM.ToUpper())).ToList();
            //}
            return PartialView("_LstSale", KhuyenMaiList);
        }

        public IActionResult Create()
        {
            ViewBag.ListLoaiHinh = new List<SelectListItem>
            {
               
                new SelectListItem { Text = "Khuyến mại giảm giá", Value = "Khuyến mại giảm giá" },
                new SelectListItem { Text = "Khuyến mại đồng giá", Value = "Khuyến mại đồng giá" }
            };
            ViewBag.TrangThai = new List<SelectListItem>
            {
                new SelectListItem { Text = "Hết hạn", Value = "0" },
                new SelectListItem { Text = "Đang hoạt động", Value = "1" },
                 new SelectListItem { Text = "Chưa bắt đầu", Value = "2" },
                  new SelectListItem { Text = "Buộc dừng", Value = "3" }
            };
            ViewBag.ListGiamGia = new List<SelectListItem>
            {
                new SelectListItem { Text = "5%", Value = "5" },
                new SelectListItem { Text = "10%", Value = "10" },
                new SelectListItem { Text = "15%", Value = "15" },
                new SelectListItem { Text = "20%", Value = "20" },
                new SelectListItem { Text = "25%", Value = "25" },
                new SelectListItem { Text = "30%", Value = "30" },
                new SelectListItem { Text = "35%", Value = "35" },
                new SelectListItem { Text = "40%", Value = "40" },
                new SelectListItem { Text = "45%", Value = "45" },
                new SelectListItem { Text = "50%", Value = "50" },
                new SelectListItem { Text = "55%", Value = "55" },
                new SelectListItem { Text = "60%", Value = "60" },
                new SelectListItem { Text = "65%", Value = "65" },
                new SelectListItem { Text = "70%", Value = "70" },
                new SelectListItem { Text = "75%", Value = "75" },
                new SelectListItem { Text = "80%", Value = "80" },
                new SelectListItem { Text = "85%", Value = "85" },
                new SelectListItem { Text = "90%", Value = "90" },
                new SelectListItem { Text = "95%", Value = "95" },
                new SelectListItem { Text = "100%", Value = "100" }
            };
            return View();
        }


        public async Task<IActionResult> Edit(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/KhuyenMai/GetAll";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
            var KhuyenMaiList = JsonConvert.DeserializeObject<List<Khuyenmai>>(responsedata.Content.ToString());
            var getById = KhuyenMaiList.FirstOrDefault(x => x.Id == Id);


            ViewBag.ListLoaiHinh = new List<SelectListItem>
            {
                new SelectListItem { Text = "Khuyến mại giảm giá", Value = "Khuyến mại giảm giá" },
                new SelectListItem { Text = "Khuyến mại đồng giá", Value = "Khuyến mại đồng giá" }
            };
            ViewBag.TrangThai = new List<SelectListItem>
            {
                new SelectListItem { Text = "Hết hạn", Value = "0" },
                new SelectListItem { Text = "Đang hoạt động", Value = "1" },
                 new SelectListItem { Text = "Chưa bắt đầu", Value = "2" },
                  new SelectListItem { Text = "Buộc dừng", Value = "3" }
            };
            ViewBag.ListGiamGia = new List<SelectListItem>
            {
                new SelectListItem { Text = "5%", Value = "5" },
                new SelectListItem { Text = "10%", Value = "10" },
                new SelectListItem { Text = "15%", Value = "15" },
                new SelectListItem { Text = "20%", Value = "20" },
                new SelectListItem { Text = "25%", Value = "25" },
                new SelectListItem { Text = "30%", Value = "30" },
                new SelectListItem { Text = "35%", Value = "35" },
                new SelectListItem { Text = "40%", Value = "40" },
                new SelectListItem { Text = "45%", Value = "45" },
                new SelectListItem { Text = "50%", Value = "50" },
                new SelectListItem { Text = "55%", Value = "55" },
                new SelectListItem { Text = "60%", Value = "60" },
                new SelectListItem { Text = "65%", Value = "65" },
                new SelectListItem { Text = "70%", Value = "70" },
                new SelectListItem { Text = "75%", Value = "75" },
                new SelectListItem { Text = "80%", Value = "80" },
                new SelectListItem { Text = "85%", Value = "85" },
                new SelectListItem { Text = "90%", Value = "90" },
                new SelectListItem { Text = "95%", Value = "95" },
                new SelectListItem { Text = "100%", Value = "100" }
            };
            return View(getById);
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Khuyenmai khuyenMai/*,IFormFile formFile*/)
        {

            ViewBag.ListLoaiHinh = new List<SelectListItem>
            {
                new SelectListItem { Text = "Khuyến mại giảm giá", Value = "Khuyến mại giảm giá" },
                new SelectListItem { Text = "Khuyến mại đồng giá", Value = "Khuyến mại đồng giá" }
            };
            ViewBag.ListGiamGia = new List<SelectListItem>
            {
                new SelectListItem { Text = "5%", Value = "5" },
                new SelectListItem { Text = "10%", Value = "10" },
                new SelectListItem { Text = "15%", Value = "15" },
                new SelectListItem { Text = "20%", Value = "20" },
                new SelectListItem { Text = "25%", Value = "25" },
                new SelectListItem { Text = "30%", Value = "30" },
                new SelectListItem { Text = "35%", Value = "35" },
                new SelectListItem { Text = "40%", Value = "40" },
                new SelectListItem { Text = "45%", Value = "45" },
                new SelectListItem { Text = "50%", Value = "50" },
                new SelectListItem { Text = "55%", Value = "55" },
                new SelectListItem { Text = "60%", Value = "60" },
                new SelectListItem { Text = "65%", Value = "65" },
                new SelectListItem { Text = "70%", Value = "70" },
                new SelectListItem { Text = "75%", Value = "75" },
                new SelectListItem { Text = "80%", Value = "80" },
                new SelectListItem { Text = "85%", Value = "85" },
                new SelectListItem { Text = "90%", Value = "90" },
                new SelectListItem { Text = "95%", Value = "95" },
                new SelectListItem { Text = "100%", Value = "100" }
            };


            try
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                var url = $"/api/KhuyenMai/UpdateAsync";
                var respone = client.PutAsJsonAsync(url, khuyenMai).Result;
                if (respone.IsSuccessStatusCode)
                {
                    var jsonResult = await respone.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(jsonResult);
                    return Content(jsonResult, "application/json");
                }
                else
                {
                    var jsonResult = await respone.Content.ReadAsStringAsync();
                    return Content(jsonResult, "application/json");
                }
              
            }
            catch (Exception ex)
            {

                
                return View();
            }
        }



        [HttpPost]
        public async Task<IActionResult> Createaa(Khuyenmai khuyenmai)
        {
            //ViewBag.ListLoaiHinh = new List<SelectListItem>
            //{
            //    new SelectListItem { Text = "Khuyến mại giảm giá", Value = "1" },
            //    new SelectListItem { Text = "Khuyến mãi đồng giá", Value = "0" }
            //};
            //ViewBag.ListGiamGia = new List<SelectListItem>
            //{
            //    new SelectListItem { Text = "5%", Value = "5" },
            //    new SelectListItem { Text = "10%", Value = "10" },
            //    new SelectListItem { Text = "15%", Value = "15" },
            //    new SelectListItem { Text = "20%", Value = "20" },
            //    new SelectListItem { Text = "25%", Value = "25" },
            //    new SelectListItem { Text = "30%", Value = "30" },
            //    new SelectListItem { Text = "35%", Value = "35" },
            //    new SelectListItem { Text = "40%", Value = "40" },
            //    new SelectListItem { Text = "45%", Value = "45" },
            //    new SelectListItem { Text = "50%", Value = "50" },
            //    new SelectListItem { Text = "55%", Value = "55" },
            //    new SelectListItem { Text = "60%", Value = "60" },
            //    new SelectListItem { Text = "65%", Value = "65" },
            //    new SelectListItem { Text = "70%", Value = "70" },
            //    new SelectListItem { Text = "75%", Value = "75" },
            //    new SelectListItem { Text = "80%", Value = "80" },
            //    new SelectListItem { Text = "85%", Value = "85" },
            //    new SelectListItem { Text = "90%", Value = "90" },
            //    new SelectListItem { Text = "95%", Value = "95" },
            //    new SelectListItem { Text = "100%", Value = "100" }
            //};         
            khuyenmai.Id = Guid.NewGuid();
            khuyenmai.TrangThai = 1;


            var client = _httpClientFactory.CreateClient("BeHat");
            //var accessToken = HttpContext.Session.GetString("TokenCheck");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.PostAsJsonAsync($"/api/Khuyenmai/CreateAsync", khuyenmai);

            


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
    }
}
