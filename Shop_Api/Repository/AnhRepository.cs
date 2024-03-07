using Shop_Models.Entities;
using System.Data.Entity;
using Shop_Api.Repository.IRepository;
using Shop_Api.AppDbContext;
using Shop_Models.Dto;

namespace Shop_Api.Repository
{
    public class AnhRepository : IAnhRepository
    {

        public ApplicationDbContext _db;
        public AnhRepository(ApplicationDbContext db )
        {
            _db = db;
        }

        public async Task<ResponseDto> CreateAnh(Anh anh)
        {
            try
            {
                await _db.Anhs.AddAsync(anh);
                await _db.SaveChangesAsync();
                return new ResponseDto
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thêm thành công"
                };

            }
            catch(Exception)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = "Thêm thất bại"
                };
            }
        }

        public async Task<ResponseDto> DeleteAnh(Guid id)
        {
            var kq = await _db.Anhs.FindAsync(id);

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

        public List<Anh> GetAll()
        {
            return _db.Anhs.ToList();   
        }

       

        public async Task<ResponseDto> UpdateAnh(Guid id, Anh anh)
        {
            var kq = await _db.Anhs.FindAsync(id);
            kq.MaAnh = anh.MaAnh;
            kq.URL = anh.URL;
           await _db.SaveChangesAsync();
            return new ResponseDto
            {

                Code = 200,
                Message = "cap nhat thanh cong"
            };
        }

       public async Task<Anh> GetAnhById(Guid id)
        {
           return  await _db.Anhs.FindAsync(id);
        }
    }
}
