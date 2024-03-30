using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;

namespace Shop_Api.Controllers
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
            if (result.Success==false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateTT")]
        public async Task<IActionResult> UpdateTT(Guid Id, UpdateUserInfoDto loginDto, string Pass)
        {
            var result = await _accountRepository.UpdateUserInfoAndSendVerificationEmail(Id, loginDto, Pass);
            if (result)
            {
                return Ok(result);
            }
            else return Unauthorized();
        }

        [HttpPost("MailToAdmin")]
        public IActionResult Mail(string mail, string subjectt, string mess)
        {
            var result = _accountRepository.SendEmailAsync(mail, subjectt, mess);
            if (result.Result==true)
            {
                return Ok("oke");
            }
            return BadRequest("No");
        }

        [HttpPost("XacNhan")]
        public async Task<IActionResult> Xac(SignUpDto model, string mail, string codeconfirm)
        {
            var result = await _accountRepository.XacNhanTaoTkChoNhanVienAsync(model, codeconfirm, mail);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("FindProfileOfUser")]
        public async Task<IActionResult> FindProfileOfUser(string userName)
        {
            var result = await _accountRepository.FindProfileOfUser(userName);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPut("CapNhatMatKhau")]
        public async Task<IActionResult> CapNhatMatKhau(DoiMatKhauDto obj)
        {
            var result = await _accountRepository.CapNhatMatKhau(obj);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPut("CapNhatSDTDiaChi")]
        public async Task<IActionResult> CapNhatSDTDiaChi(userSDTDiaChi obj)
        {
            var result = await _accountRepository.CapNhatSDTDiaChi(obj);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
       
    }
}
