using Microsoft.AspNetCore.Identity;
using Shop_Models.Dto;

namespace Shop_API.Repository.IRepository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpDto model);
        public Task<string>  LoginAsync(LoginDto model);
    }
}
