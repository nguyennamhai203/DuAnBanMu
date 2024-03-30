using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IKhuyenMaiRepository
    {
        public Task<ResponseDto> GetAsync(int? status, int page = 1);
        public Task<ResponseDto> GetAll();

        public Task<ResponseDto> GetAllProductInSale(Guid idSale);
        public Task<ResponseDto> RemoveSelectedProductPromotions(List<Guid> selectedProductIds);

        Task<ResponseDto> CreateAsync(Khuyenmai obj);
        Task<ResponseDto> UpdateAsync(Khuyenmai obj);
        Task<ResponseDto> Update(Khuyenmai obj);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        Task<ResponseDto> GetByIdAsync(Guid Id);

    }
}
