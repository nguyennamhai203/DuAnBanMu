using Microsoft.AspNetCore.Identity;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpDto model);
        public Task<ProfileOfUserDto> FindProfileOfUser(string userName);
        public Task<ResponseDto> CapNhatMatKhau(DoiMatKhauDto obj);
        public Task<ResponseDto> CapNhatSDTDiaChi(userSDTDiaChi obj);
        public Task<ResponseDto> SignUpNhanVienAsync(SignUpDto model);
        public Task<ResponseDto> SignUpKhacHangAsync(SignUpDto model);
        public Task<List<NguoiDung>> GetAllNguoiDungAsync(int? status,int page);
		public Task<LoginResponseDto> LoginAsync(LoginDto model);

        Task<ResponseDto> SendEmailAsync(string email, string subject, string message);

        public Task<bool> UpdateUserInfoAndSendVerificationEmail(Guid userId, UpdateUserInfoDto model, string Pass);
        public Task<ResponseDto> XacNhanTaoTkChoNhanVienAsync(SignUpDto model, string maxacnhan, string emailAdmin);
        public Task<ResponseDto> QuenMk(string maxacnhan, string emailAdmin,string newPass);
        public Task<bool> CheckPass(string username, string Pass);

    }
}
