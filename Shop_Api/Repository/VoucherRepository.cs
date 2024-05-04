using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ApplicationDbContext contextVC;
        public static int PAGE_SIZE { get; set; } = 1;
        public VoucherRepository(ApplicationDbContext context)
        {
            contextVC = context;
        }
        public async Task<ResponseDto> CreateVoucher(Voucher add)
        {
            try
            {
                await contextVC.Vouchers.AddAsync(add);
                await contextVC.SaveChangesAsync();
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

        public async Task<ResponseDto> DeleteVoucher(Guid Id)
        {
            var iddelete = await contextVC.Vouchers.FindAsync(Id);
            try
            {
                contextVC.Vouchers.Remove(iddelete);
                await contextVC.SaveChangesAsync();
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

        public async Task<Voucher> GetByIdVoucher(Guid id)
        {
            return await contextVC.Vouchers.FindAsync(id);
        }

        public async Task<List<Voucher>> GetListVoucher(int? status, int page = 1)
        {
            var list = contextVC.Vouchers.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }
            var result = list.Select(x => new Voucher
            {
                Guid = x.Guid,
                MaVoucher = x.MaVoucher,
                TenVoucher = x.TenVoucher,
                PhanTramGiam = x.PhanTramGiam,
                SoLuong = x.SoLuong,
                NgayBatDau = x.NgayBatDau,
                NgayHetHan = x.NgayHetHan,
                TrangThai = x.TrangThai
            });
            return result.ToList();
        }
        public async Task<List<Voucher>> GetAll()
        {
            var list = contextVC.Vouchers.AsQueryable();        
            var result = list.Select(x => new Voucher
            {
                Guid = x.Guid,
                MaVoucher = x.MaVoucher,
                TenVoucher = x.TenVoucher,
                PhanTramGiam = x.PhanTramGiam,
                SoLuong = x.SoLuong,
                NgayBatDau = x.NgayBatDau,
                NgayHetHan = x.NgayHetHan,
                TrangThai = x.TrangThai
            });
            return result.ToList();
        }

        public async Task<List<Voucher>> GetVoucher()
        {
            var list = contextVC.Vouchers.AsQueryable();
            return list.ToList();
        }

        public async Task<ResponseDto> UpdateVoucher(Voucher update, Guid id)
        {
            var idupdate = await contextVC.Vouchers.FindAsync(id);
            try
            {
                idupdate.MaVoucher = update.MaVoucher;
                idupdate.TenVoucher = update.TenVoucher;
                idupdate.PhanTramGiam = update.PhanTramGiam;
                idupdate.SoLuong = update.SoLuong;  
                idupdate.NgayBatDau = update.NgayBatDau;
                idupdate.NgayHetHan = update.NgayHetHan;
                idupdate.TrangThai = update.TrangThai;
                contextVC.Vouchers.Update(idupdate);
                await contextVC.SaveChangesAsync();
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