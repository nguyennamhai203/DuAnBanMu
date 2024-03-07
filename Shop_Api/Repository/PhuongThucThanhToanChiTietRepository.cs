using Shop_Models.Entities;
using System.Data.Entity;
using Shop_Api.Repository.IRepository;
using Shop_Api.AppDbContext;
using Shop_Models.Dto;

namespace Shop_Api.Repository
{
    public class PhuongThucThanhToanChiTietRepository : IPhuongThucThanhToanChiTietRepository
    {

        public ApplicationDbContext _db;
        public PhuongThucThanhToanChiTietRepository(ApplicationDbContext db )
        {
            _db = db;
        }

        public async Task<ResponseDto> Create(PhuongThucTTChiTiet a)
        {

            try
            {
                await _db.PhuongThucTTChiTiets.AddAsync(a);
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


        public async Task<ResponseDto> Delete(Guid id)
        {
            var kq = await _db.PhuongThucTTChiTiets.FindAsync(id);

            try
            {
                _db.Remove(kq);
                await _db.SaveChangesAsync();
                return new ResponseDto
                {

                    Code = 200,
                    Message = "Xóa thành công"
                };

            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = "Xóa thất bại"
                };
            }
        }

        public List<PhuongThucTTChiTiet> GetAll()
        {
            return _db.PhuongThucTTChiTiets.ToList();

        }

        public async Task<PhuongThucTTChiTiet> GetById(Guid id)
        {
            return await _db.PhuongThucTTChiTiets.FindAsync(id);
        }

        public async Task<ResponseDto> Update(Guid id, PhuongThucTTChiTiet anh)
        {
            var kq = await _db.PhuongThucTTChiTiets.FindAsync(id);
            kq.HoaDonId = anh.HoaDonId;
            kq.PTTToanId = anh.PTTToanId;
            kq.SoTien = anh.SoTien;
            kq.TrangThai = anh.TrangThai;
            await _db.SaveChangesAsync();
            return new ResponseDto
            {

                Code = 200,
                Message = "cap nhat thanh cong"
            };
        }
    }
}
