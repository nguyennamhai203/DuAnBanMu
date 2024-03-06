using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Dto;

namespace Shop_Api.Services
{
    public class GioHangChiTietServices : IGioHangChiTietServices
    {
        private readonly IGioHangChiTietRepository _repository;
        public GioHangChiTietServices(IGioHangChiTietRepository repository)
        {
            _repository = repository;
        }
        public Task<ResponseDto> AddCart(string userName, string codeProductDetail)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> CongQuantityCartDetail(Guid idCartDetail)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetAllCarts()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetCartById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetCartJoinForUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> TruQuantityCartDetail(Guid idCartDetail)
        {
            throw new NotImplementedException();
        }
    }
}
