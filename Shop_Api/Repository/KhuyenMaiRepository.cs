using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Repository
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
            var check = await _context.KhuyenMais.AnyAsync(x => x.TenKhuyenMai == obj.TenKhuyenMai);
            if (string.IsNullOrEmpty(obj.TenKhuyenMai) || string.IsNullOrEmpty(obj.MaKhuyenMai) || string.IsNullOrEmpty(obj.LoaiHinhKhuyenMai) || obj.NgayBatDau == null || obj.NgayKetThuc == null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Thiếu thông tin",

                };
            }
            if (check == true)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Tên Khuyến Mại Đã Tồn Tại",

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
                else if (obj.MaKhuyenMai == string.Empty || obj.TenKhuyenMai == string.Empty)
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
                Content = result.ToList(),
                IsSuccess = true,
                Code = 200,
                Message = $"trang số {page}/ tổng số trang: {totalPages} ,\n số bản ghi: {result.Count()}/ tổng số bản ghi: {listAll.Count()}",
            };
        }


        public async Task<ResponseDto> GetAll()
        {
            var list = _context.KhuyenMais.AsQueryable();

            //var result = list.Select(sp => new Khuyenmai
            //{
            //    MaKhuyenMai = sp.MaKhuyenMai,
            //    NgayBatDau = sp.NgayBatDau,
            //    NgayKetThuc = sp.NgayKetThuc,
            //    LoaiHinhKhuyenMai = sp.LoaiHinhKhuyenMai,
            //    MucGiam = sp.MucGiam,
            //    TrangThai = sp.TrangThai,
            //});
            if (list != null)
            {
                return new ResponseDto
                {
                    Content = list.ToList(),
                    IsSuccess = true,
                    Code = 200,

                };
            }
            return new ResponseDto
            {
                Content = null,
                IsSuccess = true,
                Code = 405,
                Message = "Chưa có bản ghi nào !"

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
                        Content = _findKM,
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
            var _update = await _context.KhuyenMais.FirstOrDefaultAsync(x => x.Id == obj.Id);
            var checkTrungTen = await _context.KhuyenMais.FirstOrDefaultAsync(x => x.TenKhuyenMai == obj.TenKhuyenMai && x.Id != _update.Id);


            if (checkTrungTen != null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 409,
                    Message = "Tên Khuyến Mãi Đã Tồn Tại",

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
                    _update.LoaiHinhKhuyenMai = obj.LoaiHinhKhuyenMai;
                    _update.MucGiam = obj.MucGiam;
                    _update.TrangThai = obj.TrangThai;
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
        public async Task<ResponseDto> Update(Khuyenmai obj)
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

        public async Task<ResponseDto> GetAllProductInSale(Guid idSale)
        {
            var listkmct = _context.ChiTietKhuyenMais.Where(x => x.KhuyenMaiId == idSale).ToList();
            var sanphamct = _context.ChiTietSanPhams.ToList();
            var listsp = _context.SanPhams.ToList();
            var listcolor = _context.MauSacs.ToList();
            var listth = _context.ThuongHieus.ToList();
            var listcl = _context.ChatLieus.ToList();
            var listxx = _context.XuatXus.ToList();



            var result = (from spct in sanphamct
                          join kmct in listkmct on spct.Id equals kmct.ChiTietSanPhamId
                          join sp in listsp on spct.SanPhamId equals sp.IdSanPham
                          join color in listcolor on spct.MauSacId equals color.Guid
                          join th in listth on spct.ThuongHieuId equals th.Guid
                          join cl in listcl on spct.ChatLieuId equals cl.Guid
                          join xx in listxx on spct.XuatXuId equals xx.Guid

                          select new SanPhamChiTietDto
                          {
                              Id = kmct.ChiTietSanPhamId,
                              MaSanPhamChiTiet = spct.MaSanPham,
                              TenSanPham = sp?.TenSanPham,
                              TenThuongHieu = th?.TenThuongHieu,
                              TenXuatXu = xx?.TenXuatXu,
                              TenMauSac = color?.TenMauSac,
                              TenChatLieu = cl?.TenChatLieu,
                              GiaThucTe = spct?.GiaThucTe,
                              GiaNhap = spct?.GiaNhap,
                              GiaBan = spct.GiaBan,
                              TrangThai = sp.TrangThai,
                              TrangThaiKhuyenMai = spct.TrangThaiKhuyenMai,
                          }).ToList();

            if (result != null)
            {
                return new ResponseDto
                {
                    Content = result,
                    IsSuccess = true,
                    Code = 200,
                    Count = result.Count,
                };
            }
            return new ResponseDto
            {
                Content = null,
                IsSuccess = true,
                Code = 405,
                Message = "Chưa có bản ghi nào !"

            };
        }

        public async Task<ResponseDto> RemoveSelectedProductPromotions(List<Guid> selectedProductIds)
        {
            try
            {
                // Tìm các khuyến mãi chi tiết chứa sản phẩm chi tiết ID đã chọn
                var promotionsToRemove = _context.ChiTietKhuyenMais.Where(kmct => selectedProductIds.Contains(kmct.ChiTietSanPhamId)).ToList();

                // Xóa các khuyến mãi chi tiết tìm thấy
                _context.ChiTietKhuyenMais.RemoveRange(promotionsToRemove);
                await _context.SaveChangesAsync();

                return new ResponseDto
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "Xóa các khuyến mãi chi tiết thành công."
                };
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = $"Đã xảy ra lỗi: {ex.Message}"
                };
            }
        }


    }
}
