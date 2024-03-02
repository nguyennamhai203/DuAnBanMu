using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository
{
    public class LoaiRepository : ILoaiRepository
    {
        public readonly ApplicationDbContext dbcontext;
        public LoaiRepository(ApplicationDbContext context)
        {
            this.dbcontext = context;
        }

        public async Task<ResponseDto> CreateAsync(Loai loai)
        {
            
            try
            {
                dbcontext.Set<Loai>().Add(loai);
                await dbcontext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Thành Công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi Hệ Thống" };
            }
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return new ResponseDto { IsSuccess = false, Code = 404, Message = "Không Tìm Thấy Bản Ghi" };
            }

            try
            {
                dbcontext.Set<Loai>().Remove(entity);
                await dbcontext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Xóa Thành Công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi Hệ Thống" };
            }
        }

        public async Task<List<Loai>> GetAllAsync()
        {
            return await dbcontext.Set<Loai>().ToListAsync();
        }

        public async Task<Loai> GetByIdAsync(Guid id)
        {
            return await dbcontext.Set<Loai>().FindAsync(id);
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, Loai loai)
        {
            try
            {
                dbcontext.Set<Loai>().Update(loai);
                await dbcontext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Thành Công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi Hệ Thống" };
            }
        }
    }
}
