using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;


namespace Shop_Api.Repository
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public static int PAGE_SIZE { get; set; } = 2;
        public SanPhamRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ResponseDto> CreateAsync(SanPham model)
        {
            var checkMa = await _dbContext.SanPhams.AnyAsync(x => x.MaSanPham == model.MaSanPham);
            if (model == null || checkMa == true)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Trùng Mã",
                };
            }
            try
            {
                await _dbContext.SanPhams.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<List<SanPham>> GetAsync()
        {
            var list = await _dbContext.SanPhams.ToListAsync();
            return list;
        }

        public async Task<List<SanPham>> GetAsync(int? status, int page = 1)
        {
            var list = _dbContext.SanPhams.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }

            #region Paging
            list = list.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            var result = list.Select(sp => new SanPham
            {
                MaSanPham = sp.MaSanPham,
                TenSanPham = sp.TenSanPham,
                TrangThai = sp.TrangThai
            });
            return result.ToList();
        }

        public async Task<ResponseDto> UpdateAsync(SanPham model)
        {
            var sanPham = await _dbContext.SanPhams.FindAsync(model.IdSanPham);
            if (sanPham == null)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 404,
                    Message = "Không Tim Thấy Bản Ghi",
                };
            }
            try
            {
                sanPham.MaSanPham = model.MaSanPham;
                sanPham.TenSanPham = model.TenSanPham;
                sanPham.TrangThai = model.TrangThai;
                _dbContext.SanPhams.Update(sanPham);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
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
                _dbContext.SanPhams.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Code = 200, Message = "Xóa thành công" };
            }
            catch (DbUpdateException dbEx)
            {
                // Log lỗi cơ sở dữ liệu
                Console.WriteLine($"Database error: {dbEx.Message}");
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi cơ sở dữ liệu" };
            }
            catch (Exception ex)
            {
                // Log lỗi hệ thống
                Console.WriteLine($"System error: {ex.Message}");
                return new ResponseDto { IsSuccess = false, Code = 500, Message = "Lỗi hệ thống" };
            }
        }

        public async Task<SanPham> GetByIdAsync(Guid id)
        {
            return await _dbContext.SanPhams.FindAsync(id);
        }
    }
}
