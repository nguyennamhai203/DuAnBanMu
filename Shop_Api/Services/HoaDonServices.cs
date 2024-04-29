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
        private readonly HoaDonDto _reponseBill;
        private readonly ResponseDto _reponse;
        ApplicationDbContext _context;
        private readonly IPhuongThucThanhToanRepository _phuongThucThanhToanRepos;
        public HoaDonServices(UserManager<NguoiDung> userManager, IHoaDonRepository hoaDonRepository, IGioHangChiTietRepository gioHangCtRepository, IVoucherRepository voucherRepository,
            IHoaDonChiTietRepository hoaDonCTRepository, ApplicationDbContext applicationDbContext, IPhuongThucThanhToanRepository phuongThucThanhToanRepos)
        {
            _userManager = userManager;
            _hoaDonRepository = hoaDonRepository;
            _gioHangCtRepository = gioHangCtRepository;
            _voucherRepository = voucherRepository;
            _hoaDonCTRepository = hoaDonCTRepository;
            _context = applicationDbContext;
            _reponseBill = new HoaDonDto();
            _reponse = new ResponseDto();
            _phuongThucThanhToanRepos = phuongThucThanhToanRepos;
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
                var tongTienSanPham = 0;
                foreach (var x in requestBill.CartItem)
                {

                    tongTienSanPham = (int)(x.GiaBan * x.SoLuong);
                   
                }
                var listVoucher = await _voucherRepository.GetAll();
                var voucherX = listVoucher.FirstOrDefault(x => x.MaVoucher == requestBill.CodeVoucher);
                var tienGiam = 0;
                if (voucherX != null)
                {
                    tienGiam = (tongTienSanPham / 100) * voucherX.PhanTramGiam.Value;
                }
                else tienGiam = 0;
                var bill = new HoaDon
                {
                    Id = Guid.NewGuid(),
                    MaHoaDon = requestBill.MaHoaDon != null ? requestBill.MaHoaDon : "Bill" + GenerateRandomString(10),
                    NgayTao = DateTime.Now,
                    //NgayShip = DateTime.Now,
                    //NgayNhan = DateTime.Now,
                    //NgayThanhToan = DateTime.Now,
                    //NgayGiaoDuKien = DateTime.Now,
                    TrangThaiGiaoHang = 0, // chờ xác nhận
                    TrangThaiThanhToan = 0, // chờ thanh toán
                    TenKhachHang = user != null ? user.TenNguoiDung : requestBill.FullName,
                    SoDienThoai = requestBill.PhoneNumber,
                    DiaChiGiaoHang = requestBill.Address,
                    NguoiDungId = user != null ? user.Id : null,
                    TongTien = requestBill.Payment /*- tienGiam + 40000*/,
                    TienGiam = tienGiam,
                    TienShip = 40000, // mặc định ship
                    VoucherId = voucherX != null ? voucherX.Guid : (Guid?)null
                };
                if (requestBill.trangthaithanhtoan == 1)
                {
                    bill.NgayThanhToan = DateTime.Now;
                    bill.TrangThaiThanhToan = 1;
                }
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
                    if (pttt == null)
                    {
                        var ptttCreate = new PhuongThucThanhToan();
                        ptttCreate.TenMaPTThanhToan = requestBill.MaPTTT;
                        ptttCreate.MaPTThanhToan = requestBill.MaPTTT;
                        ptttCreate.MoTa = "";
                        ptttCreate.TrangThai = 1;

                        await _phuongThucThanhToanRepos.CreateAsync(ptttCreate);
                    }
                    if (requestBill.trangthaithanhtoan == 0)
                    {
                        PhuongThucTTChiTiet phuongThucTTChiTiet = new PhuongThucTTChiTiet()
                        {
                            Id = Guid.NewGuid(),
                            HoaDonId = bill.Id,
                            PTTToanId = pttt.Id,
                            TrangThai = 0, // trạng thái chưa thanh toán
                            SoTien = 0,

                        }; await _context.PhuongThucTTChiTiets.AddAsync(phuongThucTTChiTiet);
                        _context.SaveChanges();
                    }
                    else
                    {
                        PhuongThucTTChiTiet phuongThucTTChiTiet = new PhuongThucTTChiTiet()
                        {
                            Id = Guid.NewGuid(),
                            HoaDonId = bill.Id,
                            PTTToanId = pttt.Id,
                            TrangThai = 1, // trạng thái đã thanh toán
                            SoTien = bill.TongTien,

                        }; await _context.PhuongThucTTChiTiets.AddAsync(phuongThucTTChiTiet);
                        _context.SaveChanges();
                    }

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

        public async Task<ResponseDto> PGetBillByInvoiceCode(string invoiceCode)
        {
            var billT = await _hoaDonRepository.GetBillByInvoiceCode(invoiceCode);
            if (billT != null)
            {
                var listBillDetail = await _hoaDonRepository.GetBillDetailByInvoiceCode(invoiceCode);
                _reponseBill.InvoiceCode = billT.InvoiceCode;
                _reponseBill.PhoneNumber = billT.PhoneNumber;
                _reponseBill.TenKhachHang = billT.TenKhachHang;
                _reponseBill.FullName = billT.FullName;
                _reponseBill.Address = billT.Address;
                _reponseBill.TienShip = billT.TienShip;
                _reponseBill.TrangThaiGiaoHang = billT.TrangThaiGiaoHang;
                _reponseBill.TrangThaiThanhToan = billT.TrangThaiThanhToan;
                _reponseBill.CreateDate = billT.CreateDate;
                _reponseBill.NgayThanhToan = billT.NgayThanhToan;
                _reponseBill.CodeVoucher = billT.CodeVoucher;
                _reponseBill.GiamGia = billT.GiamGia;
                _reponseBill.Payment = billT.Payment;
                _reponseBill.TienGiam = billT.TienGiam;
                _reponseBill.ThanhTien = billT.ThanhTien;
                _reponseBill.IsPayment = billT.IsPayment;
                _reponseBill.UserId = billT.UserId;
                _reponseBill.BillDetail = listBillDetail;
                _reponseBill.Count = listBillDetail.Count();
                _reponseBill.Phuongthucthanhtoan = billT.Phuongthucthanhtoan;
                _reponse.Message = $"Lấy hóa đơn {invoiceCode} thành công.";
                _reponse.Content = _reponseBill;
                return _reponse;
            }
            _reponse.Code = 404;
            _reponse.IsSuccess = false;
            _reponse.Message = $"Không tìm thấy hóa đơn {invoiceCode}.";
            return _reponse;
        }
    }
}
