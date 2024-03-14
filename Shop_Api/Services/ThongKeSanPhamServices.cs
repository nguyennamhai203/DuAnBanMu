using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Services
{
    public class ThongKeSanPhamServices : IThongKeSanPhamServices
    {
        private readonly IHoaDonRepository hoaDonRes;
        private readonly IChiTietSanPhamRepository chiTietSanPhamRes;
        private readonly IThongKeRepository thongKeRes;

        public ThongKeSanPhamServices(IHoaDonRepository hd,IChiTietSanPhamRepository ctsp,IThongKeRepository tk)
        {
            hoaDonRes = hd;
            chiTietSanPhamRes = ctsp;
            thongKeRes = tk;
        }
        /*
         * [AllowAnonymous]
[HttpGet("GetRevenueStatistics")]
public IActionResult GetRevenueStatistics(int year)
{
    var revenueData = _context.BillDetails
        .Where(bd => bd.Bill.CreateDate.Year == year)
        .GroupBy(bd => new { Month = bd.Bill.CreateDate.Month })
        .Select(g => new RevenueDto
        {
            Date = new DateTime(year, g.Key.Month, 1),
            Amount = (decimal)g.Sum(bd => bd.Quantity * bd.Price)
        })
        .ToList();

    return Ok(revenueData);
}


[AllowAnonymous]
[HttpGet("GetRevenueStatisticss")]
public IActionResult GetRevenueStatisticss(DateTime selectedDate)
{
    var startDate = selectedDate.Date;
    var endDate = startDate.AddMonths(1).AddDays(-1);

    var dailyStatistics = _context.Bills
        .Where(b => b.CreateDate >= startDate && b.CreateDate <= endDate)
        .Join(
            _context.BillDetails,
            bill => bill.Id,
            billDetail => billDetail.BillId,
            (bill, billDetail) => new
            {
                Day = bill.CreateDate.Day,
                Amount = billDetail.Price * billDetail.Quantity,
                // Add other fields as needed
            }
        )
        .GroupBy(result => result.Day)
        .Select(group => new
        {
            Day = group.Key,
            Amount = group.Sum(result => result.Amount),
            TotalOrders = group.Count(),
            // Add other fields as needed
        })
        .ToList();
    return Ok(dailyStatistics);
}



[AllowAnonymous]
[HttpGet("test")]
public IActionResult test(int? year, int? month)
{
    var startDate = year.HasValue && month.HasValue
        ? new DateTime(year.Value, month.Value, 1)
        : DateTime.MinValue;

    var endDate = startDate != DateTime.MinValue
        ? startDate.AddMonths(1).AddDays(-1)
        : DateTime.MaxValue;

    var statistics = _context.Bills
        .Where(b => b.CreateDate >= startDate && b.CreateDate <= endDate)
        .Join(
            _context.BillDetails,
            bill => bill.Id,
            billDetail => billDetail.BillId,
            (bill, billDetail) => new
            {
                Date = bill.CreateDate,
                Amount = billDetail.Price * billDetail.Quantity,
                // Add other fields as needed
            }
        )
        .GroupBy(result => result.Date)
        .Select(group => new
        {
            Date = group.Key,
            Amount = group.Sum(result => result.Amount),
            TotalOrders = group.Count(),
            // Add other fields as needed
        })
        .ToList();

    return Ok(statistics);
}
         */
        public async Task<ResponseDto> GetThongKeSanPhamTheoNgay(DateTime datetime,int? status,int page = 1)
        {
            /*
             *  solution 1 : tìm hóa đơn đã giao và đã thanh toán --> truyền ngày tháng năm để thống kê theo ngày tháng năm
             *  hoặc có thể tìm hóa đơn theo ngày tháng năm sau đó tiếp tục kiểm tra trạng thái của hóa đơn để mình thống kê
             */
            var timngaytronghoadon = hoaDonRes.GetAsync(status, page).Result.Where(x => x.NgayThanhToan == datetime);
            try
            {
                var thongke = (from tk in thongKeRes.GetAsync(status, page).Result
                              join ctsp in chiTietSanPhamRes.GetAsync(/*status, page*/).Result on tk.SanPhamChiTietId equals ctsp.Id
                              join hd in hoaDonRes.GetAsync(status, page).Result on tk.HoaDonId equals hd.Id
                              select new ThongKe 
                              {
                                  Id = tk.Id,
                                  Ngay = tk.Ngay,
                                  Thang = tk.Thang,
                                  Nam = tk.Nam,
                              }).ToList();
                return new ResponseDto
                {
                    Code = 200,
                    Message = "thong ke thanh cong",
                    Result = thongke
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = "khong tim thay so lieu"
                };
            }
        }
    }
}