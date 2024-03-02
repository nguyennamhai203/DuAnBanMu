using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Repository
{
    public class SanPhamYeuThichRepository : ISanPhamYeuThichRepository
    {
        private readonly ApplicationDbContext contextSPYT;
        public static int PAGE_SIZE { get; set; } = 1;
        public SanPhamYeuThichRepository(ApplicationDbContext context)
        {
            contextSPYT = context;
        }
        public async Task<ResponseDto> CreateSPYT(SanPhamYeuThich add)
        {
            try
            {
                await contextSPYT.SanPhamYeuThichs.AddAsync(add);
                await contextSPYT.SaveChangesAsync();
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

        public async Task<ResponseDto> DeleteSPYT(Guid Id)
        {
            var iddelete = await contextSPYT.SanPhamYeuThichs.FindAsync(Id);
            try
            {
                contextSPYT.SanPhamYeuThichs.Remove(iddelete);
                await contextSPYT.SaveChangesAsync();
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

        public async Task<ResponseDto> GetByIdSPYT(Guid id)
        {
            var getid = await contextSPYT.SanPhamYeuThichs.FindAsync(id);
            try
            {
                return new ResponseDto
                {
                    IsSuccess = true,
                    Result = getid,
                    Code = 200,
                    Message = "Da tim thay du lieu"
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "Khong thay du lieu"
                };
            }
        }

        public async Task<List<SanPhamYeuThich>> GetListSPYT(int? status, int page = 1)
        {
            var list = contextSPYT.SanPhamYeuThichs.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }
            var result = list.Select(x => new SanPhamYeuThich
            {
                Id = x.Id,
                NguoiDungId = x.NguoiDungId,
                ChiTietSanPhamId = x.ChiTietSanPhamId,
                TrangThai = x.TrangThai
            });
            return result.ToList();
        }

        public async Task<List<SanPhamYeuThich>> GetSPYT()
        {
            return await contextSPYT.SanPhamYeuThichs.ToListAsync();
        }

        public async Task<ResponseDto> UpdateSPYT(SanPhamYeuThich update)
        {
            var idupdate = await contextSPYT.SanPhamYeuThichs.FindAsync(update.Id);
            try
            {
                idupdate.NguoiDungId = update.NguoiDungId;
                idupdate.ChiTietSanPhamId = update.ChiTietSanPhamId;
                idupdate.TrangThai = update.TrangThai;
                contextSPYT.SanPhamYeuThichs.Update(update);
                await contextSPYT.SaveChangesAsync();
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