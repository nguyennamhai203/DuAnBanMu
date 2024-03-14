using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository
{
    public class ChiTietKhuyenMaiRepository : IChiTietKhuyenMaiRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public static int PAGE_SIZE { get; set; } = 2;
        public ChiTietKhuyenMaiRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<ResponseDto> CreateAsync(ChiTietKhuyenMai model)
        {
            var checkId = await _dbContext.ChiTietKhuyenMais.AnyAsync(x => x.Id == model.Id);
            if (model == null || checkId == true)
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
                await _dbContext.ChiTietKhuyenMais.AddAsync(model);
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

        public async Task<ResponseDto> UpdateAsync(ChiTietKhuyenMai model)
        {
            var chiTietKhuyenMai = await _dbContext.ChiTietKhuyenMais.FindAsync(model.Id);
            if (chiTietKhuyenMai == null)
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
                chiTietKhuyenMai.Mota = model.Mota;
                chiTietKhuyenMai.TrangThai = model.TrangThai;
                _dbContext.ChiTietKhuyenMais.Update(chiTietKhuyenMai);
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

        public async Task<ResponseDto> DeleteAsync(Guid Id)
        {
            var chiTietKhuyenMai = await _dbContext.ChiTietKhuyenMais.FindAsync(Id);
            if (chiTietKhuyenMai == null)
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
                //chiTietKhuyenMai.TrangThai = 2;
                _dbContext.ChiTietKhuyenMais.Remove(chiTietKhuyenMai);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Xóa Thành Công",
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

        public async Task<List<ChiTietKhuyenMai>> GetAsync()
        {
            var list = await _dbContext.ChiTietKhuyenMais.ToListAsync();
            return list;
        }

        public async Task<List<ChiTietKhuyenMai>> GetAsync(int? status, int page = 1)
        {
            var list = _dbContext.ChiTietKhuyenMais.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }

            #region Paging
            list = list.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            var result = list.Select(sp => new ChiTietKhuyenMai
            {
                Mota = sp.Mota,
                TrangThai = sp.TrangThai
            });
            return result.ToList();
        }
    }
}
