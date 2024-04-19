using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Shop_Models.Heplers;
using System.Data.Entity;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeViewModelController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IThongKeViewModelRepository thongKeViewModelRepository;
        private readonly IChiTietSanPhamRepository chiTietSanPhamRepository;
        private readonly ISanPhamRepository sanPhamRepository;
        private readonly IHoaDonRepository hoaDonRepository;
        public ThongKeViewModelController(ApplicationDbContext ct,IThongKeViewModelRepository tk,IChiTietSanPhamRepository ctsp,ISanPhamRepository sp,IHoaDonRepository hd)
        {
            context = ct;
            thongKeViewModelRepository = tk;
            chiTietSanPhamRepository = ctsp;
            sanPhamRepository = sp;
            hoaDonRepository = hd;
        }

        [HttpGet("Thong-ke-san-pham-update")]
        public async Task<IActionResult> GetThongKe(int? status,int page)
        {
            try
            {
                var getctsp = await chiTietSanPhamRepository.GetAllAsync2(status, page);
                var gethd = await hoaDonRepository.GetAsync(status, page);
                var getsp = await sanPhamRepository.GetAsync(status,page);
                var list = (from ctsp in getctsp
                           join hd in gethd on ctsp.HoaDonId equals hd.Id
                           join sp in getsp on ctsp.SanPhamId equals sp.IdSanPham
                           select new ThongKeViewModel
                           {
                               MaSanPham = sp.MaSanPham,
                               TenSanPham = sp.TenSanPham,
                               GiaNhap = Convert.ToDouble(ctsp.GiaNhap),
                               GiaBan = Convert.ToDouble(ctsp.GiaBan),
                               SoLuongTon = Convert.ToInt32(ctsp.SoLuongTon),
                               SoLuongDaBan = Convert.ToInt32(ctsp.SoLuongDaBan),
                               MaHoaDon = hd.MaHoaDon,
                               NgayTao = Convert.ToDateTime(hd.NgayTao),
                               NgayThanhToan = Convert.ToDateTime(hd.NgayThanhToan),
                               NgayShip = Convert.ToDateTime(hd.NgayShip),
                               NgayNhan = Convert.ToDateTime(hd.NgayNhan),
                               TienGiam = Convert.ToDouble(hd.TienGiam),
                               TienShip = Convert.ToDouble(hd.TienShip),
                               NgayThongKe = Convert.ToInt32(hd.NgayThanhToan.Day),
                               ThangThongKe = Convert.ToInt32(hd.NgayThanhToan.Month),
                               NamThongKe = Convert.ToInt32(hd.NgayThanhToan.Year),
                               TongTienThongKe =
                               Convert.ToDouble(ctsp.GiaBan) * Convert.ToInt32(ctsp.SoLuongDaBan),
                               PhanTramLoiNhuan =
                               ((Convert.ToDouble(ctsp.GiaBan) - Convert.ToDouble(ctsp.GiaNhap)) * Convert.ToInt32(ctsp.SoLuongDaBan)
                               / (Convert.ToDouble(ctsp.GiaBan) * Convert.ToInt32(ctsp.SoLuongDaBan))) * 100,
                               PhanTramLo =
                               ((Convert.ToDouble(ctsp.GiaNhap) - Convert.ToDouble(ctsp.GiaBan)) * Convert.ToInt32(ctsp.SoLuongDaBan)
                               / (Convert.ToDouble(ctsp.GiaNhap) * Convert.ToInt32(ctsp.SoLuongDaBan))) * 100
                           });
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}