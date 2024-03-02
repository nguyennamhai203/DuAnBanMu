using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository
{
    public class ChiTietSanPhamRepository : IChiTietSanPhamRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public static int PAGE_SIZE { get; set; } = 2;
        public ChiTietSanPhamRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<ResponseDto> CreateAsync(ChiTietSanPham model)
        {
            var checkMa = await _dbContext.ChiTietSanPhams.AnyAsync(x => x.MaSanPham == model.MaSanPham);
            if (model == null || checkMa == true)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Trùng Mã",
                };
            }
            try
            {
                await _dbContext.ChiTietSanPhams.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<ResponseDto> UpdateAsync(ChiTietSanPham model)
        {
            var chiTietSanPham = await _dbContext.ChiTietSanPhams.FindAsync(model.Id);
            if (chiTietSanPham == null)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 404,
                    Message = "Không Tim Thấy Bản Ghi",
                };
            }
            try
            {
                chiTietSanPham.MaSanPham = model.MaSanPham;
                chiTietSanPham.GiaBan = model.GiaBan;
                chiTietSanPham.GiaNhap = model.GiaNhap;
                chiTietSanPham.GiaThucTe = model.GiaThucTe;
                chiTietSanPham.SoLuongTon = model.SoLuongTon;
                chiTietSanPham.SoLuongDaBan = model.SoLuongDaBan;

                chiTietSanPham.SanPhamId = model.SanPhamId;
                chiTietSanPham.LoaiId = model.LoaiId;
                chiTietSanPham.ThuongHieuId = model.ThuongHieuId;
                chiTietSanPham.XuatXuId = model.XuatXuId;
                chiTietSanPham.MauSacId = model.MauSacId;
                chiTietSanPham.ChatLieuId = model.ChatLieuId;
                _dbContext.ChiTietSanPhams.Update(chiTietSanPham);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<ResponseDto> DeleteAsync(Guid Id)
        {
            var chiTietSanPham = await _dbContext.ChiTietSanPhams.FindAsync(Id);
            if (chiTietSanPham == null)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 404,
                    Message = "Không Tim Thấy Bản Ghi",
                };
            }
            try
            {
                //chiTietSanPham.TrangThai = 2;
                _dbContext.ChiTietSanPhams.Remove(chiTietSanPham);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Xóa Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<List<ChiTietSanPham>> GetAsync()
        {
            var list = await _dbContext.ChiTietSanPhams.ToListAsync();
            return list;
        }

        public async Task<List<ChiTietSanPham>> GetAsync(int? status, int page = 1)
        {
            var list = _dbContext.ChiTietSanPhams.AsQueryable();
            

            #region Paging
            list = list.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            var result = list.Select(sp => new ChiTietSanPham
            {
                
                MaSanPham = sp.MaSanPham,
                GiaBan = sp.GiaBan,
                GiaNhap = sp.GiaNhap,
                GiaThucTe = sp.GiaThucTe,
                SoLuongTon = sp.SoLuongTon,
                SoLuongDaBan = sp.SoLuongDaBan,

                SanPhamId = sp.SanPhamId,
                LoaiId = sp.LoaiId,
                ThuongHieuId = sp.ThuongHieuId,
                XuatXuId = sp.XuatXuId,
                MauSacId = sp.MauSacId,
                ChatLieuId = sp.ChatLieuId

            });
            return result.ToList();
        }
    }
}
