using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Services.IServices
{
    public interface IHoaDonServices
    {
        public Task<ResponseDto> CreateBill(RequestBillDto requestBill);
        public Task<ResponseDto> PGetBillByInvoiceCode(string invoiceCode);
        public Task<ResponseDto> CreateHoaDonTaiQuay(RequestBillDto requestBill);
        public List<HoaDonChoDTO> GetAllHDTaiQuay();
        public ResponseDto TaoHoaDonTaiQuay(RequestHDTaiQuay requestHDTaiQuay);
        public Task<ResponseDto> AddBillDetail(string mahoadon, string codeProductDetail, int? soluong);
        public ResponseDto CapNhatSoLuongHoaDonChiTietTaiQuay(string maHoaDon, string maSPCT, int soLuong);
        public Task<ResponseDto> TruQuantityBillDetail(Guid idBillDetail);
        public Task<ResponseDto> CongQuantityBillDetail(Guid idBillDetail);
        public bool ThanhToan(HoaDon _hoaDon);
        public string XoaSanPhamKhoiHoaDon(string maHoaDon, string maSPCT);
        public Task<ResponseDto> HuyHoaDonAsync(string maHoaDon, string maSPCT);

    }
}
