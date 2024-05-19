using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Shop_Api.AppDbContext;
using Shop_Api.Migrations;
using Shop_Api.Repository;
using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;
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
        private readonly IChiTietSanPhamRepository _reposSanPhamChiTiet;

        public HoaDonServices(UserManager<NguoiDung> userManager, IHoaDonRepository hoaDonRepository, IGioHangChiTietRepository gioHangCtRepository, IVoucherRepository voucherRepository,
            IHoaDonChiTietRepository hoaDonCTRepository, ApplicationDbContext applicationDbContext, IPhuongThucThanhToanRepository phuongThucThanhToanRepos, IChiTietSanPhamRepository reposSanPhamChiTiet)
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
            _reposSanPhamChiTiet = reposSanPhamChiTiet;
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
                        var ptttAfterCreate = _context.PhuongThucThanhToans.FirstOrDefault(x => x.MaPTThanhToan == requestBill.MaPTTT);
                        PhuongThucTTChiTiet phuongThucTTChiTiet = new PhuongThucTTChiTiet()
                        {
                            Id = Guid.NewGuid(),
                            HoaDonId = bill.Id,
                            PTTToanId = ptttAfterCreate.Id,
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
                _reponseBill.IdHoaDon = billT.IdHoaDon;
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

        public Task<ResponseDto> CreateHoaDonTaiQuay(RequestBillDto requestBill)
        {
            throw new NotImplementedException();
        }

        public List<HoaDonChoDTO> GetAllHDTaiQuay()
        {

            var query = from bill in _context.HoaDons
                        where bill.TrangThaiGiaoHang == 5 && bill.TrangThaiThanhToan == 0
                        join v in _context.Vouchers on bill.VoucherId equals v.Guid into voucherGroup
                        from voucher in voucherGroup.DefaultIfEmpty()

                        join ptttct in _context.PhuongThucTTChiTiets on bill.Id equals ptttct.HoaDonId into ptttctGroup
                        from thanhtoanchitiet in ptttctGroup.DefaultIfEmpty()

                        join pttt in _context.PhuongThucThanhToans on thanhtoanchitiet.PTTToanId equals pttt.Id into ptttGroup
                        from phuongthucthanhtoan in ptttGroup.DefaultIfEmpty()

                        join user in _context.NguoiDungs on bill.NguoiDungId equals user.Id into userGroup
                        from u in userGroup.DefaultIfEmpty()
                            //where bill.NguoiDungId != Guid.Empty

                        select new HoaDonChoDTO
                        {
                            Id = bill.Id,
                            MaHoaDon = bill.MaHoaDon,
                            SDT = bill.SoDienThoai,
                            TenKhachHang = bill.TenKhachHang,
                            TrangThaiGiaoHang = bill.TrangThaiGiaoHang,
                            TrangThaiThanhToan = bill.TrangThaiThanhToan,
                            NgayTao = bill.NgayTao,
                            TienGiam = (int)bill.TienGiam,
                            GiamGia = voucher != null ? voucher.PhanTramGiam : 0,
                            CodeVoucher = voucher != null ? voucher.MaVoucher : null,
                            IdNguoiDung = bill.NguoiDungId,
                            ThanhTien = (int)bill.TongTien,
                            Phuongthucthanhtoan = phuongthucthanhtoan.TenMaPTThanhToan,
                            NguoiTao = bill.NguoiDungId == null ? "..." : u.TenNguoiDung
                            //hoaDonChiTietDTOs = DanhSachHoaDonChiTietTaiQuay(bill.MaHoaDon)
                        };
            var result = query.ToList();
            foreach (var item in result)
            {
                item.hoaDonChiTietDTOs = DanhSachHoaDonChiTietTaiQuay(item.MaHoaDon);
            }
            return result.ToList();
        }


        public List<HoaDonChiTietDto> DanhSachHoaDonChiTietTaiQuay(string invoiceCode)
        {
            try
            {
                var billDetails = (
                    from x in _context.HoaDons.AsNoTracking().Where(a => a.MaHoaDon == invoiceCode)
                    join y in _context.HoaDonChiTiets.AsNoTracking() on x.Id equals y.HoaDonId
                    join z in _context.ChiTietSanPhams.AsNoTracking() on y.ChiTietSanPhamId equals z.Id
                    select new HoaDonChiTietDto
                    {
                        Id = y.Guid,
                        HoaDonId = x.Id,
                        SanPhamChiTietId = y.ChiTietSanPhamId,
                        CodeProductDetail = z.MaSanPham,
                        Quantity = y.SoLuong,
                        Price = (float)z.GiaThucTe,
                        PriceBan = (float)y.GiaBan
                    }).ToList();
                return billDetails;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResponseDto TaoHoaDonTaiQuay(RequestHDTaiQuay _requestHdTaiQuay)
        {
            //var _searchVoucher = _voucherRepository.GetVoucher().Result.FirstOrDefault(x=>x.MaVoucher==_requestHdTaiQuay.MaVoucher);
            //if (_searchVoucher != null)
            //{
            //    var tienGiamVoucher = _
            //}
            HoaDon hdtq = new HoaDon()
            {
                Id = Guid.NewGuid(),
                NguoiDungId = _requestHdTaiQuay.IdNguoiDung,// người tạo hóa đơn admin hoặc nhân viên
                NgayTao = DateTime.Now,
                //TongTien = (double)(_requestHdTaiQuay.TongThanhToan != null ? _requestHdTaiQuay.TongThanhToan : 0),
                MaHoaDon = TaoMaHoaDon(),
                // Gán các thông tin cần thiết cho hóa đơn
                TrangThaiGiaoHang = 5, // trạng thái hóa đơn tại quầy!
                TrangThaiThanhToan = 0, // trạng thái chưa thanh toán!
            };
            var result = _context.HoaDons.Add(hdtq);
            _context.SaveChanges();

            if (result.State != 0)
            {
                return new ResponseDto
                {
                    IsSuccess = true,
                    Code = 200,
                    Content = hdtq,
                    Message = "Tạo Hóa Đơn Thành Công"
                };
            }
            else return new ResponseDto
            {
                IsSuccess = false,
                Code = 400,
                Message = "Tạo Hóa Đơn Thất Bại"
            };
        }

        public string TaoMaHoaDon()
        {
            // lấy só lượng hóa đơn
            var countHoaDon = _context.HoaDons.Count(x => x.MaHoaDon.StartsWith("HDTQ"));
            //Tạo Hóa Đơn Mới 
            string maHoaDon = "HDTQ" + (countHoaDon + 1).ToString();
            return maHoaDon;
        }


        public async Task<ResponseDto> AddBillDetail(string mahoadon, string codeProductDetail, int? soluong)
        {
            try
            {
                if (soluong == null || soluong == 0)
                {
                    soluong = 1;
                }
                else soluong = soluong.Value;

                var sanPhamChiTietDTO1 = _reposSanPhamChiTiet.PGetProductDetail(null, codeProductDetail, null, null, null, null, null, null, null, null, null, null, null, null).Result.FirstOrDefault();
                var sanPhamChiTietDTO = _reposSanPhamChiTiet.GetAllAsync(1).Result.Where(x => x.MaSanPhamChiTiet == codeProductDetail).FirstOrDefault();

                var sanPhamChiTiet = await _reposSanPhamChiTiet.GetAsync();

                if (sanPhamChiTietDTO == null)
                {
                    return new ResponseDto { IsSuccess = false, Code = 405, Message = "Sản Phẩm Không Tồn Tại" };
                }

                else if (soluong <= 0)
                {
                    return new ResponseDto { IsSuccess = false, Code = 400, Message = "so luong phai lon hon 0" };

                }



                var _getBillByInvoiceCode = _hoaDonRepository.GetBillByInvoiceCode(mahoadon).Result;
                if (_getBillByInvoiceCode != null)
                {
                    var checkProductInBillDetail = _hoaDonCTRepository.GetAllAsync().Result.FirstOrDefault(x => x.HoaDonId == _getBillByInvoiceCode.IdHoaDon && x.ChiTietSanPhamId == sanPhamChiTietDTO.Id);
                    if (checkProductInBillDetail != null)
                    {
                        checkProductInBillDetail.SoLuong += (int)soluong;
                        var update = Update(checkProductInBillDetail);
                        if (update.IsSuccess)
                        {
                            var spct = _reposSanPhamChiTiet.GetAsync().Result.Where(x => x.Id == sanPhamChiTietDTO.Id).FirstOrDefault();
                            spct.SoLuongTon = sanPhamChiTietDTO.SoLuongTon - 1;
                            _context.ChiTietSanPhams.Update(spct);
                            _context.SaveChanges();
                            return new ResponseDto { Content = update.Content, IsSuccess = true, Code = 200, Message = "Cập sản phẩm vào giỏ hàng thành công" };
                        }
                        else if (sanPhamChiTietDTO.SoLuongTon < checkProductInBillDetail.SoLuong + soluong)
                        {
                            return new ResponseDto { IsSuccess = false, Code = 400, Message = "Vượt quá số lượng" };

                        }
                        return new ResponseDto { IsSuccess = false, Code = 400, Message = "Không thể cập nhật số lượng sản phẩm trong giỏ hàng" };


                    }
                    else
                    {
                        var newBillDetail = new HoaDonChiTiet
                        {
                            ChiTietSanPhamId = sanPhamChiTietDTO.Id,
                            HoaDonId = _getBillByInvoiceCode.IdHoaDon,
                            SoLuong = 1,
                            GiaBan = (double)sanPhamChiTietDTO.GiaBan,

                        };

                        var spct = _reposSanPhamChiTiet.GetAsync().Result.Where(x => x.Id == sanPhamChiTietDTO.Id).FirstOrDefault();
                        spct.SoLuongTon = sanPhamChiTietDTO.SoLuongTon - 1;
                        _context.ChiTietSanPhams.Update(spct);
                        _context.SaveChanges();
                        var create = Create(newBillDetail);
                        if (create.IsSuccess)
                        {
                            return new ResponseDto { Content = create.Content, IsSuccess = true, Code = 200, Message = "Thêm sản phẩm vào giỏ hàng thành công" };

                        }
                        return new ResponseDto { IsSuccess = false, Code = 404, Message = "Không thể thêm sản phẩm vào trong giỏ hàng" };

                    }
                }
                else
                {


                    return new ResponseDto { IsSuccess = false, Code = 405, Message = "Không tìm thấy hóa đơn" };

                }
            }
            catch (Exception e)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = e.Message };
            }
        }



        public ResponseDto Update(HoaDonChiTiet _getBillByInvoiceCode)
        {
            var getById = _context.HoaDonChiTiets.FirstOrDefault(x => x.HoaDonId == _getBillByInvoiceCode.HoaDonId
            && x.ChiTietSanPhamId == _getBillByInvoiceCode.ChiTietSanPhamId);
            if (getById.SoLuong <= 0)
            {
                var delete = _context.HoaDonChiTiets.Remove(getById);
                _context.SaveChanges();
                return new ResponseDto { Message = "xóa do số lượng hdct là 0", Code = 200 };
            }
            else
            {
                var result = _context.HoaDonChiTiets.Update(getById);
                _context.SaveChanges();
                if (result.State != 0)
                {
                    var allHDTQ = GetAllHDTaiQuay();
                    var HDTQ = allHDTQ.FirstOrDefault(x => x.Id == result.Entity.HoaDonId).hoaDonChiTietDTOs;
                    var HDCTTQ = HDTQ.FirstOrDefault(x => x.Id == result.Entity.Guid);

                    return new ResponseDto { Content = HDCTTQ, Message = "OK", Code = 200 };
                }
                return new ResponseDto { Message = "Fail", Code = 400 };
            }

        }
        public ResponseDto Create(HoaDonChiTiet _getBillByInvoiceCode)
        {

            var result = _context.HoaDonChiTiets.Add(_getBillByInvoiceCode);
            _context.SaveChanges();

            if (result.State != 0)
            {
                var allHDTQ = GetAllHDTaiQuay();
                var HDTQ = allHDTQ.FirstOrDefault(x => x.Id == result.Entity.HoaDonId).hoaDonChiTietDTOs;
                var HDCTTQ = HDTQ.FirstOrDefault(x => x.Id == result.Entity.Guid);
                return new ResponseDto { Content = HDCTTQ, Message = "OK", Code = 200 };
            }
            return new ResponseDto { Message = "Fail", Code = 400 };
        }

        public ResponseDto CapNhatSoLuongHoaDonChiTietTaiQuay(string maHoaDon, string maSPCT, int soLuong)
        {
            try
            {
                var _getBillByInvoiceCode = _hoaDonRepository.GetBillByInvoiceCode(maHoaDon).Result;
                var sanPhamChiTietDTO = _reposSanPhamChiTiet.GetAllAsync(1).Result.Where(x => x.MaSanPhamChiTiet == maSPCT).FirstOrDefault();

                var billDetail = _hoaDonCTRepository.GetAllAsync().Result.FirstOrDefault(x => x.HoaDonId == _getBillByInvoiceCode.IdHoaDon && x.ChiTietSanPhamId == sanPhamChiTietDTO.Id);

                if (billDetail == null)
                {
                    return new ResponseDto { IsSuccess = false, Code = 404, Message = "Không tìm thấy giỏ hàng chi tiết" };
                }

                if (soLuong > sanPhamChiTietDTO.SoLuongTon + billDetail.SoLuong)
                {
                    return new ResponseDto { IsSuccess = false, Code = 404, Message = "Vượt quá số lượng tồn!" };
                }
                var SLThayDoi = soLuong - billDetail.SoLuong;
                billDetail.SoLuong = soLuong;
                //var checkProductDetailInBillDetail = billDetail.SoLuong;
                //GioHangChiTiet cartDetail = new GioHangChiTiet
                //{
                //    Id = idCartDetail,
                //    SoLuong = soLuong
                //};

                //if (cartDetail.SoLuong < 0)
                //{
                //    return await HandleNegativeQuantity(cartDetail);
                //}


                var spct = _reposSanPhamChiTiet.GetAsync().Result.Where(x => x.Id == sanPhamChiTietDTO.Id).FirstOrDefault();

                //var spct = new ChiTietSanPham()
                //{

                spct.SoLuongTon = sanPhamChiTietDTO.SoLuongTon - SLThayDoi;
                //};
                _context.ChiTietSanPhams.Update(spct);
                _context.SaveChanges();
                var update = Update(billDetail);
                if (update.IsSuccess)
                {
                    return new ResponseDto { Content = update.Content, IsSuccess = true, Code = 200, Message = "Thành Công Cập nhật số lượng" };

                }
                else
                {
                    return new ResponseDto { IsSuccess = false, Code = 400, Message = "Không thành công" };
                }
            }
            catch (Exception e)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = e.Message };

            }
        }

        public async Task<ResponseDto> DeleteHDTQ(string maHoaDon, string maSPCT)
        {
            try
            {
                var _getBillByInvoiceCode = _hoaDonRepository.GetBillByInvoiceCode(maHoaDon).Result;
                var sanPhamChiTietDTO = _reposSanPhamChiTiet.GetAllAsync(1).Result.Where(x => x.MaSanPhamChiTiet == maSPCT).FirstOrDefault();

                var billDetail = _hoaDonCTRepository.GetAllAsync().Result.FirstOrDefault(x => x.HoaDonId == _getBillByInvoiceCode.IdHoaDon && x.ChiTietSanPhamId == sanPhamChiTietDTO.Id);

                if (billDetail == null)
                {
                    return new ResponseDto { IsSuccess = false, Code = 405, Message = "Không tìm thấy giỏ hàng chi tiết" };
                }

                //var checkProductDetailInbill = billDetail.SoLuong;
                //HoaDonChiTiet billDetail = new HoaDonChiTiet
                //{
                //    Id = idbillDetail,
                //};

                //if (billDetail.SoLuong < 0)
                //{
                //    return await HandleNegativeQuantity(billDetail);
                //}

                if (_hoaDonCTRepository.DeleteAsync(billDetail.Guid).Result.IsSuccess == true)
                {
                    return new ResponseDto { IsSuccess = false, Code = 200, Message = "Xóa sản phẩm thành công ra khỏi giỏ hàng" };

                }
                else
                {
                    return new ResponseDto { IsSuccess = false, Code = 400, Message = "Thất bại. Vui lòng thử lại" };

                }
            }
            catch (Exception e)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = e.Message };
            }
        }

        public async Task<ResponseDto> TruQuantityBillDetail(Guid idCartDetail)
        {
            return await CongOrTruQuantityBillDetail(idCartDetail, -1);
        }
        public async Task<ResponseDto> CongQuantityBillDetail(Guid idBillDetail)
        {
            return await CongOrTruQuantityBillDetail(idBillDetail, 1);
        }

        public async Task<ResponseDto> CongOrTruQuantityBillDetail(Guid idCartDetail, int changeAmount)
        {
            try
            {
                var billDetail = await _hoaDonCTRepository.GetByIdAsync(idCartDetail);

                if (billDetail == null)
                {
                    return new ResponseDto { IsSuccess = false, Code = 405, Message = "Không tìm thấy hóa đơn chi tiết" };

                }

                //var checkProductDetailInCart = billDetail.SoLuong;
                //HoaDonChiTiet cartDetail = new HoaDonChiTiet
                //{
                //    Guid = idCartDetail,
                //    SoLuong = billDetail.SoLuong + changeAmount
                //};
                billDetail.SoLuong += changeAmount;
                //if (cartDetail.SoLuong < 0)
                //{
                //    return await HandleNegativeQuantity(cartDetail);
                //}
                if (billDetail.SoLuong == 0)
                {
                    var result = await _hoaDonCTRepository.DeleteAsync(billDetail.Guid);
                    return new ResponseDto { IsSuccess = true, Code = 200, Message = $"Số lượng bằng 0, xóa sản phẩm khỏi hóa đơn" };
                }
                if (Update(billDetail).IsSuccess)
                {
                    //return SuccessResponse(cartDetail, 200, $"{(changeAmount > 0 ? "Tăng" : "Giảm")} số lượng sản phẩm thành công");
                    return new ResponseDto { IsSuccess = true, Code = 200, Message = $"{(changeAmount > 0 ? "Tăng" : "Giảm")} số lượng sản phẩm thành công" };
                }
                else
                {
                    return new ResponseDto { IsSuccess = false, Code = 400, Message = $"{(changeAmount > 0 ? "Tăng" : "Giảm")} số lượng sản phẩm thất bại" }; ;
                }
            }
            catch (Exception e)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = e.Message };
            }
        }

        public bool ThanhToan(HoaDon _hoaDon)
        {
            try
            {
                _context.HoaDons.Update(_hoaDon);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string XoaSanPhamKhoiHoaDon(string maHoaDon, string maSPCT)
        {
            var _getBillByInvoiceCode = _hoaDonRepository.GetBillByInvoiceCode(maHoaDon).Result;
            var sanPhamChiTietDTO = _reposSanPhamChiTiet.GetAllAsync(1).Result.Where(x => x.MaSanPhamChiTiet == maSPCT).FirstOrDefault();
            var hoaDonChiTiet = _hoaDonCTRepository.GetAllAsync().Result.FirstOrDefault(x => x.HoaDonId == _getBillByInvoiceCode.IdHoaDon && x.ChiTietSanPhamId == sanPhamChiTietDTO.Id);
            // xóa sản phẩm khỏi hd chi tiết

            //var hoaDonChiTiet = context.hoaDonChiTiets.FirstOrDefault(c =>  c.IdHoaDon == _getBillByInvoiceCode.IdHoaDon && c.san == idSanPham);
            var soLuongSanPham = hoaDonChiTiet.SoLuong;
            _context.HoaDonChiTiets.Remove(hoaDonChiTiet);

            // cập nhât lại số lượng sản phẩm chi tiết sau khi hủy hóa đơn
            var spct = _reposSanPhamChiTiet.GetAsync().Result.Where(x => x.Id == sanPhamChiTietDTO.Id).FirstOrDefault();

            //var spct = new ChiTietSanPham()
            //{

            spct.SoLuongTon = sanPhamChiTietDTO.SoLuongTon + soLuongSanPham;
            _context.ChiTietSanPhams.Update(spct);
            _context.SaveChanges();
            return soLuongSanPham.ToString();
        }

        public async Task<ResponseDto> HuyHoaDonAsync(string maHoaDon, string lyDoHuy)
        {

            var _getBillByInvoiceCodez = _hoaDonRepository.GetBillByInvoiceCode(maHoaDon).Result;
            var billDetail = _hoaDonCTRepository.GetAllAsync().Result.Where(x => x.HoaDonId == _getBillByInvoiceCodez.IdHoaDon).ToList();

            //var billDetail = JsonConvert.DeserializeObject<List<HoaDonChiTietDto>>(_getBillByInvoiceCodez);
            if (billDetail.Count() == 0)
            {
                var hoadon = _hoaDonRepository.GetAll().Find(x => x.MaHoaDon == maHoaDon);
                hoadon.TrangThaiGiaoHang = 4;
                hoadon.LiDoHuy = lyDoHuy;
                _context.HoaDons.Update(hoadon);
                _context.SaveChanges();

                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Hủy hóa đơn thành công" };
            }
            if (billDetail.Any())
            {
                var hoadon = _hoaDonRepository.GetAll().Find(x => x.MaHoaDon == maHoaDon);
                hoadon.TrangThaiGiaoHang = 4;
                hoadon.LiDoHuy = lyDoHuy;

                _context.HoaDons.Update(hoadon);
                _context.SaveChanges();

                foreach (var item in billDetail)
                {
                    await UpdateSoLuongSanPhamChiTietAynsc(item.ChiTietSanPhamId, -item.SoLuong);
                }
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Hủy hóa đơn thành côngz" };
            }
            return new ResponseDto { IsSuccess = false, Code = 400, Message = "Hủy hóa đơn thất bại" };
        }
        public async Task UpdateSoLuongSanPhamChiTietAynsc(Guid IdSanPhamChiTiet, int soLuong)
        {
            try
            {
                var sanPhamChiTiet = await _context.ChiTietSanPhams.FindAsync(IdSanPhamChiTiet);
                sanPhamChiTiet!.SoLuongTon = sanPhamChiTiet.SoLuongTon - soLuong;
                sanPhamChiTiet!.SoLuongDaBan += soLuong;
                _context.ChiTietSanPhams.Update(sanPhamChiTiet);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

    }
}
