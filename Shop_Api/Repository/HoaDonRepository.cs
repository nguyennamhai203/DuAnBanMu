using Shop_Models.Entities;
//using System.Data.Entity;
using Shop_Api.Repository.IRepository;
using Shop_Api.AppDbContext;
using Shop_Models.Dto;
using static Shop_Models.Heplers.TrangThai;
using Microsoft.EntityFrameworkCore;
using Shop_Api.Migrations;

namespace Shop_Api.Repository
{
    public class HoaDonRepository : IHoaDonRepository
    {

        public ApplicationDbContext _db;
        public HoaDonRepository(ApplicationDbContext db)
        {
            _db = db;
        }



        List<HoaDon> IHoaDonRepository.GetAll()
        {
            return _db.HoaDons.ToList();
        }

        public async Task<ResponseDto> CreateHD(HoaDon a)
        {
            try
            {
                await _db.HoaDons.AddAsync(a);
                await _db.SaveChangesAsync();
                return new ResponseDto
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thêm thành công"
                };

            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "Thêm thất bại"
                };
            }
        }

        public async Task<ResponseDto> UpdateHD(Guid id, HoaDon a)
        {
            var kq = await _db.HoaDons.FindAsync(id);
            kq.NgayTao = a.NgayTao;
            kq.NgayThanhToan = a.NgayThanhToan;
            kq.NgayGiaoDuKien = a.NgayGiaoDuKien;
            kq.NgayNhan = a.NgayNhan;
            kq.NgayShip = a.NgayShip;
            kq.MoTa = a.MoTa;
            kq.LiDoHuy = a.LiDoHuy;
            kq.TienGiam = a.TienGiam;
            kq.TienShip = a.TienShip;

            await _db.SaveChangesAsync();


            return new ResponseDto
            {

                Code = 200,
                Message = "cap nhat thanh cong"
            };
        }

        public async Task<HoaDon> GetById(Guid id)
        {
            return await _db.HoaDons.FindAsync(id);
        }

        public async Task<List<HoaDon>> GetAsync(int? status, int page = 1)
        {
            var list = _db.HoaDons.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThaiThanhToan == status && x.TrangThaiGiaoHang == status);
            }
            var result = list.Select(x => new HoaDon
            {
                Id = x.Id,
                MaHoaDon = x.MaHoaDon,
                NgayTao = x.NgayTao,
                NgayThanhToan = x.NgayThanhToan,
                NgayShip = x.NgayShip,
                NgayNhan = x.NgayNhan,
                MoTa = x.MoTa,
                TienGiam = x.TienGiam,
                TienShip = x.TienShip,
                TongTien = x.TongTien,
                TrangThaiThanhToan = x.TrangThaiThanhToan,
                TrangThaiGiaoHang = x.TrangThaiGiaoHang,
                NgayGiaoDuKien = x.NgayGiaoDuKien,
                LiDoHuy = x.LiDoHuy
            });
            return result.ToList();
        }
        public async Task<HoaDonDto> GetBillByInvoiceCode(string invoiceCode)
        {
            var query = from bill in _db.HoaDons
                        where bill.MaHoaDon == invoiceCode
                        join v in _db.Vouchers on bill.VoucherId equals v.Guid into voucherGroup
                        from voucher in voucherGroup.DefaultIfEmpty()
                        join ttct in _db.PhuongThucTTChiTiets on bill.Id equals ttct.HoaDonId into ttctGroup
                        from thanhtoanchitiet in ttctGroup.DefaultIfEmpty()
                        join pttt in _db.PhuongThucThanhToans on thanhtoanchitiet.PTTToanId equals pttt.Id into ptttGroup
                        from phuongthucthanhtoan in ptttGroup.DefaultIfEmpty()
                        select new HoaDonDto
                        {
                            IdHoaDon = bill.Id,
                            InvoiceCode = bill.MaHoaDon,
                            PhoneNumber = bill.SoDienThoai,
                            FullName = bill.TenKhachHang,
                            Address = bill.DiaChiGiaoHang,
                            TienShip = (int)bill.TienShip,
                            TrangThaiGiaoHang = bill.TrangThaiGiaoHang,
                            TrangThaiThanhToan = bill.TrangThaiThanhToan,
                            TenKhachHang = bill.TenKhachHang,
                            CreateDate = bill.NgayTao,
                            NgayThanhToan = bill.NgayThanhToan,
                            TienGiam = (int)bill.TienGiam,
                            GiamGia = voucher != null ? voucher.PhanTramGiam : 0,
                            CodeVoucher = voucher != null ? voucher.MaVoucher : null,
                            UserId = bill.NguoiDungId,
                            ThanhTien = (int)bill.TongTien,
                            Phuongthucthanhtoan = phuongthucthanhtoan.TenMaPTThanhToan

                        };

            return await query/*.AsNoTracking()*/.FirstOrDefaultAsync();

        }



        public async Task<IEnumerable<HoaDonChiTietDto>> GetBillDetailByInvoiceCode(string invoiceCode)
        {
            try
            {
                var billDetails = (
                    from x in _db.HoaDons.AsQueryable().AsNoTracking().Where(a => a.MaHoaDon == invoiceCode)
                    join y in _db.HoaDonChiTiets.AsQueryable().AsNoTracking() on x.Id equals y.HoaDonId
                    join z in _db.ChiTietSanPhams.AsQueryable().AsNoTracking() on y.ChiTietSanPhamId equals z.Id
                    join m in _db.MauSacs.AsQueryable().AsNoTracking() on z.MauSacId equals m.Guid
                    select new HoaDonChiTietDto
                    {
                        HoaDonId = x.Id,
                        Id = y.Guid,
                        SanPhamChiTietId = y.ChiTietSanPhamId,
                        CodeProductDetail = z.MaSanPham,
                        Quantity = y.SoLuong,
                        Price = (float)z.GiaThucTe, // giá gốc chưa giảm giá
                        PriceBan = (float)y.GiaBan,
                        MauSac = m.TenMauSac
                    }).AsEnumerable();
                return billDetails;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<ResponseDto> CancelOrder(Guid id, string reason)
        {
            try
            {
                var hoaDon = await _db.HoaDons.FindAsync(id);
                if (hoaDon == null)
                {
                    return new ResponseDto { Code = 400, Message = "Không tìm thấy hóa đơn." };
                }
                // Kiểm tra nếu trạng thái hiện tại là Đang giao hàng thì không thể hủy
                if ((TrangThaiGiaoHang)hoaDon.TrangThaiGiaoHang == TrangThaiGiaoHang.DangGiaoHang)
            
                {
                    return new ResponseDto { Code = 500, Message = "Không thể hủy đơn hàng khi đang giao hàng ." };
                } else if ((TrangThaiGiaoHang) hoaDon.TrangThaiGiaoHang == TrangThaiGiaoHang.DaGiaoHang)
                {
                    return new ResponseDto { Code = 500, Message = "Không thể hủy đơn hàng khi đã giao hàng." };
                }
                // Lấy ra chi tiết hóa đơn để hoàn lại số lượng
                var chiTietHoaDon = await _db.HoaDonChiTiets
                    .Include(ct => ct.ChiTietSanPham)
                    .Where(ct => ct.HoaDonId == hoaDon.Id)
                    .ToListAsync();
                if (chiTietHoaDon != null && chiTietHoaDon.Any())
                {
                    foreach (var chiTiet in chiTietHoaDon)
                    {
                        var sanPhamChiTiet = chiTiet.ChiTietSanPham;
                        if (sanPhamChiTiet != null)
                        {
                            // Hoàn lại số lượng tồn
                            sanPhamChiTiet.SoLuongTon += chiTiet.SoLuong;
                        }
                    }
                }
                hoaDon.NgayThanhToan = DateTime.Now;
                hoaDon.TrangThaiGiaoHang = (int)TrangThaiGiaoHang.DaHuy;
                hoaDon.LiDoHuy = reason;

                await _db.SaveChangesAsync();

                return new ResponseDto { Code = 200, Message = "Hóa đơn đã được hủy thành công." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Code = 500, Message = "Đã xảy ra lỗi khi hủy đơn hàng: " + ex.Message };
            }
        }

        public async Task<bool> CheckCustomerExistence(Guid nguoidungId)
        {
            var customer = await _db.NguoiDungs.FindAsync(nguoidungId);
            return customer != null;
        }

        public List<HoaDon> GetHoaDonUpdate()
        {
            // Lấy danh sách các hoá đơn cần cập nhật (ví dụ: trạng thái giao hàng)
            return _db.HoaDons
                            .Where(hd => hd.TrangThaiGiaoHang != 0) // Thay X bằng trạng thái cụ thể cần cập nhật
                            .ToList();
        }

        public List<HoaDon> GetAllHoaDonCho()
        {
            // Lấy danh sách tất cả các hoá đơn đang chờ xử lý
            return _db.HoaDons
                            .Where(hd => hd.TrangThaiThanhToan == 0) // Thay X bằng trạng thái cụ thể cho hoá đơn chờ xử lý
                            .Select(hd => new HoaDon
                            {
                                MaHoaDon = hd.MaHoaDon,
                                NgayTao = hd.NgayTao,
                                NgayThanhToan = hd.NgayThanhToan,
                                NgayShip = hd.NgayShip,
                                NgayNhan = hd.NgayNhan,
                                MoTa = hd.MoTa,
                                TienGiam = hd.TienGiam,
                                TienShip = hd.TienShip,
                                TongTien = hd.TongTien,
                                TrangThaiThanhToan = hd.TrangThaiThanhToan,
                                TrangThaiGiaoHang = hd.TrangThaiGiaoHang,
                                NgayGiaoDuKien = hd.NgayGiaoDuKien,
                                LiDoHuy = hd.LiDoHuy,


                                // Thêm các thuộc tính khác của HoaDonChoDTO cần thiết
                            })
                            .ToList();
        }

        public async Task<ResponseDto> UpdateNgayHoaDonOnline(Guid idHoaDon, DateTime? NgayThanhToan, DateTime? NgayNhan, DateTime? NgayShip)
        {
            try
            {
                var hoadon = await _db.HoaDons.FindAsync(idHoaDon);
                if (hoadon == null)
                    return new ResponseDto { IsSuccess = false, Code = 404, Message = "Không tìm thấy hóa đơn." };

                // Kiểm tra trạng thái của hóa đơn
                if (hoadon.TrangThaiThanhToan == (int)TrangThaiThanhToan.Dathanhtoan) // Nếu đã thanh toán
                {
                    // Chỉ cập nhật ngày nhận và ngày ship
                    hoadon.NgayNhan = NgayNhan ?? hoadon.NgayNhan;
                    hoadon.NgayShip = NgayShip ?? hoadon.NgayShip;
                }
                else // Nếu chưa thanh toán
                {
                    // Cập nhật ngày thanh toán, ngày nhận và ngày ship
                    hoadon.NgayThanhToan = NgayThanhToan ?? hoadon.NgayThanhToan;
                    hoadon.NgayNhan = NgayNhan ?? hoadon.NgayNhan;
                    hoadon.NgayShip = NgayShip ?? hoadon.NgayShip;
                }

                await _db.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Cập nhật ngày hóa đơn thành công." };
            }
            catch (Exception ex)
            {
                // Xử lý các trường hợp ngoại lệ nếu cần
                return new ResponseDto { IsSuccess = false, Code = 500, Message = $"Lỗi khi cập nhật ngày hóa đơn: {ex.Message}" };
            }
        }

        public async Task<ResponseDto> UpdateThanhToan(Guid idHoaDon, int TrangThaiThanhToan)
        {
            try
            {
                var hoaDon = await _db.HoaDons.FindAsync(idHoaDon);

                if (hoaDon == null)
                {
                    return new ResponseDto { IsSuccess = false, Code = 400, Message = "Hóa đơn không tồn tại" };
                }

                hoaDon.TrangThaiThanhToan = TrangThaiThanhToan;
                _db.Update(hoaDon);
                await _db.SaveChangesAsync();

                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Cập nhật trạng thái thanh toán thành công" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = $"Lỗi: {ex.Message}" };
            }
        }
        public async Task<List<HoaDon>> GetByHoaDonStatus(int HoaDonStatus)
        {
            var hoaDons = await _db.HoaDons
                .Where(hd => hd.TrangThaiGiaoHang == HoaDonStatus)
                .ToListAsync();

            return hoaDons;
        }
        public async Task<ResponseDto> UpdateHoaDonStatus(Guid id, int HoaDonStatus)
        {
            var hoaDon = await _db.HoaDons.FirstOrDefaultAsync(hd => hd.Id == id);
            if (hoaDon != null)
            {
                var chiTietHoaDon = await _db.HoaDonChiTiets
                    .Include(ct => ct.ChiTietSanPham)
                    .Where(ct => ct.HoaDonId == hoaDon.Id)
                    .ToListAsync();

                if ((TrangThaiGiaoHang)HoaDonStatus == TrangThaiGiaoHang.DangGiaoHang)
                {
                    if (hoaDon != null)
                    {
                        foreach (var chiTiet in chiTietHoaDon) // Sử dụng biến chiTietHoaDon đã được tải từ bảng HoaDonChiTiets
                        {
                            var sanPhamChiTiet = chiTiet.ChiTietSanPham;
                            if (sanPhamChiTiet != null)
                            {
                                // Kiểm tra số lượng tồn
                                if (sanPhamChiTiet.SoLuongTon >= chiTiet.SoLuong)
                                {
                                    // Trừ số lượng đã bán từ số lượng tồn
                                    sanPhamChiTiet.SoLuongTon -= chiTiet.SoLuong;
                                }
                                else
                                {
                                    // Nếu số lượng tồn không đủ, không cập nhật và trả về thông báo lỗi
                                    return new ResponseDto { IsSuccess = false, Message = "Số lượng tồn không đủ", Code = 400 };
                                }
                            }
                        }
                    }
                }
                // Hủy đơn hàng
                else if (HoaDonStatus == (int)TrangThaiGiaoHang.DaGiaoHang)
                {
                    //if (hoaDon.TrangThaiGiaoHang == (int)TrangThaiGiaoHang.DangGiaoHang)
                    //{
                    //    // Không thể hủy đơn hàng nếu đang giao hàng
                    //    return new ResponseDto { IsSuccess = false, Message = "Không thể hủy đơn hàng khi đang giao hàng", Code = 400 };
                    //}
                    if (hoaDon != null)
                    {
                        foreach (var chiTiet in chiTietHoaDon)
                        {
                            var sanPhamChiTiet = chiTiet.ChiTietSanPham;
                            if (sanPhamChiTiet != null)
                            {
                                // Tăng số lượng đã bán của sản phẩm
                                sanPhamChiTiet.SoLuongDaBan += chiTiet.SoLuong;
                                // Cập nhật thông tin sản phẩm sau khi tăng số lượng đã bán
                                _db.ChiTietSanPhams.Update(sanPhamChiTiet);
                            }
                        }

                        await _db.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu
                    }
                }
            }
            if (hoaDon == null)
                return new ResponseDto { Message = "Hóa đơn không tồn tại", Code = 404 };

            if ((TrangThaiThanhToan)hoaDon.TrangThaiThanhToan == TrangThaiThanhToan.Chuathanhtoan &&
                (TrangThaiGiaoHang)HoaDonStatus == TrangThaiGiaoHang.DaGiaoHang)
            {
                // Chỉ cập nhật ngày thanh toán nếu trạng thái hiện tại là Chưa thanh toán
                if ((TrangThaiThanhToan)hoaDon.TrangThaiThanhToan == TrangThaiThanhToan.Chuathanhtoan)
                {
                    hoaDon.NgayThanhToan = DateTime.Now;
                    // Cập nhật ngày ship và ngày nhận hiện tại
                    hoaDon.NgayShip = DateTime.Now;
                    hoaDon.NgayNhan = DateTime.Now;
                }
                hoaDon.TrangThaiThanhToan = (int)TrangThaiThanhToan.Dathanhtoan; // Chuyển trạng thái thanh toán thành "Đã thanh toán"
            }
            else if ((TrangThaiThanhToan)hoaDon.TrangThaiThanhToan == TrangThaiThanhToan.Dathanhtoan &&
                     (TrangThaiGiaoHang)HoaDonStatus == TrangThaiGiaoHang.DaGiaoHang)
            {
                // Nếu hóa đơn đã thanh toán và được chuyển thành đã giao hàng,
                // chỉ cập nhật ngày nhận và ngày ship
                hoaDon.NgayShip = DateTime.Now;
                hoaDon.NgayNhan = DateTime.Now;
            }
            hoaDon.TrangThaiGiaoHang = HoaDonStatus;
            await _db.SaveChangesAsync();
            return new ResponseDto { IsSuccess = true, Message = "Cập nhật trạng thái hóa đơn thành công" };

        }


        public async Task<HoaDon> GetHoaDonByMaHoaDonAsync(string maHoaDon)
        {
            return await _db.HoaDons.FirstOrDefaultAsync(h => h.MaHoaDon == maHoaDon);
        }
        public async Task<List<HoaDon>> GetCustomerPurchaseHistory(string customerId)
        {
            // Giả định rằng trường NguoiDungId trong HoaDon là ID của khách hàng
            // và bạn muốn lấy tất cả các hóa đơn của khách hàng đó.
            var user = _db.NguoiDungs.FirstOrDefault(x => x.UserName == customerId).Id;
            return _db.HoaDons.Where(hd => hd.NguoiDungId == user).ToList();
        }
        //public byte[] GeneratePDF()
        //{
        //    // Tạo một tài liệu mới
        //    Document document = new Document();
        //    MemoryStream memoryStream = new MemoryStream();
        //    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

        //    document.Open();

        //    // Thêm nội dung vào tài liệu
        //    Paragraph paragraph = new Paragraph("Đây là nội dung của PDF.");
        //    document.Add(paragraph);

        //    document.Close();

        //    // Chuyển tài liệu đã tạo thành một mảng byte để trả về
        //    byte[] bytes = memoryStream.ToArray();
        //    memoryStream.Close();

        //    return bytes;
        //}
    }
}
