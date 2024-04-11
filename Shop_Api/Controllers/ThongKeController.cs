using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.Heplers;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeRepository _repository;
        private readonly ApplicationDbContext _context;
        public ThongKeController(IThongKeRepository repository,ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int? status, int page = 1)
        {
            var result = await _repository.GetAsync(status, page);
            return Ok(result);
        }

        [Authorize(Roles = AppRole.Admin)]
        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(ThongKe obj)
        {
            var respon = await _repository.CreateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [Authorize(Roles = AppRole.Admin)]
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(ThongKe obj)
        {
            var respon = await _repository.UpdateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [Authorize(Roles = AppRole.Admin)]
        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var respon = await _repository.DeleteAsync(Id);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [Authorize(Roles = AppRole.Admin)]
        [HttpGet("Thong-ke-san-pham-theo-ngay")]
        public IActionResult ThongKeSanPhamTheoNgay(DateTime ngay)
        {
            var ngaybatdau = ngay.Date;
            var ngayketthuc = ngaybatdau.AddMonths(1).AddDays(-1);
            var thongketheongay = _context.HoaDons
                .Where(b => b.NgayThanhToan >= ngaybatdau && b.NgayThanhToan <= ngayketthuc)
                .Join(
                    _context.HoaDonChiTiets,
                    hd => hd.Id,
                    hdct => hdct.HoaDonId,
                    (hd, hdct) => new
                    {
                        Daily = hd.NgayThanhToan.HasValue ? hd.NgayThanhToan.Value.Day : 0,
                        Amount = hdct.GiaBan * hdct.SoLuong
                    }
                )
                .GroupBy(result => result.Daily)
                .Select(group => new
                {
                    Day = group.Key,
                    Amount = group.Sum(result => result.Amount),
                    TotalOrders = group.Count(),
                }).ToList();
            return Ok(thongketheongay);
        }
    }
}