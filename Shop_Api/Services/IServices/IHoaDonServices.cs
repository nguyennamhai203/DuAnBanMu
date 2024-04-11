using Shop_Models.Dto;

namespace Shop_Api.Services.IServices
{
    public interface IHoaDonServices
    {
        public Task<ResponseDto> CreateBill(RequestBillDto requestBill);
    }
}
