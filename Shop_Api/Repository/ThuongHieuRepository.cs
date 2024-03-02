using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository
{
    public class ThuongHieuRepository : IThuongHieuRepository
    {
        public readonly ApplicationDbContext dbContext;
        public ThuongHieuRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResponseDto> CreateAsync(ThuongHieu TH)
        {
            try
            {
                await dbContext.Set<ThuongHieu>().AddAsync(TH);
                await dbContext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Thành công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi hệ thống" };
            }
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return new ResponseDto { IsSuccess = false, Code = 404, Message = "Không tìm thấy bản ghi" };
            }

            try
            {
                dbContext.Set<ThuongHieu>().Remove(entity);
                await dbContext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Xóa thành công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi hệ thống" };
            }
        }

        public async Task<List<ThuongHieu>> GetAllAsync()
        {
            return await dbContext.Set<ThuongHieu>().ToListAsync();
        }

        public async Task<ThuongHieu> GetByIdAsync(Guid id)
        {
            return await dbContext.Set<ThuongHieu>().FindAsync(id);
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, ThuongHieu TH)
        {
            try
            {
                dbContext.Set<ThuongHieu>().Update(TH);
                await dbContext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Thành công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi hệ thống" };
            }
        }
    }
}
