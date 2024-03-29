using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static Shop_Models.Heplers.TrangThai;

namespace AdminApp.Controllers
{
    public class KhuyenMaiChiTietController : Controller
    {
        private readonly ILogger<KhuyenMaiChiTietController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IChiTietKhuyenMaiRepository _CTKMRepo;
        //private readonly ApplicationDbContext _context;

        public KhuyenMaiChiTietController(ILogger<KhuyenMaiChiTietController> logger, IHttpClientFactory httpClientFactory /*, IChiTietKhuyenMaiRepository CTKMRepo,*/ /*ApplicationDbContext context*/)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            //_CTKMRepo = CTKMRepo;
            //_context = context;
        }


        public async Task<IActionResult> IndexAsync(Guid idSale)
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
                var getById = KhuyenMaiList.FirstOrDefault(x => x.Id == idSale);
                ViewBag.KhuyenMai = KhuyenMaiList.Where(x => x.TrangThai == (int)TrangThaiSale.DangBatDau).ToList(); ;

                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }


        [HttpGet]
        public async Task<IActionResult> ProductsInSale(Guid idSale)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/KhuyenMai/GetAllProductInSale?idSale=" + idSale;
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
            var sanphamList = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(responsedata.Content.ToString());
            return PartialView("ProductsInSale", sanphamList);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSale(List<Guid> selectedProducts)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var token = HttpContext.Session.GetString("TokenCheck");
            var client = _httpClientFactory.CreateClient("BeHat");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //var content = new StringContent(JsonConvert.SerializeObject(selectedProducts), Encoding.UTF8, "application/json");
            var url = $"/api/KhuyenMai/RemoveSelectedProductPromotions";
            // Tạo content để gửi trong request


            var respone = client.PostAsJsonAsync(url, selectedProducts).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);


            if (respone.IsSuccessStatusCode)
            {
                var result = await respone.Content.ReadAsStringAsync();

                return Content(result, "application/json");
            }
            else
            {
                var result = await respone.Content.ReadAsStringAsync();

                return Content(result, "application/json");
            }


        }
        [HttpGet]
        public async Task<IActionResult> viewSaleAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/KhuyenMai/GetAll";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
            var KhuyenMaiList = JsonConvert.DeserializeObject<List<Khuyenmai>>(responsedata.Content.ToString());
            var getById = KhuyenMaiList.FirstOrDefault(x => x.Id == id);
            return PartialView("viewSale", getById);
        }

        [HttpGet]
        public async Task<IActionResult> ApllySale(Guid id, Guid? idThuongHieu, Guid? idLoaiMu, Guid? idMauSac)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                var url = $"/api/KhuyenMai/GetAll";
                var respone = client.GetAsync(url).Result;
                string data = await respone.Content.ReadAsStringAsync();
                var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
                var KhuyenMaiList = JsonConvert.DeserializeObject<List<Khuyenmai>>(responsedata.Content.ToString());
                var getById = KhuyenMaiList.FirstOrDefault(x => x.Id == id);

                ViewBag.KhuyenMai = KhuyenMaiList.Where(x => x.TrangThai == (int)TrangThaiSale.DangBatDau).ToList(); ;

                var urlSPCT = $"/api/ChiTietSanPham/GetAllDto";
                var responeSPCT = client.GetAsync(urlSPCT).Result;
                string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
                //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
                var KhuyenMaiListSPCT = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT.ToString());



                if (id == Guid.Empty)
                {
                    KhuyenMaiList = KhuyenMaiList?.Where(x => x.TrangThai == (int)TrangThaiSale.DangBatDau).ToList(); // Kiểm tra null trước khi sử dụng
                    ViewBag.KhuyenMai = KhuyenMaiList ?? new List<Khuyenmai>(); // Đảm bảo ViewBag không null
                }
                else
                {
                    if (getById.TrangThai == (int)TrangThaiSale.HetHan)
                    {
                        //ViewBag.ThongBao = "Đã hết chương trình khuyến mại không áp dụng được";
                        TempData["ThongBao"] = "Đã hết chương trình khuyến mại không áp dụng được";
                        return RedirectToAction("Index", "KhuyenMai");
                    }
                    if (getById.TrangThai == (int)TrangThaiSale.BuocDung)
                    {
                        //ViewBag.ThongBao = "Chương trình khuyến mại không hoạt động";
                        TempData["ThongBao"] = "Chương trình khuyến mại không hoạt động";
                        return RedirectToAction("Index", "KhuyenMai");
                    }

                }

                string getLoai = await client.GetStringAsync($"/api/Loai");
                ViewBag.GetLoai = JsonConvert.DeserializeObject<List<Loai>>(getLoai);
                var loaibyid = JsonConvert.DeserializeObject<List<Loai>>(getLoai);
                string getColor = await client.GetStringAsync($"/Get-All-MauSac");
                ViewBag.GetColor = JsonConvert.DeserializeObject<List<MauSac>>(getColor);
                string getThuongHieu = await client.GetStringAsync($"/api/ThuongHieu");
                ViewBag.GetThuongHieu = JsonConvert.DeserializeObject<List<ThuongHieu>>(getThuongHieu);

                var getallProductDT = KhuyenMaiListSPCT
                    .Where(x => (x.TrangThaiKhuyenMai == (int)TrangThaiSaleInProductDetail.DuocApDungSale || x.TrangThaiKhuyenMai == (int)TrangThaiSaleInProductDetail.DaApDungSale)
                                && x.TrangThai == (int)TrangThaiCoBan.HoatDong);



                //if (idThuongHieu != Guid.Empty && idThuongHieu != null)
                //    {
                //        //var tenThuongHieu = _context.thuongHieus.Find(idThuongHieu).TenThuongHieu;
                //        //lstSpDuocApDungKhuyenMai = (List<ItemShopViewModel>?)lstSpDuocApDungKhuyenMai.Where(x => x.ThuongHieu.ToUpper() == tenThuongHieu.ToUpper()).ToList();
                //        getallProductDT = getallProductDT.Where(x => x.ThuongHieuId == idThuongHieu);
                //    }
                //    if (idLoaiMu != Guid.Empty && idLoaiMu != null)
                //    {
                //        //var tenLoaiGiay = _context.LoaiGiays.Find(idLoaiGiay).TenLoaiGiay;
                //        //lstSpDuocApDungKhuyenMai = (List<ItemShopViewModel>?)lstSpDuocApDungKhuyenMai.Where(x => x.TheLoai.ToUpper() == tenLoaiGiay.ToUpper()).ToList();
                //        getallProductDT = getallProductDT.Where(x => x.LoaiId == idLoaiMu);
                //    }
                //    if (idMauSac != Guid.Empty && idMauSac != null)
                //    {
                //        //var tenMauSac = _context.mauSacs.Find(idMauSac).TenMauSac;
                //        //lstSpDuocApDungKhuyenMai = (List<ItemShopViewModel>?)lstSpDuocApDungKhuyenMai.Where(x => x.MauSac.ToUpper() == tenMauSac.ToUpper()).ToList();
                //        getallProductDT = getallProductDT.Where(x => x.MauSacId == idMauSac);
                //    }
                return View(getallProductDT.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> viewSanPhamFilter(Guid id, Guid? idThuongHieu, Guid? idLoaiMu, Guid? idMauSac, int? status)
        {
            var client = _httpClientFactory.CreateClient("BeHat");

            var url = $"/api/KhuyenMai/GetAll";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
            var KhuyenMaiList = JsonConvert.DeserializeObject<List<Khuyenmai>>(responsedata.Content.ToString());
            var km = KhuyenMaiList.FirstOrDefault(x => x.Id == id);


            var urlSPCT = $"/api/ChiTietSanPham/GetAllDto";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var KhuyenMaiListSPCT = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT);

            var getallProductDT = KhuyenMaiListSPCT
                .Where(x => (x.TrangThaiKhuyenMai == (int)TrangThaiSaleInProductDetail.DuocApDungSale || x.TrangThaiKhuyenMai == (int)TrangThaiSaleInProductDetail.DaApDungSale)
                            && x.TrangThai == (int)TrangThaiCoBan.HoatDong).ToList();

            if (idThuongHieu != Guid.Empty && idThuongHieu != null)
            {

                getallProductDT = getallProductDT.Where(x => x.ThuongHieuId == idThuongHieu).ToList();
            }
            if (idLoaiMu != Guid.Empty && idLoaiMu != null)
            {

                getallProductDT = getallProductDT.Where(x => x.LoaiId == idLoaiMu).ToList();
            }
            if (idMauSac != Guid.Empty && idMauSac != null)
            {

                getallProductDT = getallProductDT.Where(x => x.MauSacId == idMauSac).ToList();
            }
            if (status != null)
            {

                getallProductDT = getallProductDT.Where(x => x.TrangThaiKhuyenMai == status).ToList();
            }

            //if (km.LoaiHinhKhuyenMai == 0)
            //{
            //    var kmDongGia = lstSpDuocApDungKhuyenMai.Where(x => x.GiaGoc > Convert.ToDouble(km.MucGiam)).ToList();
            //    var a = kmDongGia;
            //    return PartialView("viewSanPhamFilter", (List<ItemShopViewModel>?)kmDongGia);
            //}
            return PartialView("viewSanPhamFilter", getallProductDT);
        }

        [HttpPost]
        public async Task<IActionResult> ApllySale(Guid idSale, List<string> selectedProducts)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var token = HttpContext.Session.GetString("TokenCheck");
            var client = _httpClientFactory.CreateClient("BeHat");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $"/api/KhuyenMai/GetAll";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
            var KhuyenMaiList = JsonConvert.DeserializeObject<List<Khuyenmai>>(responsedata.Content.ToString());


            ViewBag.KhuyenMai = KhuyenMaiList.Where(x => x.TrangThai == (int)TrangThaiSale.DangBatDau).ToList(); ;

            var urlSPCT = $"/api/ChiTietSanPham/GetAllDto";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var lstSpct = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT.ToString());


            var urlSimple = $"/api/ChiTietSanPham/Get";
            var responeSimple = client.GetAsync(urlSimple).Result;
            string dataSimple = await responeSimple.Content.ReadAsStringAsync();
            var lstSimple = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(dataSimple.ToString());

            List<string> DataMessage = new List<string>();
            List<string> SpDaApDungKm = new List<string>();
            var km = KhuyenMaiList.FirstOrDefault(x => x.Id == idSale);
            var successApllySale = "";



            try
            {

                int temp = 0;
                if (idSale != null && idSale != Guid.Empty && selectedProducts != null && selectedProducts.Count > 0)
                {
                    foreach (var idspct in selectedProducts)
                    {
                        var lstSp = lstSpct.Where(x => x.Id == Guid.Parse(idspct)).ToList();

                        foreach (var IdProduct in lstSp)
                        {
                            var SpSimPle = lstSp.FirstOrDefault(x => x.Id == IdProduct.Id);

                            var urlKMCT = $"/api/ChiTietKhuyenMai/GetAll";
                            var responeKMCT = client.GetAsync(urlKMCT).Result;
                            string dataKMCT = await responeKMCT.Content.ReadAsStringAsync();
                            var saledetail = JsonConvert.DeserializeObject<List<ChiTietKhuyenMai>>(dataKMCT.ToString());
                            var saledetailbyId = saledetail.Where(x => x.ChiTietSanPhamId == IdProduct.Id).ToList();



                            var name = lstSpct.FirstOrDefault(x => x.Id == IdProduct.Id).TenSanPham;

                            if (saledetailbyId != null && saledetailbyId.Count() > 0)
                            {
                                int i = 0;

                                foreach (var checkSale in saledetailbyId)
                                {
                                    if (checkSale.KhuyenMaiId == idSale)
                                    {
                                        i++;
                                        break;
                                    }
                                }
                                if (i != 0)
                                {

                                    SpDaApDungKm.Add($"Sản phẩm {name} đang áp dụng chương trình {km.TenKhuyenMai}");
                                }
                                else
                                {
                                    if (km.LoaiHinhKhuyenMai == "Khuyến mại giảm giá")
                                    {
                                        if (IdProduct.GiaBan > Convert.ToDouble(km.MucGiam))
                                        {
                                            var addSale = new ChiTietKhuyenMai()
                                            {
                                                Id = Guid.NewGuid(),
                                                KhuyenMaiId = idSale,
                                                ChiTietSanPhamId = IdProduct.Id,
                                                Mota = "Harinw",
                                                TrangThai = (int)TrangThaiSaleDetail.DangKhuyenMai
                                            };
                                            SpSimPle.TrangThaiKhuyenMai = (int)TrangThaiSaleInProductDetail.DaApDungSale;
                                            await client.PutAsJsonAsync($"/api/ChiTietSanPham/UpdateAsync2", SpSimPle);

                                            await client.PostAsJsonAsync($"/api/ChiTietKhuyenMai/CreateAsync", addSale);
                                            DataMessage.Add($"Áp dụng thành công chương trình giảm giá {km.TenKhuyenMai} với sản phẩm {name}");
                                        }
                                    }
                                    else
                                    {
                                        var addSale = new ChiTietKhuyenMai()
                                        {
                                            Id = Guid.NewGuid(),
                                            KhuyenMaiId = idSale,
                                            ChiTietSanPhamId = IdProduct.Id,
                                            Mota = "Kaisan",
                                            TrangThai = (int)TrangThaiSaleDetail.DangKhuyenMai
                                        };

                                        SpSimPle.TrangThaiKhuyenMai = (int)TrangThaiSaleInProductDetail.DaApDungSale;
                                        await client.PutAsJsonAsync($"/api/ChiTietSanPham/UpdateAsync2", SpSimPle);
                                        await client.PostAsJsonAsync($"/api/ChiTietKhuyenMai/CreateAsync", addSale);
                                        DataMessage.Add($"Áp dụng thành công chương trình giảm giá {km.TenKhuyenMai} với sản phẩm {name}");
                                    }

                                }
                            }
                            else
                            {
                                if (km.LoaiHinhKhuyenMai == "Khuyến mại giảm giá")
                                {
                                    if (IdProduct.GiaBan > Convert.ToDouble(km.MucGiam))
                                    {
                                        var addSale = new ChiTietKhuyenMai()
                                        {
                                            Id = Guid.NewGuid(),
                                            KhuyenMaiId = idSale,
                                            ChiTietSanPhamId = IdProduct.Id,
                                            Mota = "Kaisan",
                                            TrangThai = (int)TrangThaiSaleDetail.DangKhuyenMai
                                        };

                                        SpSimPle.TrangThaiKhuyenMai = (int)TrangThaiSaleInProductDetail.DaApDungSale;
                                        await client.PutAsJsonAsync($"/api/ChiTietSanPham/UpdateAsync2", SpSimPle);
                                        await client.PostAsJsonAsync($"/api/ChiTietKhuyenMai/CreateAsync", addSale);
                                        successApllySale = $"Ap dụng thành công chương trình {km.TenKhuyenMai} với sản phẩm đã chọn";
                                    }
                                }
                                else
                                {
                                    var addSale = new ChiTietKhuyenMai()
                                    {
                                        Id = Guid.NewGuid(),
                                        KhuyenMaiId = idSale,
                                        ChiTietSanPhamId = IdProduct.Id,
                                        Mota = "Kaisan",
                                        TrangThai = (int)TrangThaiSaleDetail.DangKhuyenMai
                                    };

                                    SpSimPle.TrangThaiKhuyenMai = (int)TrangThaiSaleInProductDetail.DaApDungSale;
                                    await client.PutAsJsonAsync($"/api/ChiTietSanPham/UpdateAsync2", SpSimPle);
                                    await client.PostAsJsonAsync($"/api/ChiTietKhuyenMai/CreateAsync", addSale);
                                    successApllySale = $"Ap dụng thành công chương trình {km.TenKhuyenMai} với sản phẩm đã chọn";
                                }
                            }
                            temp++;
                        }
                        ViewBag.Sales = DataMessage;

                    }
                    return Ok(new { err = DataMessage.Count, err1 = SpDaApDungKm.Count, add = successApllySale });

                }
                else
                {
                    successApllySale = "Vui lòng chọn sản phẩm để add sale";
                    return Ok(new { add = successApllySale });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }



    }
}
