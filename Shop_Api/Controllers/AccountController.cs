using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDto signUpModel)
        {
            var result = await _accountRepository.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            else return Unauthorized();

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _accountRepository.LoginAsync(loginDto);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }return Ok(result);
        }

    }
}
