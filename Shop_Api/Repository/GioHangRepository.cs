using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Repository
{
	public class GioHangRepository : IGioHangRepository
	{
		private readonly ApplicationDbContext contextGH;
		public static int PAGE_SIZE { get; set; } = 1;
		public GioHangRepository(ApplicationDbContext context)
		{
			contextGH = context;
		}
		public async Task<ResponseDto> CreateGioHang(GioHang add)
		{
			try
			{
				await contextGH.GioHangs.AddAsync(add);
				await contextGH.SaveChangesAsync();
				return new ResponseDto
				{
					Code = 200,
					Message = "Them thanh cong"
				};
			}
			catch (Exception)
			{
				return new ResponseDto
				{
					Code = 500,
					Message = "Them loi roi"
				};
			}
		}

		public async Task<ResponseDto> DeleteGioHang(Guid Id)
		{
			var iddelete = await contextGH.GioHangs.FindAsync(Id);
			try
			{
				contextGH.GioHangs.Remove(iddelete);
				await contextGH.SaveChangesAsync();
				return new ResponseDto
				{
					Code = 200,
					Message = "Xoa thanh cong"
				};
			}
			catch (Exception)
			{
				return new ResponseDto
				{
					Code = 500,
					Message = "Xoa loi roi"
				};
			}
		}

		public async Task<ResponseDto> GetByIdGioHang(Guid id)
		{
			var getid = await contextGH.GioHangs.FindAsync(id);
			try
			{

				if (getid != null)
				{
					return new ResponseDto
					{
						IsSuccess = true,
						Content = getid,
						Code = 200,
						Message = "Da tim thay du lieu"
					};
				}
				else
				{
					return new ResponseDto
					{
						IsSuccess = false,
						Code = 405,
						Message = "Khong thay du lieu"
					};
				}
			}
			catch (Exception)
			{
				return new ResponseDto
				{
					IsSuccess = false,
					Code = 500,
					Message = "Loi He Thong"
				};
			}
		}

		public async Task<List<GioHang>> GetGioHang()
		{
			return await contextGH.GioHangs.ToListAsync();
		}

		public async Task<List<GioHang>> GetListGioHang(int? status, int page = 1)
		{
			var list = contextGH.GioHangs.AsQueryable();
			if (status.HasValue)
			{
				list = list.Where(x => x.TrangThai == status);
			}
			var result = list.Select(x => new GioHang
			{
				IdNguoiDung = x.IdNguoiDung,
				NguoiDung = x.NguoiDung,
				TrangThai = x.TrangThai
			});
			return result.ToList();
		}

		public async Task<ResponseDto> UpdateGioHang(GioHang update)
		{
			var idupdate = await contextGH.GioHangs.FindAsync(update.IdNguoiDung);
			try
			{
				idupdate.TrangThai = update.TrangThai;
				contextGH.GioHangs.Update(update);
				await contextGH.SaveChangesAsync();
				return new ResponseDto
				{
					Code = 200,
					Message = "Cap nhat thanh cong"
				};
			}
			catch (Exception)
			{
				return new ResponseDto
				{
					Code = 500,
					Message = "Cap nhat loi roi"
				};
			}
		}
	}
}