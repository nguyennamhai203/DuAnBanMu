using Shop_Models.Entities;
//using System.Data.Entity;
using Shop_Api.Repository.IRepository;
using Shop_Api.AppDbContext;
using Shop_Models.Dto;
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
                    select new HoaDonChiTietDto
                    {
                        HoaDonId = x.Id,
                        Id = y.Guid,
                        SanPhamChiTietId = y.ChiTietSanPhamId,
                        CodeProductDetail = z.MaSanPham,
                        Quantity = y.SoLuong,
                        Price = (float)z.GiaThucTe,
                        PriceBan = (float)y.GiaBan,

                    }).AsEnumerable();
                return billDetails;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
