using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository
{
    public class PhuongThucThanhToanRepository : IPhuongThucThanhToanRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PhuongThucThanhToanRepository(ApplicationDbContext context)
        {
            this._dbContext = context;
        }
        public async Task<ResponseDto> CreateAsync(PhuongThucThanhToan PTTT)
        {
            try
            {
                await _dbContext.Set<PhuongThucThanhToan>().AddAsync(PTTT);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Thành công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi hệ thống" };
            }
        }

        public async Task<List<PhuongThucThanhToan>> GetAllAsync()
        {
            return await _dbContext.Set<PhuongThucThanhToan>().ToListAsync();
        }

        public async Task<PhuongThucThanhToan> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<PhuongThucThanhToan>().FindAsync(id);
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, PhuongThucThanhToan entity)
        {
            try
            {
                _dbContext.Set<PhuongThucThanhToan>().Update(entity);
                await _dbContext.SaveChangesAsync();
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
                _dbContext.Set<PhuongThucThanhToan>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Xóa thành công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi hệ thống" };
            }
        }
    }
    }
