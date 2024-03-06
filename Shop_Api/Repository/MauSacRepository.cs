using Shop_Models.Entities;
using System.Data.Entity;
using Shop_Api.Repository.IRepository;
using Shop_Api.AppDbContext;
using Shop_Models.Dto;

namespace Shop_Api.Repository
{
    public class MauSacRepository : IMauSacRepository
    {

        public ApplicationDbContext _db;
        public MauSacRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseDto> CreateMS(MauSac a)
        {

            try
            {
                await _db.MauSacs.AddAsync(a);
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

        public async Task<ResponseDto> DeleteMS(Guid id)
        {
            var kq = await _db.MauSacs.FindAsync(id);

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

        public List<MauSac> GetAll()
        {
            return _db.MauSacs.ToList();
        }

        public async Task<MauSac> GetById(Guid id)
        {
            return await _db.MauSacs.FindAsync(id);
        }

        public async Task<ResponseDto> UpdateMS(Guid id, MauSac anh)
        {
            var kq = await _db.MauSacs.FindAsync(id);
            kq.MaMauSac = anh.MaMauSac;
            kq.TenMauSac = anh.TenMauSac;
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
