using Shop_Models.Entities;
//using System.Data.Entity;
using Shop_Api.Repository.IRepository;
using Shop_Api.AppDbContext;
using Shop_Models.Dto;
using Microsoft.EntityFrameworkCore;
using static Shop_Models.Heplers.TrangThai;

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
                        select new HoaDonDto
                        {
                            InvoiceCode = bill.MaHoaDon,
                            PhoneNumber = bill.SoDienThoai,
                            FullName = bill.TenKhachHang,
                            Address = bill.DiaChiGiaoHang,
                            TienShip = (int)bill.TienShip,
                            TrangThaiGiaoHang = bill.TrangThaiGiaoHang,
                            TrangThaiThanhToan = bill.TrangThaiThanhToan,
                            TenKhachHang = bill.TenKhachHang,
                            CreateDate = bill.NgayTao,
                            TienGiam = (int)bill.TienGiam,
                            GiamGia = voucher != null ? voucher.PhanTramGiam : 0,
                            CodeVoucher = voucher != null ? voucher.MaVoucher : null,
                            UserId = bill.NguoiDungId,
                            ThanhTien = (int)bill.TongTien,

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
                    select new HoaDonChiTietDto
                    {
                        SanPhamChiTietId = y.ChiTietSanPhamId,
                        CodeProductDetail = z.MaSanPham,
                        Quantity = y.SoLuong,
                        Price = (float)y.GiaBan
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

                hoadon.NgayNhan = NgayNhan ?? hoadon.NgayNhan;
                hoadon.NgayShip = NgayShip ?? hoadon.NgayShip;
                hoadon.NgayThanhToan = NgayThanhToan ?? hoadon.NgayThanhToan;

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
    }
}
