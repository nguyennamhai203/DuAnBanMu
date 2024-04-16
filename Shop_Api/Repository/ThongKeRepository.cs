using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Repository
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public static int PAGE_SIZE { get; set; } = 2;
        public ThongKeRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<ResponseDto> CreateAsync(ThongKe model)
        {
            var checkId = await _dbContext.ThongKes.AnyAsync(x => x.Id == model.Id);
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
                await _dbContext.ThongKes.AddAsync(model);
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

        public async Task<ResponseDto> UpdateAsync(ThongKe model)
        {
            var thongKe = await _dbContext.ThongKes.FindAsync(model.Id);
            if (thongKe == null)
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
                thongKe.Ngay = model.Ngay;
                thongKe.Thang = model.Thang;
                thongKe.Nam = model.Nam;
                
                _dbContext.ThongKes.Update(thongKe);
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
            var thongKe = await _dbContext.ThongKes.FindAsync(Id);
            if (thongKe == null)
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
                _dbContext.ThongKes.Remove(thongKe);
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

        public async Task<List<ThongKe>> GetAsync()
        {
            var list = await _dbContext.ThongKes.ToListAsync();
            return list;
        }

        public async Task<List<ThongKe>> GetAsync(int? status, int page = 1)
        {
            var list = _dbContext.ThongKes.AsQueryable();
            //if (status.HasValue)
            //{
            //    list = list.Where(x => x.TrangThai == status);
            //}

            #region Paging
            list = list.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            var result = list.Select(tk => new ThongKe
            {
                Ngay = tk.Ngay,
                Thang = tk.Thang,
                Nam = tk.Nam
            });
            return result.ToList();
        }

        public async Task<List<ThongKe>> GetAll()
        {
            var list = await _dbContext.ThongKes.ToListAsync();
            return list;
        }
    }
}
