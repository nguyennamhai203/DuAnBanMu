using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository
{
    public class HoaDonChiTietRepository : IHoaDonChiTietRepository
    {
        public readonly ApplicationDbContext dbContext;
        public HoaDonChiTietRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResponseDto> CreateAsync(HoaDonChiTiet HDCT)
        {
            try
            {
                await dbContext.Set<HoaDonChiTiet>().AddAsync(HDCT);
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
                dbContext.Set<HoaDonChiTiet>().Remove(entity);
                await dbContext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Xóa thành công" };
            }
            catch (Exception)
            {
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi hệ thống" };
            }
        }

        public async Task<List<HoaDonChiTiet>> GetAllAsync()
        {
            return await dbContext.Set<HoaDonChiTiet>().ToListAsync();
        }

        public async Task<HoaDonChiTiet> GetByIdAsync(Guid id)
        {
            return await dbContext.Set<HoaDonChiTiet>().FindAsync(id);
        }
        public async Task<List<HoaDonChiTiet>> GetAllById(Guid id)
        {
            //var find = dbContext.HoaDonChiTiets.ToList().Where(x => x.HoaDonId == id);
            //var result = find.Select(x=> new HoaDonChiTietDto
            //{

            //});
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, HoaDonChiTiet HDCT)
        {
            try
            {
                dbContext.Set<HoaDonChiTiet>().Update(HDCT);
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
