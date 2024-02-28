using Microsoft.AspNetCore.Identity;
using Shop_Models.Dto;

namespace Shop_Api.Repository.IRepository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpDto model);
        public Task<string> LoginAsync(LoginDto model);

        Task<bool> SendEmailAsync(string email, string subject, string message);

        public Task<bool> UpdateUserInfoAndSendVerificationEmail(Guid userId, UpdateUserInfoDto model,string Pass);
        public Task<ResponseDto> XacNhanTaoTkChoNhanVienAsync(SignUpDto model, string maxacnhan, string emailAdmin);
        public Task<bool> CheckPass(string username, string Pass);

    }
}
