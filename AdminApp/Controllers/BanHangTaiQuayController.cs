using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http;

namespace AdminApp.Controllers
{
    public class BanHangTaiQuayController : Controller
    {
        private readonly ILogger<BanHangTaiQuayController> logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public BanHangTaiQuayController(ILogger<BanHangTaiQuayController> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> DanhSachHoaDonCho()
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            //var responsedata = JsonConvert.DeserializeObject<ResponseDto>(data);
            var listHoaDonCho = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(data);
            //var listHoaDonCho = await _hoaDonServices.GetAllHoaDonCho();

            var urlSPCT = $"/api/ChiTietSanPham/GetAllDto?status=1";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var listsanpham = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT);
            //var listHoaDonCho = await _hoaDonServices.GetAllHoaDonCho();
            //var listsanpham = await _sanPhamChiTietService.GetDanhSachBienTheItemShopViewModelAsync();
            foreach (var item in listHoaDonCho)
            {
                foreach (var item2 in item.hoaDonChiTietDTOs)
                {
                    var sanpham = listsanpham.FirstOrDefault(c => c.Id == item2.SanPhamChiTietId);
                    item2.NameProductDetail = sanpham.TenSanPham + "/" + sanpham.TenMauSac + "/" + sanpham.TenThuongHieu;
                    //item2.masanpham  = sanpham
                }
            }
            var v = listHoaDonCho.OrderBy(c => Convert.ToInt32(c.MaHoaDon.Substring(4)));

            return View(listHoaDonCho.OrderBy(c => Convert.ToInt32(c.MaHoaDon.Substring(4))));
        }

        [HttpPost]
        public async Task<IActionResult> TaoHoaDonTaiQuay()
        {
            try
            {
                var idUser = HttpContext.Session.GetString("IdNguoiDung");

                var hoaDonMoi = new RequestHDTaiQuay()
                {
                    IdNguoiDung = Guid.Parse(idUser)
                };

                var client = _httpClientFactory.CreateClient("BeHat");
                var url = $"/api/BanHangTaiQuay/CreateHdTaiQuay";
                var request = await client.PostAsJsonAsync(url, hoaDonMoi);
                request.EnsureSuccessStatusCode(); // Tự động ném một ngoại lệ nếu có lỗi HTTP

                var responseContent = await request.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ResponseDto>(responseContent);
                var hoadonNew = JsonConvert.DeserializeObject<HoaDonChoDTO>(responseData.Content.ToString());

                return Json(hoadonNew.MaHoaDon);
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi HTTP
                Console.WriteLine($"HTTP Error: {ex.Message}");
                return StatusCode(500, "Lỗi không xác định xảy ra");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khác
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Lỗi không xác định xảy ra");
            }
        }

        [HttpGet]
        public async Task<IActionResult> HoaDonDuocChon(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
            {
                return Ok(new
                {
                    TongTienGoc = 0,
                    TienPhaiTra = 0,
                    SoTienKhuyenMaiGiam = 0,
                    SoTienVoucherGiam = 0,
                    MaHoaDon = "",
                    NgayTao = "",
                    TenNhanVien = ""
                });
            }
            //var hoaDon = (await _hoaDonServices.GetAllHoaDonCho()).FirstOrDefault(hd => hd.MaHoaDon == maHD);
            var client = _httpClientFactory.CreateClient("BeHat");
            var urlSPCT = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            //var response = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var hoaDon = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(dataSPCT.ToString());
            var hoaDonDuocChon = hoaDon.FirstOrDefault(x => x.MaHoaDon == maHD);
            //var listHoaDonCho = await _hoaDonServices.GetAllHoaDonCho();
            //var listsanpham = await _sanPhamChiTietService.GetDanhSachBienTheItemShopViewModelAsync();
            //_httpClient.GetFromJsonAsync<List<HoaDonChoDTO>>("https://localhost:7038/api/HoaDon/GetAllHoaDonCho");

            ////var user = await _userManager.FindByIdAsync(hoaDon.IdNguoiDung);
            var user = HttpContext.Session.GetString("TenNguoiDung");

            if (hoaDonDuocChon == null)
            {
                return Ok(new
                {
                    TongTienGoc = 0,
                    TienPhaiTra = 0,
                    SoTienKhuyenMaiGiam = 0,
                    SoTienVoucherGiam = 0,
                    MaHoaDon = "Null",
                    NgayTao = "",
                    TenNhanVien = ""

                });
            }
            double tongTienGoc = 0;
            double tienPhaiTra = 0;
            string ngayTao = ((DateTime)hoaDonDuocChon.NgayTao).ToString("dd/MM/yyyy HH:mm");
            foreach (var item in hoaDonDuocChon.hoaDonChiTietDTOs)
            {
                tongTienGoc += (double)item.Price * (int)item.Quantity;
                tienPhaiTra += (double)item.PriceBan * (int)item.Quantity;
            }
            return Ok(new
            {
                TongTienGoc = tongTienGoc,
                TienPhaiTra = tienPhaiTra,
                SoTienKhuyenMaiGiam = tongTienGoc - tienPhaiTra,
                SoTienVoucherGiam = 0,
                MaHoaDon = hoaDonDuocChon.MaHoaDon,
                NgayTao = ngayTao,
                TenNhanVien = user
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChiTietHoaDonDuocChon(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
            {
                return PartialView("_HoaDonChiTietPartialView", new List<HoaDonChiTietDto>());
            }
            //var hoaDonDuocChon = (await _hoaDonServices.GetAllHoaDonCho()).FirstOrDefault(c => c.MaHoaDon == maHD).hoaDonChiTietDTOs;
            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            //var response = JsonConvert.DeserializeObject<ResponseDto>(data);
            //var hoaDonDuocChon = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(response.Content.ToString()).FirstOrDefault(x => x.MaHoaDon == maHD).hoaDonChiTietDTOs;

            var hoaDon = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(data.ToString());


            //var listsanpham = await _sanPhamChiTietService.GetDanhSachBienTheItemShopViewModelAsync();
            var urlSPCT = $"/api/ChiTietSanPham/GetAllDto?status=1";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var listsanpham = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT);
            if (hoaDon.FirstOrDefault(x => x.MaHoaDon == maHD).hoaDonChiTietDTOs != null)
            {
                var hoaDonDuocChon = hoaDon.FirstOrDefault(x => x.MaHoaDon == maHD).hoaDonChiTietDTOs;
                foreach (var item2 in hoaDonDuocChon)
                {
                    var sanpham = listsanpham.FirstOrDefault(c => c.Id == item2.SanPhamChiTietId);
                    item2.NameProductDetail = sanpham.TenSanPham + "/" + sanpham.TenMauSac + "/" + sanpham.TenThuongHieu;
                    item2.CodeProductDetail = sanpham.MaSanPhamChiTiet;
                }
                return PartialView("_HoaDonChiTietPartialView", hoaDonDuocChon);
            }
            return PartialView("_HoaDonChiTietPartialView", null);

        }

        [HttpGet]
        public async Task<IActionResult> LoadPartialViewDanhSachSanPham(string tukhoa)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var urlSPCT = $"/api/ChiTietSanPham/GetAllDto?status=1";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var listsanpham = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT);

            if (!string.IsNullOrWhiteSpace(tukhoa))
            {
                var listsanphamLoc = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT).Where(c => c.TenSanPham.ToLower().Replace(" ", "").Contains(tukhoa.ToLower().Replace(" ", "")) || c.MaSanPham.ToLower().Replace(" ", "").Contains(tukhoa.ToLower().Replace(" ", "")));

                return PartialView("_DanhSachSanPhamPartialView", listsanphamLoc);

            }


            else
                return PartialView("_DanhSachSanPhamPartialView", listsanpham);
        }


        [HttpPost]
        public async Task<IActionResult> ThemSanPhamVaoHoaDon(string maHD, string idSanPham)
        {
            if (string.IsNullOrWhiteSpace(maHD))
            {
                return Ok(new { TrangThai = false, TrangThaiHang = false });
            }

            var client = _httpClientFactory.CreateClient("BeHat");


            //var response = JsonConvert.DeserializeObject<ResponseDto>(data);
            //var hoaDon = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(response.Content.ToString()).FirstOrDefault(x => x.MaHoaDon == maHD).hoaDonChiTietDTOs;

            //var hoaDon = (await _hoaDonServices.GetAllHoaDonCho()).FirstOrDefault(hd => hd.MaHoaDon == maHD);
            //var sanPham = (await _sanPhamChiTietService.GetDanhSachBienTheItemShopViewModelAsync()).FirstOrDefault(c => c.IdChiTietSp == idSanPham);

            var urlSPCT = $"/api/ChiTietSanPham/GetAllDto?status=1";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var sanPham = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT).FirstOrDefault(sp => sp.Id == Guid.Parse(idSanPham));

            if (sanPham.SoLuongTon == 0)
            {
                return Ok(new { TrangThai = false, TrangThaiHang = true });
            }
            //var hoaDonChitiet = new HoaDonChiTiet()
            //{
            //    IdHoaDon = hoaDon.Id,
            //    IdHoaDonChiTiet = Guid.NewGuid().ToString(),
            //    IdSanPhamChiTiet = idSanPham.ToString(),
            //    SoLuong = 1,
            //    TrangThai = (int)TrangThaiHoaDonChiTiet.ChoTaiQuay,
            //    GiaBan = sanPham.GiaThucTe,
            //    GiaGoc = sanPham.GiaGoc,
            //};
            //var hoaDonChiTietTraLai = await _hoaDonChiTietServices.ThemSanPhamVaoHoaDon(hoaDonChitiet);

            var urlAddBillDetail = $"api/BanHangTaiQuay/AddBillDetail?mahoadon={maHD}&codeProductDetail={sanPham.MaSanPhamChiTiet}";
            var responseAddBillDetail = await client.PostAsync(urlAddBillDetail, null);
            var jsonResponse = await responseAddBillDetail.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(jsonResponse);
            var message = responseDto.Message;
            var responseHDCT = JsonConvert.DeserializeObject<HoaDonChiTietDto>(responseDto.Content.ToString());


            //await _sanPhamChiTietService.UpDatSoLuongAynsc(new SanPhamSoLuongDTO()
            //{
            //    IdChiTietSanPham = sanPham.IdChiTietSp,
            //    SoLuong = 1
            //});
            var url = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var hoaDon = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(data.ToString());
            var hoaDonDuocChon = hoaDon.FirstOrDefault(x => x.MaHoaDon == maHD).hoaDonChiTietDTOs.FirstOrDefault(x => x.Id == responseHDCT.Id);

            var tongTienThayDoi = hoaDonDuocChon.Price;//gias thuc te trong spct
            var soTienTraLaiThayDoi = hoaDonDuocChon.PriceBan;// gia ban trog hoa don
            var SoTienKhyenMaiGiam = tongTienThayDoi - soTienTraLaiThayDoi;
            return Ok(new
            {
                TrangThai = true,
                IdHoaDon = hoaDonDuocChon.HoaDonId,
                IdHoaDonChiTiet = hoaDonDuocChon.Id,
                IdSanPhamChiTiet = hoaDonDuocChon.SanPhamChiTietId,
                SoLuong = hoaDonDuocChon.Quantity,
                GiaBan = hoaDonDuocChon.PriceBan,
                GiaGoc = hoaDonDuocChon.Price,
                TenSanPham = sanPham.TenSanPham + "/" + sanPham.TenMauSac + "/" + sanPham.TenThuongHieu,
                MaSanPham = sanPham.MaSanPham,
                TongTienThayDoi = tongTienThayDoi,
                SoTienTraLaiThayDoi = soTienTraLaiThayDoi,
                SoTienKhyenMaiGiam = SoTienKhyenMaiGiam,
                SoTienVoucherGiam = 0,
            });
        }

        [HttpGet]
        public async Task<IActionResult> CapNhapThongTinTien(string maHD)
        {

            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var hoaDon = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(data.ToString());
            var hoaDonDuocChon = hoaDon.FirstOrDefault(x => x.MaHoaDon == maHD).hoaDonChiTietDTOs;


            double tongTienThayDoi = 0;
            double soTienTraLaiThayDoi = 0;
            if (hoaDon != null)
            {
                foreach (var item in hoaDonDuocChon)
                {
                    tongTienThayDoi += (double)item.PriceBan * (double)item.Quantity;
                    soTienTraLaiThayDoi += (double)item.PriceBan * (double)item.Quantity;
                }
                var SoTienKhyenMaiGiam = tongTienThayDoi - soTienTraLaiThayDoi;
                return Ok(new
                {
                    TrangThai = true,
                    TongTienThayDoi = tongTienThayDoi,
                    SoTienTraLaiThayDoi = soTienTraLaiThayDoi,
                    SoTienKhyenMaiGiam = SoTienKhyenMaiGiam,
                    SoTienVoucherGiam = 0,
                });
            }
            return Ok(new
            {
                TrangThai = false,
                TongTienThayDoi = 0,
                SoTienTraLaiThayDoi = 0,
                SoTienKhyenMaiGiam = 0,
                SoTienVoucherGiam = 0,
            });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateSoLuong(string maHD, string idSanPham, int SoLuongMoi)
        {
            if (string.IsNullOrWhiteSpace(maHD))
            {
                return Ok(new { TrangThai = false });
            }
            //var hoaDon = (await _hoaDonServices.GetAllHoaDonCho()).FirstOrDefault(hd => hd.MaHoaDon == maHD);
            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var hoaDonChoDtoList = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(data.ToString());
            var hoaDon = hoaDonChoDtoList.FirstOrDefault(x => x.MaHoaDon == maHD);

            //var sanPham = (await _sanPhamChiTietService.GetDanhSachBienTheItemShopViewModelAsync()).FirstOrDefault(c => c.IdChiTietSp == idSanPham);
            var urlSPCT = $"/api/ChiTietSanPham/GetAllDto?status=1";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var sanPham = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT).FirstOrDefault(sp => sp.Id == Guid.Parse(idSanPham));

            var soLuongThayDoi = SoLuongMoi - hoaDon.hoaDonChiTietDTOs.FirstOrDefault(x => x.HoaDonId == hoaDon.Id && x.SanPhamChiTietId == sanPham.Id).Quantity;
            // hàm cập nhật số lượng tồn của san phẩm 

            //var hoaDonCt = hoaDonDuocChonList.FirstOrDefault(x=>x.HoaDonId==hoa);
            var hoaDonDuocChon = hoaDon.hoaDonChiTietDTOs.FirstOrDefault(x => x.HoaDonId == hoaDon.Id && x.SanPhamChiTietId == sanPham.Id);

            //var soLuongThayDoi = 
            //var soLuongThayDoi = await _hoaDonChiTietServices.UpdateSoLuong(hoaDon.Id, idSanPham, SoLuongMoi, sanPham.SoLuongTon.ToString());

            var urlUpdateBillDetail = $"/api/BanHangTaiQuay/UpdateBillDetail?mahoadon={maHD}&codeProductDetail={sanPham.MaSanPhamChiTiet}&soluong={SoLuongMoi}";
            var responeUpdateBillDetail = client.PutAsync(urlUpdateBillDetail, null).Result;
            string jsonUpdateBillDetail = await responeUpdateBillDetail.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(jsonUpdateBillDetail);
            var content = responseDto.Content;
            if (content != null)
            {
                var responseHDCT = JsonConvert.DeserializeObject<HoaDonChiTietDto>(responseDto.Content.ToString());

            }

            var urlSPCT2 = $"/api/ChiTietSanPham/GetAllDto?status=1";
            var responeSPCT2 = client.GetAsync(urlSPCT2).Result;
            string dataSPCT2 = await responeSPCT2.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var sanPham2 = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT2).FirstOrDefault(sp => sp.Id == Guid.Parse(idSanPham));

            //var sanPham2 = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT).FirstOrDefault(sp => sp.Id == Guid.Parse(idSanPham));

            if (content != null)
            {


                var tongTienThayDoi = Convert.ToInt32(soLuongThayDoi) * (double)hoaDonDuocChon.Price;
                var soTienTraLaiThayDoi = Convert.ToInt32(soLuongThayDoi) * (double)hoaDonDuocChon.PriceBan;
                var SoTienKhyenMaiGiam = tongTienThayDoi - soTienTraLaiThayDoi;
                //await _sanPhamChiTietService.UpDatSoLuongAynsc(new SanPhamSoLuongDTO()
                //{
                //    IdChiTietSanPham = idSanPham,
                //    SoLuong = Convert.ToInt32(soLuongThayDoi)
                //});

                var a = Convert.ToInt32(sanPham2.SoLuongTon) - Convert.ToInt32(soLuongThayDoi);
                if (Convert.ToInt32(sanPham2.SoLuongTon) - Convert.ToInt32(soLuongThayDoi) != 0)
                {
                    return Ok(new
                    {
                        TrangThai = true,
                        SoLuongConLai = /*Convert.ToInt32(sanPham.SoLuongTon) - Convert.ToInt32(soLuongThayDoi)*/sanPham2.SoLuongTon,
                        TongTienThayDoi = tongTienThayDoi,
                        SoTienTraLaiThayDoi = soTienTraLaiThayDoi,
                        SoTienKhyenMaiGiam = SoTienKhyenMaiGiam,
                        SoTienVoucherGiam = 0,

                    });
                }
                else
                {
                    return Ok(new
                    {
                        TrangThai = true,
                        TongTienThayDoi = tongTienThayDoi,
                        SoTienTraLaiThayDoi = soTienTraLaiThayDoi,
                        SoTienKhyenMaiGiam = SoTienKhyenMaiGiam,
                        SoLuongConLai = "Hết hàng",
                        SoTienVoucherGiam = 0,

                    });
                }
            }
            else
            {
                var soLuongTrongHoaDon = hoaDon.hoaDonChiTietDTOs.FirstOrDefault(c => c.SanPhamChiTietId == Guid.Parse(idSanPham)).Quantity;
                if (sanPham2.SoLuongTon > 0)
                    return Ok(new
                    {
                        TrangThai = false,
                        SoLuongConLai = sanPham2.SoLuongTon,
                        SoLuongCu = soLuongTrongHoaDon,
                    });
                else
                    return Ok(new
                    {
                        TrangThai = false,
                        SoLuongConLai = "Hết hàng",
                        SoLuongCu = soLuongTrongHoaDon,
                    });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ThanhToan(string maHD, string SDT, string tenKhachHang, double tongTien, double tienMat, double chuyenKhoan, string idVoucher, int hinhThuc, double tongGiam)
        {
            if (string.IsNullOrEmpty(maHD))
            {
                return Ok(new
                {
                    TrangThai = false,
                    Chuoi = "Vui lòng chọn hóa đơn",
                });
            }


            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var hoaDonChoDtoList = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(data.ToString());
            var hoaDon = hoaDonChoDtoList.FirstOrDefault(x => x.MaHoaDon == maHD);

            var hoaDonChiTiet = hoaDonChoDtoList.FirstOrDefault(x => x.MaHoaDon == maHD).hoaDonChiTietDTOs;
            
            
                
                if (hoaDonChiTiet.Count()==0)
                {
                    return Ok(new
                    {
                        TrangThai = false,
                    });
                }

            
            var hoaDonUpdate = new HoaDon()
            {
                Id = hoaDon.Id,
                MaHoaDon = hoaDon.MaHoaDon,
                NguoiDungId = hoaDon.IdNguoiDung,
                NgayTao = (DateTime)hoaDon.NgayTao,
                TrangThaiGiaoHang = 5, // tại quầy
                TrangThaiThanhToan = 1, //DaThanhToan
                TongTien = tongTien,
                TienGiam = tongGiam,
                NgayThanhToan = DateTime.Now,
                TenKhachHang = tenKhachHang,
                SoDienThoai = SDT,
            };
    
            var urlThanhToan = $"/api/BanHangTaiQuay/ThanhToanTaiQuay";
            var responeThanhToan = await client.PutAsJsonAsync(urlThanhToan, hoaDonUpdate);
            if (responeThanhToan.IsSuccessStatusCode)
            {
                //return await res.Content.ReadAsAsync<bool>();

                if (hinhThuc == 1)
                {
                    var pttt = await GetPTTT("TienMat");
                    if (await TaoPTTTChiTiet(hoaDonUpdate.Id, pttt, tienMat, 1))
                    {
                        return Ok(new
                        {
                            TrangThai = true,
                        });
                    }

                }
                if (hinhThuc == 2)
                {
                    var pttt = await GetPTTT("ChuyenKhoan");

                    if (await TaoPTTTChiTiet(hoaDonUpdate.Id, pttt, chuyenKhoan, 1))
                    {
                        return Ok(new
                        {
                            TrangThai = true,
                        });
                    }
                }
                if (hinhThuc == 3)
                {
                    var pttt = await GetPTTT("ChuyenKhoan");

                    if (await TaoPTTTChiTiet(hoaDonUpdate.Id, pttt, chuyenKhoan, 1))
                    {
                        pttt = await GetPTTT("TienMat");

                        if (await TaoPTTTChiTiet(hoaDonUpdate.Id, pttt, tienMat, 1))
                        {
                            return Ok(new
                            {
                                TrangThai = true,
                            });
                        }
                        //else
                        //{
                        //    _hoaDonServices.XoaPhuongThucThanhToanChiTietBangIdHoaDon(hoaDonUpdate.IdHoaDon);
                        //    return Ok(new
                        //    {
                        //        TrangThai = false,
                        //        Chuoi = "Thanh toán không thành công"
                        //    });
                        //}
                    }
                    return Ok(new
                    {
                        TrangThai = false,
                        Chuoi = "Thanh toán không thành công"
                    });
                }
            }

            return Ok(new
            {
                TrangThai = false,
                Chuoi = "Thanh toán không thành công"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> XoaSanPhamKhoiHoaDon(string maHD, string idSanPham)
        {
            if (string.IsNullOrWhiteSpace(maHD))
            {
                return Ok(new { TrangThai = false });
            }
            //var hoaDon = (await _hoaDonServices.GetAllHoaDonCho()).FirstOrDefault(hd => hd.MaHoaDon == maHD);

            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var hoaDonChoDtoList = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(data.ToString());
            var hoaDon = hoaDonChoDtoList.FirstOrDefault(x => x.MaHoaDon == maHD);

            var urlSPCT = $"/api/ChiTietSanPham/GetAllDto?status=1";
            var responeSPCT = client.GetAsync(urlSPCT).Result;
            string dataSPCT = await responeSPCT.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var sanPham = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT).FirstOrDefault(sp => sp.Id == Guid.Parse(idSanPham));

            var urlXoaSPHoaDon = $"/api/BanHangTaiQuay/XoaSanPhamKhoiHoaDon?maHD={maHD}&maSP={sanPham.MaSanPham}";

            var responeThanhToan = await client.DeleteAsync(urlXoaSPHoaDon);
            var soLuongThayDoi = responeThanhToan.Content.ReadAsStringAsync().Result;

            var tongTienThayDoi = -Convert.ToInt32(soLuongThayDoi) * (double)hoaDon.hoaDonChiTietDTOs.FirstOrDefault(c => c.SanPhamChiTietId == Guid.Parse(idSanPham)).Price;
            var soTienTraLaiThayDoi = -Convert.ToInt32(soLuongThayDoi) * (double)hoaDon.hoaDonChiTietDTOs.FirstOrDefault(c => c.SanPhamChiTietId == Guid.Parse(idSanPham)).PriceBan;
            var SoTienKhyenMaiGiam = tongTienThayDoi - soTienTraLaiThayDoi;


            //await _sanPhamChiTietService.UpDatSoLuongAynsc(new SanPhamSoLuongDTO()
            //{
            //    IdChiTietSanPham = idSanPham,
            //    SoLuong = (-Convert.ToInt32(soLuongThayDoi))
            //});



            var urlSPCT2 = $"/api/ChiTietSanPham/GetAllDto?status=1";
            var responeSPCT2 = client.GetAsync(urlSPCT2).Result;
            string dataSPCT2 = await responeSPCT2.Content.ReadAsStringAsync();
            //var responsedataSPCT = JsonConvert.DeserializeObject<ResponseDto>(dataSPCT);
            var sanPham2 = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(dataSPCT2).FirstOrDefault(sp => sp.Id == Guid.Parse(idSanPham));

            //var sanPham = (await _sanPhamChiTietService.GetDanhSachBienTheItemShopViewModelAsync()).FirstOrDefault(c => c.IdChiTietSp == idSanPham);
            return Ok(new
            {
                SoLuongConLai = sanPham2.SoLuongTon,
                TongTienThayDoi = tongTienThayDoi,
                SoTienTraLaiThayDoi = soTienTraLaiThayDoi,
                SoTienKhyenMaiGiam = SoTienKhyenMaiGiam,
                SoTienVoucherGiam = 0,
            });
        }

        public async Task<Guid> GetPTTT(string ten)
        {
            var client = _httpClientFactory.CreateClient("BeHat");
            var result = await client.GetStringAsync($"/api/PhuongThucThanhToan/PhuongThucThanhToanByName?name={ten}");

            if (Guid.TryParse(result.Trim('"'), out Guid idPTTT))
            {
                return idPTTT;
            }
            else
            {
                throw new Exception("Invalid GUID format");
            }
        }

        public async Task<bool> TaoPTTTChiTiet(Guid idHoaDon, Guid idPTTT, double soTien, int trangThai)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BeHat");
                //var _ptttId = Guid.Parse(idPTTT);
                var res = await client.PostAsync($"https://localhost:7050/AddPhuongThucThanhToanChiTietTaiQuay?IdHoaDon={idHoaDon}&IdThanhToan={idPTTT}&SoTien={soTien}&TrangThai={trangThai}", null);
                //https://localhost:7050/AddPhuongThucThanhToanChiTietTaiQuay?IdHoaDon=6268ef68-0cf6-433a-b4db-001029fa141c&IdThanhToan=d3d58019-cf00-48f8-bb0c-0ca8365d00bf&SoTien=250000&TrangThai=1

                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<bool>();
                }
                Console.WriteLine(await res.Content.ReadAsAsync<bool>());
                throw new Exception("Not IsSuccessStatusCode");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Not IsSuccessStatusCode");
            }
        }


        [HttpPost]
        public async Task<IActionResult> HuyHoaDon(string maHD, string lyDoHuy)
        {
            if (string.IsNullOrEmpty(maHD))
            {
                return Ok(new
                {
                    TrangThai = false,
                });
            }
            //var hoaDon = (await _hoaDonServices.GetAllHoaDonCho()).FirstOrDefault(c => c.MaHoaDon == maHD);

            var client = _httpClientFactory.CreateClient("BeHat");
            var url = $"/api/BanHangTaiQuay/GetAllHdTaiQuay";
            var respone = client.GetAsync(url).Result;
            string data = await respone.Content.ReadAsStringAsync();
            var hoaDonChoDtoList = JsonConvert.DeserializeObject<List<HoaDonChoDTO>>(data.ToString());
            var hoaDon = hoaDonChoDtoList.FirstOrDefault(x => x.MaHoaDon == maHD);


            //var user = await _userManager.GetUserAsync(User);
            //var idUser = await _userManager.GetUserIdAsync(user);
            //var hoadonchitiet = await _hoaDonChiTietServices.HuyHoaDon(maHD, lyDoHuy);
            var urlHuyHoaDon = $"/api/BanHangTaiQuay/HuyHoaDon?maHD={hoaDon.MaHoaDon}&lyDoHuy={lyDoHuy}";
            var responseHuyHoaDon = await client.PutAsync(urlHuyHoaDon, null);
            var readAsync = responseHuyHoaDon.Content.ReadAsStringAsync();
            var jsondataHuyHoaDon = responseHuyHoaDon.Content.ReadAsStringAsync();
            var reponseDto = JsonConvert.DeserializeObject<ResponseDto>(jsondataHuyHoaDon.Result.ToString());

            if (reponseDto.IsSuccess == true)
            {
                  return Ok(
                  new { TrangThai = true, }
                  );
            }
            return Ok(new
            {
                TrangThai = false,
            });

            //if (hoaDon.hoaDonChiTietDTOs.Count() == 0)
            //{
            //    return Ok(
            //      new { TrangThai = true, }
            //      );
            //}
            //if (hoadonchitiet.Any())
            //{
            //    foreach (var item in hoadonchitiet)
            //    {
            //        await UpDatSoLuongAynsc(new SanPhamSoLuongDTO()
            //        {
            //            IdChiTietSanPham = item.IdSanPhamChiTiet,
            //            SoLuong = -(int)item.SoLuong,
            //        });
            //    }
            //    return Ok(
            //        new { TrangThai = true, }
            //        );
            //}
            //return Ok(new
            //{
            //    TrangThai = false,
            //});
        }
    }
}
