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

		public async Task<List<SanPhamChiTietDto>> GetAsync(int? status/*, int page = 1*/)
		{
			var list = _dbContext.ChiTietSanPhams.AsQueryable();


			//#region Paging
			//list = list.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

			//#endregion

			var result = list.Select(sp => new SanPhamChiTietDto
			{

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

				//MaXuatXu = sp.XuatXu.MaXuatXu,
				//TenXuatXu = sp.XuatXu.TenXuatXu,

				//MaMauSac = sp.MauSac.MaMauSac,
				//TenMauSac = sp.MauSac.TenMauSac,

				//MaChatLieu = sp.ChatLieu.MaChatLieu,
				//TenChatLieu = sp.ChatLieu.TenChatLieu,

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
			return result.ToList();
		}

		public async Task<List<SanPhamChiTietDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? tenSanPham, double? from, double? to, string? sortBy, int? page, string? tenLoai, string? tenThuongHieu, string? tenMauSac, string? tenXuatXu, string? chatLieu)
		{
			page ??= 1;
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

			var pageSize = PAGE_SIZE;
			//var totalItems = await query.CountAsync();
			//var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
			//page = Math.Clamp((int)page, 1, totalPages);
			query = query.Skip((int)((page - 1) * pageSize)).Take(pageSize);
			//query = query.Skip((int)((page - 1) * pageSize)).Take(pageSize);



			//var result = await query.ToListAsync();


			return query.ToList();
		}

	}
}
