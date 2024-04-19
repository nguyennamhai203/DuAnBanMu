using Shop_Models.Entities;
using System.Data.Entity;
using Shop_Api.Repository.IRepository;
using Shop_Api.AppDbContext;
using Shop_Models.Dto;

namespace Shop_Api.Repository
{
    public class HoaDonRepository : IHoaDonRepository
    {

        public ApplicationDbContext _db;
        public HoaDonRepository(ApplicationDbContext db )
        {
            _db = db;
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

        public async Task<List<HoaDon>> GetAsync(int? status,int page)
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

        public List<HoaDon> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}