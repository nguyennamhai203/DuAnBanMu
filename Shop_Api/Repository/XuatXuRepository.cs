using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Shop_Api.Repository
{
    public class XuatXuRepository : IXuatXuRepository
    {
        private readonly ApplicationDbContext contextXX;
        public static int PAGE_SIZE { get; set; } = 1;
        public XuatXuRepository(ApplicationDbContext context)
        {
            contextXX = context;
        }
        public async Task<ResponseDto> CreateXX(XuatXu add)
        {
            try
            {
                await contextXX.XuatXus.AddAsync(add);
                await contextXX.SaveChangesAsync();
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

        public async Task<ResponseDto> DeleteXX(Guid Id)
        {
            var iddelete = await contextXX.XuatXus.FindAsync(Id);
            try
            {
                contextXX.XuatXus.Remove(iddelete);
                await contextXX.SaveChangesAsync();
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

        public async Task<List<XuatXu>> GetListXuatXu(int? status, int page = 1)
        {
            var list = contextXX.XuatXus.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }
            var result = list.Select(x => new XuatXu
            {
                Guid = x.Guid,
                MaXuatXu = x.MaXuatXu,
                TenXuatXu = x.TenXuatXu,
                TrangThai = x.TrangThai
            });
            return result.ToList();
        }

        public async Task<List<XuatXu>> GetXuatXu()
        {
            return await contextXX.XuatXus.ToListAsync();
        }

        public async Task<ResponseDto> UpdateXX(XuatXu update)
        {
            var idupdate = await contextXX.XuatXus.FindAsync(update.Guid);
            try
            {
                idupdate.MaXuatXu = update.MaXuatXu;
                idupdate.TenXuatXu = update.TenXuatXu;
                idupdate.TrangThai = update.TrangThai;
                contextXX.XuatXus.Update(idupdate);
                await contextXX.SaveChangesAsync();
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

        public async Task<ResponseDto> GetByIdXuatXu(Guid id)
        {
            var getid = await contextXX.XuatXus.FindAsync(id);
            try
            {
                return new ResponseDto
                {
                    IsSuccess = true,
                    Content = getid,
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

    }
}