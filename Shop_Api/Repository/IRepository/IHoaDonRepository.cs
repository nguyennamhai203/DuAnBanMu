using Shop_Models.Entities;
using Shop_Models.Dto;


namespace Shop_Api.Repository.IRepository
{
    public interface IHoaDonRepository

    {
        public List<HoaDon> GetAll();
        public Task<ResponseDto> CreateHD(HoaDon a);
        public Task<ResponseDto> UpdateHD(Guid id, HoaDon a);
        public Task<HoaDon> GetById(Guid id);
        public Task<List<HoaDon>> GetAsync(int? status, int page = 1);
        public Task<HoaDonDto> GetBillByInvoiceCode(string invoiceCode);
        Task<IEnumerable<HoaDonChiTietDto>> GetBillDetailByInvoiceCode(string invoiceCode);
        public Task<ResponseDto> CancelOrder(Guid id, string lydo);
        public Task<ResponseDto> UpdateNgayHoaDonOnline(Guid idHoaDon, DateTime? NgayThanhToan, DateTime? NgayNhan, DateTime? NgayShip);
        public Task<ResponseDto> UpdateThanhToan(Guid idHoaDon, int TrangThaiThanhToan);
        public Task<bool> CheckCustomerExistence(Guid customerId);
        Task<List<HoaDon>> GetByHoaDonStatus(int HoaDonStatus);
        Task<ResponseDto> UpdateHoaDonStatus(Guid id, int HoaDonStatus);
        Task<HoaDon> GetHoaDonByMaHoaDonAsync(string maHoaDon);
        Task<List<HoaDon>> GetCustomerPurchaseHistory(string customerId);
        //public byte[] GeneratePDF();
    }
}
