using Microsoft.AspNetCore.Identity;
using Shop_Api.AppDbContext;
using Shop_Api.Repository;
using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Text;

namespace Shop_Api.Services
{
    public class HoaDonServices : IHoaDonServices
    {

        private readonly UserManager<NguoiDung> _userManager;
        private readonly IHoaDonRepository _hoaDonRepository;
        private readonly IHoaDonChiTietRepository _hoaDonCTRepository;
        private readonly IGioHangChiTietRepository _gioHangCtRepository;
        private readonly IVoucherRepository _voucherRepository;
        ApplicationDbContext _context;
        public HoaDonServices(UserManager<NguoiDung> userManager, IHoaDonRepository hoaDonRepository, IGioHangChiTietRepository gioHangCtRepository, IVoucherRepository voucherRepository,
            IHoaDonChiTietRepository hoaDonCTRepository, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _hoaDonRepository = hoaDonRepository;
            _gioHangCtRepository = gioHangCtRepository;
            _voucherRepository = voucherRepository;
            _hoaDonCTRepository = hoaDonCTRepository;
            _context = applicationDbContext;
        }

        public async Task<ResponseDto> CreateBill(RequestBillDto requestBill)
        {

            try
            {
                var user = _context.NguoiDungs.FirstOrDefault(x => x.UserName == requestBill.Usename);



                //if (cartItem == null)
                //{
                //    return NotFoundResponse("Không có sản phẩm trong giỏ hàng");
                //}

                var listVoucher = await _voucherRepository.GetAll();
                var voucherX = listVoucher.FirstOrDefault(x => x.MaVoucher == requestBill.CodeVoucher);
                var tienGiam = 0;
                if (voucherX != null)
                {
                    tienGiam = requestBill.Payment - voucherX.PhanTramGiam.Value;
                }
                else tienGiam = 0;
                var bill = new HoaDon
                {
                    Id = Guid.NewGuid(),
                    MaHoaDon = "Bill" + GenerateRandomString(10),
                    NgayTao = DateTime.Now,
                    NgayShip = DateTime.Now,
                    NgayNhan = DateTime.Now,
                    NgayThanhToan = DateTime.Now,
                    NgayGiaoDuKien = DateTime.Now,
                    TrangThaiGiaoHang = 0, // chờ xác nhận
                    TrangThaiThanhToan = 0, // chờ thanh toán
                    TenKhachHang = user != null ? user.TenNguoiDung : requestBill.FullName,
                    SoDienThoai = requestBill.PhoneNumber,
                    DiaChiGiaoHang = requestBill.Address,
                    NguoiDungId = user != null ? user.Id : null,
                    TongTien = requestBill.Payment,
                    TienGiam = tienGiam,
                    TienShip = 0,
                    VoucherId = voucherX != null ? voucherX.Guid : (Guid?)null
                };
                var createHoaDon = await _hoaDonRepository.CreateHD(bill);
                if (createHoaDon.IsSuccess == true)
                {
                    if (requestBill.Usename == null)
                    {
                        foreach (var x in requestBill.CartItem)
                        {
                            var spct = _context.ChiTietSanPhams.FirstOrDefault(a => a.MaSanPham == x.MaSPCT);
                            var billDetail = new HoaDonChiTiet
                            {
                                Guid = Guid.NewGuid(),
                                ChiTietSanPhamId = spct.Id,
                                GiaBan = x.GiaBan,
                                SoLuong = x.SoLuong,
                                HoaDonId = bill.Id
                            };

                            await _hoaDonCTRepository.CreateAsync(billDetail);
                        }

                    }
                    else
                    {
                        var cartItem = await _gioHangCtRepository.GetCartDetailByUserName(user.UserName);
                        foreach (var cartItemDetail in cartItem)
                        {
                            var billDetail = new HoaDonChiTiet
                            {
                                Guid = Guid.NewGuid(),
                                ChiTietSanPhamId = cartItemDetail.ChiTietSanPhamId,
                                GiaBan = cartItemDetail.GiaBan,
                                SoLuong = cartItemDetail.SoLuong,
                                HoaDonId = bill.Id
                            };

                            await _hoaDonCTRepository.CreateAsync(billDetail);
                        }
                    }
                    var pttt = _context.PhuongThucThanhToans.FirstOrDefault(x => x.MaPTThanhToan == requestBill.MaPTTT);
                    PhuongThucTTChiTiet phuongThucTTChiTiet = new PhuongThucTTChiTiet()
                    {
                        Id = Guid.NewGuid(),
                        HoaDonId = bill.Id,
                        PTTToanId = pttt.Id,
                        TrangThai = 0, // trạng thái chưa thanh toán
                        SoTien = 0,

                    };
                    await _context.PhuongThucTTChiTiets.AddAsync(phuongThucTTChiTiet);
                    _context.SaveChanges();
                    //return SuccessResponse(bill, $"{bill.InvoiceCode}");
                    return new ResponseDto
                    {
                        Content = bill,
                        Code = 200,
                        Message = "Đặt hàng thành công"
                    };
                }
                else
                    return new ResponseDto
                    {
                        Content = bill,
                        Code = 400,
                        Message = "Đặt hàng thất bại"
                    };
            }
            catch (Exception e)
            {
                return new ResponseDto
                {
                    Content = e.Message,
                    Code = 500,
                    Message = "Lỗi không xác định"
                };
            }
        }
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder randomString = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                randomString.Append(chars[index]);
            }

            return randomString.ToString();
        }
    }
}
