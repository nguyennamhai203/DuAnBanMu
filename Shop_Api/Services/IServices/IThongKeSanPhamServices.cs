using Shop_Models.Dto;

namespace Shop_Api.Services.IServices
{
    public interface IThongKeSanPhamServices
    {
        Task<ResponseDto> GetThongKeSanPhamTheoNgay(DateTime datetime,int? status,int page = 1);
    }
}