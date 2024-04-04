using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IVoucherRepository
    {
        public Task<ResponseDto> CreateVoucher(Voucher add);
        public Task<ResponseDto> UpdateVoucher(Voucher update, Guid id);
        public Task<ResponseDto> DeleteVoucher(Guid id);
        public Task<List<Voucher>> GetVoucher();
        public Task<List<Voucher>> GetListVoucher(int? status, int page = 1);
        public Task<Voucher> GetByIdVoucher(Guid id);
    }
}