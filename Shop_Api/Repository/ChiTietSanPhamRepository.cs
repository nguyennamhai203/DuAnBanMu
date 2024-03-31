using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
//using System.Data.Entity;

namespace Shop_Api.Repository
{
    public class ChiTietSanPhamRepository : IChiTietSanPhamRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public static int PAGE_SIZE { get; set; } = 3;
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
                    Content = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Trùng Mã",
                };
            }
            if (model.SanPhamId == Guid.Empty)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Bắt buộc phải chọn tên sản phẩm",

                };
            }
            if (model.GiaNhap == 0 || model.GiaBan == 0)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Giá phải lớn hơn 0",

                };
            }
            try
            {
                await _dbContext.ChiTietSanPhams.AddAsync(model);
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

        public async Task<ResponseDto> UpdateAsync(ChiTietSanPham model)
        {
            var chiTietSanPham = await _dbContext.ChiTietSanPhams.FindAsync(model.Id);
            if (chiTietSanPham == null)
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
                chiTietSanPham.MaSanPham = model.MaSanPham;
                chiTietSanPham.GiaBan = model.GiaBan;
                chiTietSanPham.GiaNhap = model.GiaNhap;
                //chiTietSanPham.GiaThucTe = model.GiaThucTe;
                chiTietSanPham.SoLuongTon = model.SoLuongTon;
                //chiTietSanPham.SoLuongDaBan = model.SoLuongDaBan;
                chiTietSanPham.TrangThaiKhuyenMai = model.TrangThaiKhuyenMai;
                chiTietSanPham.Mota = model.Mota;

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
        public async Task<ResponseDto> UpdateAsync2(ChiTietSanPham model)
        {
            var chiTietSanPham = await _dbContext.ChiTietSanPhams.FindAsync(model.Id);
            if (chiTietSanPham == null)
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
                chiTietSanPham.TrangThaiKhuyenMai = model.TrangThaiKhuyenMai;
                _dbContext.ChiTietSanPhams.Update(chiTietSanPham);
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
            var chiTietSanPham = await _dbContext.ChiTietSanPhams.FindAsync(Id);
            if (chiTietSanPham == null)
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
                //chiTietSanPham.TrangThai = 2;
                _dbContext.ChiTietSanPhams.Remove(chiTietSanPham);
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

        public async Task<List<ChiTietSanPham>> GetAsync()
        {
            var list = await _dbContext.ChiTietSanPhams.ToListAsync();
            return list;
        }

        public async Task<List<SanPhamChiTietDto>> GetAllAsync(int? status/*, int page = 1*/)
        {
            var list = _dbContext.ChiTietSanPhams.AsQueryable();
            var result = list.Select(sp => new SanPhamChiTietDto
            {
                Id = sp.Id,
                MaSanPhamChiTiet = sp.MaSanPham,
                GiaBan = sp.GiaBan,
                GiaNhap = sp.GiaNhap,
                GiaThucTe = sp.GiaThucTe,
                SoLuongTon = sp.SoLuongTon,
                SoLuongDaBan = sp.SoLuongDaBan,


                MaSanPham = sp.MaSanPham,
                TenSanPham = sp.SanPham.TenSanPham,

                MaLoai = sp.Loai.MaLoai,
                TenLoai = sp.Loai.TenLoai,

                MaThuongHieu = sp.ThuongHieu.MaThuongHieu,
                TenThuongHieu = sp.ThuongHieu.TenThuongHieu,

                MaXuatXu = sp.XuatXu.MaXuatXu,
                TenXuatXu = sp.XuatXu.TenXuatXu,

                MaMauSac = sp.MauSac.MaMauSac,
                TenMauSac = sp.MauSac.TenMauSac,

                MaChatLieu = sp.ChatLieu.MaChatLieu,
                TenChatLieu = sp.ChatLieu.TenChatLieu,

                TrangThai = sp.TrangThai,
                TrangThaiKhuyenMai = sp.TrangThaiKhuyenMai,

                SanPhamId = sp.SanPhamId,
                LoaiId = sp.LoaiId,
                ThuongHieuId = sp.ThuongHieuId,
                XuatXuId = sp.XuatXuId,
                MauSacId = sp.MauSacId,
                ChatLieuId = sp.ChatLieuId,

                AnhThuNhat = _dbContext.Anhs
                     .Where(image => image.ChiTietSanPhamId == sp.Id && image.MaAnh == "Anh1")
                     .Select(image => image.URL)
                     .FirstOrDefault(),
                OtherImages = _dbContext.Anhs
                     .Where(image => image.ChiTietSanPhamId == sp.Id && image.MaAnh != "Anh1")
                     .Select(image => image.URL)
                     .ToList(),

            });

            if (status != null)
            {

                return result.Where(x => x.TrangThai == status).ToList();
            }
            return result.ToList();


        }

        public async Task<List<SanPhamChiTietDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? tenSanPham, double? from, double? to, string? sortBy, int? page, string? tenLoai, string? tenThuongHieu, string? tenMauSac, string? tenXuatXu, string? chatLieu, int? PageSize)
        {
            page ??= 1;
            PageSize ??= 2;
            var query = _dbContext.ChiTietSanPhams.AsQueryable()
                .AsNoTracking()
                .Include(a => a.Anhs)
                .Where(a => (status == null || a.TrangThai == status) && (codeProductDetail != null ? a.MaSanPham == codeProductDetail : true))
                .Select(a => new SanPhamChiTietDto
                {
                    Id = a.Id,
                    MaSanPhamChiTiet = a.MaSanPham,
                    GiaNhap = a.GiaNhap,
                    GiaBan = a.GiaBan,
                    GiaThucTe = a.GiaThucTe,
                    SoLuongTon = a.SoLuongTon,
                    SoLuongDaBan = a.SoLuongDaBan,
                    TrangThai = a.TrangThai,
                    MaSanPham = a.SanPham.MaSanPham,
                    TenSanPham = a.SanPham.TenSanPham,
                    MaLoai = a.Loai.MaLoai,
                    TenLoai = a.Loai.TenLoai,
                    MaThuongHieu = a.ThuongHieu.MaThuongHieu,
                    TenThuongHieu = a.ThuongHieu.TenThuongHieu,
                    MaXuatXu = a.XuatXu.MaXuatXu,
                    TenXuatXu = a.XuatXu.TenXuatXu,
                    MaMauSac = a.MauSac.MaMauSac,
                    TenMauSac = a.MauSac.TenMauSac,
                    MaChatLieu = a.ChatLieu.MaChatLieu,
                    TenChatLieu = a.ChatLieu.TenChatLieu,
                    AnhThuNhat = _dbContext.Anhs
                    .Where(image => image.ChiTietSanPhamId == a.Id && image.MaAnh == "Anh1")
                    .Select(image => image.URL)
                    .FirstOrDefault(),
                    OtherImages = _dbContext.Anhs
                    .Where(image => image.ChiTietSanPhamId == a.Id && image.MaAnh != "Anh1")
                    .Select(image => image.URL)
                    .ToList(),
                });

            if (getNumber > 0)
            {
                query = query.Take(Convert.ToInt32(getNumber));
            }
            if (!string.IsNullOrEmpty(tenSanPham))
            {
                query = query.Where(x => x.TenSanPham.Contains(tenSanPham));
            }
            if (from.HasValue)
            {
                query = query.Where(x => x.GiaBan >= from);
            }
            if (to.HasValue)
            {
                query = query.Where(x => x.GiaBan <= to);
            }
            if (!string.IsNullOrEmpty(tenLoai))
            {
                query = query.Where(x => x.TenLoai.Contains(tenLoai));
            };

            if (!string.IsNullOrEmpty(tenThuongHieu))
            {
                query = query.Where(x => x.TenThuongHieu.Contains(tenThuongHieu));
            };

            if (!string.IsNullOrEmpty(tenMauSac))
            {
                query = query.Where(x => x.TenMauSac.Contains(tenMauSac));
            };

            if (!string.IsNullOrEmpty(tenXuatXu))
            {
                query = query.Where(x => x.TenXuatXu.Contains(tenXuatXu));
            };

            if (!string.IsNullOrEmpty(chatLieu))
            {
                query = query.Where(x => x.TenChatLieu.Contains(chatLieu));
            };



            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "nameproduct_desc":
                        query = query.OrderByDescending(x => x.TenSanPham);
                        break;
                    case "price_asc":
                        query = query.OrderBy(x => x.GiaBan);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(x => x.GiaBan);
                        break;
                }
            }

           

            //if (PageSize == 0)
            //{
            //    PageSize = PAGE_SIZE;
            //    query = query.Skip((int)((page - 1) * pageSize)).Take(pageSize);
            //}
            //query = query.Skip((int)((page - 1) * PageSize)).Take((int)PageSize);
            //var pageSize = 1;

             var pageSize = PAGE_SIZE;
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Clamp((int)page, 1, totalPages);

            query = query.Skip((int)((page - 1) * pageSize)).Take(pageSize);



            var result = await query.ToListAsync();


            return result;
        }



        public async Task<List<SPDanhSachViewModel>> GetFilteredDaTaDSTongQuanAynsc(ParametersTongQuanDanhSach parametersTongQuanDanhSach)
        {
            var query = _dbContext.ChiTietSanPhams.AsQueryable();

            if (parametersTongQuanDanhSach.IdSanPham == Guid.Empty)
            {
                query = query.Where(it => it.SanPhamId == parametersTongQuanDanhSach.IdSanPham);
            }

            if (parametersTongQuanDanhSach.IdThuongHieu == Guid.Empty)
            {
                query = query.Where(it => it.ThuongHieuId == parametersTongQuanDanhSach.IdThuongHieu);
            }



            if (parametersTongQuanDanhSach.IdChatLieu == Guid.Empty)
            {
                query = query.Where(it => it.ChatLieuId == parametersTongQuanDanhSach.IdChatLieu);
            }

            if (parametersTongQuanDanhSach.IdLoaiMu == Guid.Empty)
            {
                query = query.Where(it => it.LoaiId == parametersTongQuanDanhSach.IdLoaiMu);
            }

            if (parametersTongQuanDanhSach.IdXuatXu == Guid.Empty)
            {
                query = query.Where(it => it.XuatXuId == parametersTongQuanDanhSach.IdXuatXu);
            }

            var result = query
                .Include(sp => sp.SanPham)
                .Include(sp => sp.ThuongHieu)

                .Include(sp => sp.Loai)
                .Include(sp => sp.XuatXu)
                .Include(sp => sp.MauSac)
                .Include(sp => sp.ChatLieu);

            var viewModelResult = result
                .GroupBy(gr => new
                {
                    gr.ChatLieuId,
                    gr.SanPhamId,
                    gr.ThuongHieuId,
                    gr.XuatXuId,
                    gr.LoaiId,
                })
                .Select(gr => new SPDanhSachViewModel
                {
                    SumGuild = $"{gr.Key.SanPhamId}/{gr.Key.ThuongHieuId}/{gr.Key.LoaiId}/{gr.Key.XuatXuId}/{gr.Key.ChatLieuId}",
                    SanPham = gr.First().SanPham.TenSanPham,
                    ChatLieu = gr.First().ChatLieu.TenChatLieu,
                    LoaiMu = gr.First().Loai.TenLoai,
                    ThuongHieu = gr.First().ThuongHieu.TenThuongHieu,
                    XuatXu = gr.First().XuatXu.TenXuatXu,
                    SoMau = gr.Select(it => it.MauSacId).Distinct().Count(),
                      LstMauSac = gr.Select(it => new SelectListItem { Value = it.MauSacId.ToString(), Text = it.MauSac.TenMauSac }).ToList(),
                    TongSoLuongTon = gr.Sum(it => it.SoLuongTon.GetValueOrDefault()),
                    TongSoLuongDaBan = gr.Sum(it => it.SoLuongDaBan.GetValueOrDefault())
                });

            if (!string.IsNullOrEmpty(parametersTongQuanDanhSach.SearchValue))
            {
                parametersTongQuanDanhSach.Length = query.Count();
                string searchValueLower = parametersTongQuanDanhSach.SearchValue.ToLower();
                viewModelResult = viewModelResult
                    .Where(x =>
                        x.SanPham!.ToLower().Contains(searchValueLower) ||
                        x.LoaiMu!.ToLower().Contains(searchValueLower) ||
                        x.ChatLieu!.ToLower().Contains(searchValueLower)
                        );
            }

            return await viewModelResult.ToListAsync();
        }


        //public async Task<(List<SPDanhSachViewModel>, int)> GetFilteredDataDSTongQuanAsync(ParametersTongQuanDanhSach parametersTongQuanDanhSach)
        //{
        //    var query = _dbContext.ChiTietSanPhams.AsQueryable();

        //    // Thêm biến mới để lưu trữ tổng số bản ghi
        //    int totalRecords = 0;

        //    // Thực hiện các bước lọc dữ liệu như bạn đã làm trước đó

        //    // Tính tổng số lượng bản ghi
        //    totalRecords = await query.CountAsync();

        //    // Áp dụng phân trang và lấy dữ liệu từ cơ sở dữ liệu
        //    var result = await query
        //        .Include(sp => sp.SanPham)
        //        .Include(sp => sp.ThuongHieu)
        //        .Include(sp => sp.Loai)
        //        .Include(sp => sp.XuatXu)
        //        .Include(sp => sp.ChatLieu)
        //        .Skip((parametersTongQuanDanhSach.Start - 1) * parametersTongQuanDanhSach.Length)
        //        .Take(parametersTongQuanDanhSach.Length)
        //        .GroupBy(gr => new
        //        {
        //            gr.ChatLieuId,
        //            gr.SanPhamId,
        //            gr.ThuongHieuId,
        //            gr.XuatXuId,
        //            gr.LoaiId,
        //        })
        //        .Select(gr => new SPDanhSachViewModel
        //        {
        //            // Thêm các trường ViewModel cần thiết
        //        })
        //        .ToListAsync();

        //    // Trả về cả danh sách kết quả và tổng số bản ghi
        //    return (result, totalRecords);
        //}

        public List<SanPhamChiTietDto> GetRelatedProducts(string sumGuid)
        {
            try
            {
                var listGuid = sumGuid.ToString().Split('/');
                var idSanPham = Guid.Parse(listGuid[0]);
                var idThuongHieu = Guid.Parse(listGuid[1]);
                var idLoai = Guid.Parse(listGuid[2]);
                var idXuatXu = Guid.Parse(listGuid[3]);
                var idChatLieu = Guid.Parse(listGuid[4]);
                return _dbContext
                        .ChiTietSanPhams.AsQueryable()
                        .Where(sp =>
                        sp.SanPhamId == idSanPham &&
                        sp.ThuongHieuId == idThuongHieu &&
                        sp.LoaiId == idLoai &&
                        sp.XuatXuId == idXuatXu &&
                        sp.ChatLieuId == idChatLieu
                        ).
                        Include(sp => sp.SanPham).
                        Include(sp => sp.MauSac).
                        ////Include(sp => sp.Anh).
                        //AsEnumerable().
                        //GroupBy(sp => sp.MauSacId).
                        //OrderBy(gr => gr.Key).
                        //SelectMany(gr => gr.OrderBy(sp => sp.KichCo.SoKichCo.GetValueOrDefault())).
                        Select(a => new SanPhamChiTietDto
                        {
                            Id = a.Id,
                            MaSanPhamChiTiet = a.MaSanPham,
                            GiaNhap = a.GiaNhap,
                            GiaBan = a.GiaBan,
                            GiaThucTe = a.GiaThucTe,
                            SoLuongTon = a.SoLuongTon,
                            SoLuongDaBan = a.SoLuongDaBan,
                            TrangThai = a.TrangThai,
                            TrangThaiKhuyenMai = a.TrangThaiKhuyenMai,
                            MaSanPham = a.SanPham.MaSanPham,
                            TenSanPham = a.SanPham.TenSanPham,
                            MaLoai = a.Loai.MaLoai,
                            TenLoai = a.Loai.TenLoai,
                            MaThuongHieu = a.ThuongHieu.MaThuongHieu,
                            TenThuongHieu = a.ThuongHieu.TenThuongHieu,
                            MaXuatXu = a.XuatXu.MaXuatXu,
                            TenXuatXu = a.XuatXu.TenXuatXu,
                            MaMauSac = a.MauSac.MaMauSac,
                            TenMauSac = a.MauSac.TenMauSac,
                            MaChatLieu = a.ChatLieu.MaChatLieu,
                            TenChatLieu = a.ChatLieu.TenChatLieu,
                            AnhThuNhat = _dbContext.Anhs
                    .Where(image => image.ChiTietSanPhamId == a.Id && image.MaAnh == "Anh1")
                    .Select(image => image.URL)
                    .FirstOrDefault(),
                            OtherImages = _dbContext.Anhs
                    .Where(image => image.ChiTietSanPhamId == a.Id && image.MaAnh != "Anh1")
                    .Select(image => image.URL)
                    .ToList()
                        })
                        .ToList();
            }
            catch (Exception)
            {

                return new List<SanPhamChiTietDto>();
            }

        }

        public async Task<SanPhamChiTietDto> DetailSanPhamChiTietDto(Guid Id)
        {
            var query = await _dbContext.ChiTietSanPhams.AsQueryable()
                .AsNoTracking()
                .Include(a => a.Anhs)
                .Where(x => x.Id == Id)
                .Select(a => new SanPhamChiTietDto
                {
                    Id = a.Id,
                    MaSanPhamChiTiet = a.MaSanPham,
                    GiaNhap = a.GiaNhap,
                    GiaBan = a.GiaBan,
                    GiaThucTe = a.GiaThucTe,
                    SoLuongTon = a.SoLuongTon,
                    SoLuongDaBan = a.SoLuongDaBan,
                    TrangThai = a.TrangThai,
                    TrangThaiKhuyenMai = a.TrangThaiKhuyenMai,
                    Mota = a.Mota,
                    MaSanPham = a.SanPham.MaSanPham,
                    TenSanPham = a.SanPham.TenSanPham,
                    MaLoai = a.Loai.MaLoai,
                    TenLoai = a.Loai.TenLoai,
                    MaThuongHieu = a.ThuongHieu.MaThuongHieu,
                    TenThuongHieu = a.ThuongHieu.TenThuongHieu,
                    MaXuatXu = a.XuatXu.MaXuatXu,
                    TenXuatXu = a.XuatXu.TenXuatXu,
                    MaMauSac = a.MauSac.MaMauSac,
                    TenMauSac = a.MauSac.TenMauSac,
                    MaChatLieu = a.ChatLieu.MaChatLieu,
                    TenChatLieu = a.ChatLieu.TenChatLieu,
                    AnhThuNhat = _dbContext.Anhs
                        .Where(image => image.ChiTietSanPhamId == a.Id && image.MaAnh == "Anh1")
                        .Select(image => image.URL)
                        .FirstOrDefault(),
                    OtherImages = _dbContext.Anhs
                        .Where(image => image.ChiTietSanPhamId == a.Id && image.MaAnh != "Anh1")
                        .Select(image => image.URL)
                        .ToList(),
                })
                .FirstOrDefaultAsync();

            return (SanPhamChiTietDto)query;
        }


    }
}
