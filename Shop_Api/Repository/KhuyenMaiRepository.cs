using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class KhuyenMaiRepository : IKhuyenMaiRepository
    {
        private readonly ApplicationDbContext _context;
        public static int PAGE_SIZE { get; set; } = 2;


        public KhuyenMaiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> CreateAsync(Khuyenmai obj)
        {
            var check = await _context.KhuyenMais.AnyAsync(x => x.MaKhuyenMai == obj.MaKhuyenMai);
            if (check == true)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Mã Khuyễn Mãi Đã Tồn Tại",

                };
            }
            try
            {
                if (obj.NgayBatDau > obj.NgayKetThuc)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Code = 400,
                        Message = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc",

                    };
                }
                else if (obj.MaKhuyenMai==string.Empty||obj.TenKhuyenMai==string.Empty)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Code = 400,
                        Message = "Thiếu Mã hoặc Tên",

                    };
                }
                else
                {
                    obj.Id = Guid.NewGuid();
                    await _context.KhuyenMais.AddAsync(obj);
                    await _context.SaveChangesAsync();
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Code = 200,
                        Message = "Thêm thành công khuyến mãi",

                    };
                }
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",

                };
            }
        }

        public async Task<ResponseDto> DeleteAsync(Guid Id)
        {
            var _findKM = await _context.KhuyenMais.FindAsync(Id);
            try
            {
                if (_findKM == null)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Code = 405,
                        Message = "Không tìm thấy khuyến mãi",

                    };
                }
                else
                {
                    _context.KhuyenMais.Remove(_findKM);
                    await _context.SaveChangesAsync();
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Code = 200,
                        Message = "Xóa Thành Công Khuyến Mãi",

                    };
                }
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",

                };
            }
        }

        public async Task<ResponseDto> GetAsync(int? status, int page = 1)
        {
            var list = _context.KhuyenMais.AsQueryable();
            var listAll = _context.KhuyenMais.AsQueryable();
            int totalItems = list.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }

            #region Paging
            list = list.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);


            #endregion

            var result = list.Select(sp => new Khuyenmai
            {
                MaKhuyenMai = sp.MaKhuyenMai,
                NgayBatDau = sp.NgayBatDau,
                NgayKetThuc = sp.NgayKetThuc,
                LoaiHinhKhuyenMai = sp.LoaiHinhKhuyenMai,
                MucGiam = sp.MucGiam,
                TrangThai = sp.TrangThai,
            });
            return new ResponseDto
            {
                Result = result.ToList(),
                IsSuccess = true,
                Code = 200,
                Message = $"trang số {page}/ tổng số trang: {totalPages} ,\n số bản ghi: {result.Count()}/ tổng số bản ghi: {listAll.Count()}",
            };
        }

        public async Task<ResponseDto> GetByIdAsync(Guid Id)
        {
            var _findKM = await _context.KhuyenMais.FindAsync(Id);
            try
            {
                if (_findKM == null)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Code = 405,
                        Message = "Không tìm thấy khuyến mãi",

                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        Result = _findKM,
                        IsSuccess = true,
                        Code = 200,
                        Message = "Đã tìm thấy khuyến mãi",

                    };
                }
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",

                };
            }
        }

        public async Task<ResponseDto> UpdateAsync(Khuyenmai obj)
        {
            var _update = await _context.KhuyenMais.FindAsync(obj.Id);
            if (_update == null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Mã Khuyễn Mãi Không Tồn Tại",

                };
            }
            try
            {
                if (obj.NgayBatDau > obj.NgayKetThuc)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Code = 400,
                        Message = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc",

                    };
                }
                else
                {
                    _update.MaKhuyenMai = obj.MaKhuyenMai;
                    _update.TenKhuyenMai = obj.TenKhuyenMai;
                    _update.NgayBatDau = obj.NgayBatDau;
                    _update.NgayKetThuc = obj.NgayKetThuc;
                    _update.MucGiam = obj.MucGiam;
                    _context.KhuyenMais.Update(_update);
                    await _context.SaveChangesAsync();
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Code = 200,
                        Message = "Chỉnh sửa thành công khuyến mãi",

                    };
                }
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",

                };
            }
        }
    }
}
