using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Shop_Models.Heplers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop_API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<NguoiDung> userManager,
            SignInManager<NguoiDung> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<string> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            var passWordVaild = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !passWordVaild)
            {
                return string.Empty;
            }
            var authClamis = new List<Claim>
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClamis.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            JwtSecurityToken token = new(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClamis,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<IdentityResult> SignUpAsync(SignUpDto model)
        {
            var user = new NguoiDung
            {
                MaNguoiDung = "1" + Guid.NewGuid(),
                TenNguoiDung = model.TenNguoiDung,
                UserName = model.Email,
                Email = model.Email,

            };
            var result = await _userManager.CreateAsync(user, model.PassWord);
            if (result.Succeeded)
            {
                // kiểm tra role KhachHang đã có
                if (!await _roleManager.RoleExistsAsync(AppRole.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.Admin));
                }
                    await _userManager.AddToRoleAsync(user, AppRole.Admin);
                
                
            }
            return result;
        }
    }
}
