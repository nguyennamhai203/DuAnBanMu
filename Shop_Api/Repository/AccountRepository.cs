using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Shop_Models.Heplers;
using System;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;


using MailKit.Net.Smtp;
using MimeKit;
using System;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace Shop_Api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<ChucVu> _roleManager;
        private readonly ApplicationDbContext _context;


        public AccountRepository(UserManager<NguoiDung> userManager,
            SignInManager<NguoiDung> signInManager, IConfiguration configuration, RoleManager<ChucVu> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _context = context;
        }


        public async Task<ProfileOfUserDto> FindProfileOfUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return new ProfileOfUserDto
            {
                TenTaiKhoan = user.UserName,
                TenNguoiDung = user.TenNguoiDung,
                soDienThoai = user.SoDienThoai,
                DiaChi = user.DiaChi,
                Email = user.Email,
                GioiTinh = user.GioiTinh,
            };

        }

        public async Task<ResponseDto> CapNhatMatKhau(DoiMatKhauDto obj)
        {
            var user = await _userManager.FindByNameAsync(obj.UserName);
            var oldPassWordVaild = await _userManager.CheckPasswordAsync(user, obj.OldPass);
            var newPassVaild = await _userManager.CheckPasswordAsync(user, obj.NewPass);

            if (obj.NewPass != obj.RemindNewPass)
            {
                return new ResponseDto
                {
                    Content = null,
                    Message = "Nhắc lại mật khẩu không trùng khớp",
                    Code = 400,
                };
            }
            else if (oldPassWordVaild == false)
            {
                return new ResponseDto
                {
                    Content = null,
                    Message = "Mật khẩu cũ sai",
                    Code = 400,
                };
            }
            else if (newPassVaild == true)
            {
                return new ResponseDto
                {
                    Content = null,
                    Message = "Bạn Đã Nhập Mật Khẩu Cũ",
                    Code = 400,
                };
            }
            else if (oldPassWordVaild)
            {
                // Xác minh mật khẩu cũ
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, obj.NewPass);
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return new ResponseDto
                    {
                        Content = user,
                        Message = "Cập Nhật Thành Công",
                        Code = 200,
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        Content = null,
                        Message = "Cập Nhật Thất Bại",
                        Code = 400,
                    };
                }
            }
            else
            {
                return new ResponseDto
                {
                    Content = user,
                    Message = "Mật khẩu cũ sai",
                    Code = 500,
                };
            }



        }
        public async Task<ResponseDto> CapNhatSDTDiaChi(userSDTDiaChi obj)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(obj.userName);


                if (obj.SDT == null || obj.SDT == "" || obj.DiaChi == null || obj.DiaChi == "")
                {
                    return new ResponseDto
                    {
                        Content = null,
                        Message = "Thiếu Thông Tin Đầu Vào!",
                        Code = 405,
                    };

                }
                else if (user == null) { return new ResponseDto { Message = "Không tìm thấy người dùng",Code=405 }; }
                else
                {
                    if (IsValidPhoneNumber(obj.SDT) == true)
                    {
                        user.SoDienThoai = obj.SDT;
                        user.DiaChi = obj.DiaChi;
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return new ResponseDto
                            {
                                Content = user,
                                Message = "Cập Nhật Thành Công",
                                Code = 200,
                            };
                        }
                        return new ResponseDto
                        {
                            Content = user,
                            Message = "Cập Nhật Thất Bại",
                            Code = 400,
                        };

                    }
                    return new ResponseDto
                    {
                        Content = null,
                        Message = "Số Điện Thoại Không Đúng Định Dạng \r " +
                        "định dạng số điện thoại tiếng Việt (10 hoặc 11 chữ số, bắt đầu bằng 0)",
                        Code = 400,
                    };
                }
            }
            catch
            {
                return new ResponseDto
                {
                    Content = null,
                    Message = "Lỗi Hệ Thống",
                    Code = 500,
                };
            }
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto model)
        {
			if (model.NameAccount==null ||model.Password==null)
			{
				return new LoginResponseDto
				{
					Message = "Vui Lòng Nhập Thông Tin !",
					Success = false,
					Code = 405,
				};
			}

			var user = await _userManager.FindByNameAsync(model.NameAccount);

            var passWordVaild = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !passWordVaild)
            {
                return new LoginResponseDto
                {
                    Message = "Thông tin tài khoản không chính xác !",
                    Success = false,
                    Code = 405,
                };
            }
            var authClamis = new List<Claim>
            {
                new Claim(ClaimTypes.Name,model.NameAccount),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            string roleUser = "";
            foreach (var role in userRoles)
            {
                authClamis.Add(new Claim(ClaimTypes.Role, role.ToString()));
                roleUser = role;
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            JwtSecurityToken token = new(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClamis,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
                );

            //return new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Result = user,
                Role = roleUser,
                Success = true,
                Code = 200,
            };
        }


        public async Task<bool> CheckPass(string username, string Pass)
        {
            var user = await _userManager.FindByNameAsync(username);

            var passWordVaild = await _userManager.CheckPasswordAsync(user, Pass);
            if (!passWordVaild == true)
            {
                return false;
            }
            return true;
        }



        public async Task<IdentityResult> SignUpAsync(SignUpDto model)
        {
            var user = new NguoiDung
            {
                MaNguoiDung = GenerateUserIdAsync(model.UserName),
                TenNguoiDung = model.TenNguoiDung,
                UserName = model.UserName,
                Email = model.Email,
                SoDienThoai = model.SDT
            };

            var result = await _userManager.CreateAsync(user, model.PassWord);
            if (result.Succeeded)
            {
                // Kiểm tra role "Admin" đã tồn tại chưa
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    // Nếu chưa tồn tại, tạo mới chức vụ "Admin"
                    var newRole = new ChucVu
                    {
                        Name = "Admin",
                        MaChucVu = "1" + Guid.NewGuid(),
                        TenChucVu = "Quản trị viên",
                        TrangThai = 1 // Set trạng thái mặc định cho chức vụ
                    };
                    await _roleManager.CreateAsync(newRole);
                }

                // Gán chức vụ "Admin" cho người dùng
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return result;
        }


        // Hàm tạo mã người dùng từ tên người dùng
        private string GenerateUserIdAsync(string userName)
        {
            // Lấy danh sách mã người dùng hiện có
            var existingUserIds = _context.NguoiDungs
                                                .Where(u => u.UserName == userName)
                                                .Select(u => u.MaNguoiDung)
                                                .ToList();

            // Tìm chỉ số duy nhất cho mã người dùng mới
            int index = 1;
            string userId = userName.Replace(" ", string.Empty); // Xóa khoảng trắng và ký tự đặc biệt nếu có
            string uniqueUserId = userId;
            while (existingUserIds.Contains(uniqueUserId))
            {
                uniqueUserId = $"{userId}_{index}";
                index++;
            }

            return uniqueUserId;
        }

        public async Task<bool> UpdateUserInfoAndSendVerificationEmail(Guid userId, UpdateUserInfoDto model, string Pass)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var checkPass = await CheckPass(user.UserName, Pass);
            if (user == null || checkPass == false)
            {
                // Không tìm thấy tài khoản hoặc sai mật khẩu
                return false;
            }

            else
            {

                // Kiểm tra xem số điện thoại có đúng định dạng không

                var isValidPhoneNumber = IsValidPhoneNumber(model.SDT);
                if (!isValidPhoneNumber)
                {
                    // Số điện thoại không đúng định dạng
                    return false;
                }


                // Update user information
                //user.TenNguoiDung = model.TenNguoiDung;
                //user.Email = model.Email;
                user.SoDienThoai = model.SDT;
                user.DiaChi = model.DiaChi;


                var updateResult = await _userManager.UpdateAsync(user);// Lưu thay đổi cho người dùng
                if (!updateResult.Succeeded)
                {
                    // Cập nhật thông tin người dùng không thành công
                    return false;
                }

            }
            return true;


        }
        private bool IsValidPhoneNumber(string phoneNumber) // check số điện thoại đúng định dạng
        {
            // định dạng số điện thoại tiếng Việt (10 hoặc 11 chữ số, bắt đầu bằng 0)
            var regex = new Regex(@"^(0\d{9,10})$");

            // Kiểm tra xem số điện thoại có khớp với biểu thức chính quy không
            return regex.IsMatch(phoneNumber);
        }


        //For Admin
        //public async Task<ResponseDto> SignUpNhanVienAsync(SignUpDto model)
        //{



        //    var user = new NguoiDung
        //    {
        //        MaNguoiDung = "1" + Guid.NewGuid(),
        //        TenNguoiDung = model.TenNguoiDung,
        //        UserName = model.TenNguoiDung,
        //        Email = model.Email,
        //        SoDienThoai = model.SDT
        //    };

        //    // Kiểm tra xem số điện thoại có đúng định dạng không

        //    var isValidPhoneNumber = IsValidPhoneNumber(model.SDT);

        //    if (isValidPhoneNumber==true)
        //    {

        //    }

        //    var result = await _userManager.CreateAsync(user, model.PassWord);
        //    if (result.Succeeded)
        //    {
        //        // Kiểm tra role "NhanVien" đã tồn tại chưa
        //        if (!await _roleManager.RoleExistsAsync("NhanVien"))
        //        {
        //            // Nếu chưa tồn tại, tạo mới chức vụ "NhanVien"
        //            var newRole = new ChucVu
        //            {
        //                Name = "NhanVien",
        //                MaChucVu = "1" + Guid.NewGuid(),
        //                TenChucVu = "Nhân Viên",
        //                TrangThai = 1 // Set trạng thái mặc định cho chức vụ
        //            };
        //            await _roleManager.CreateAsync(newRole);
        //        }

        //        // Gán chức vụ "NhanVien" cho người dùng
        //        await _userManager.AddToRoleAsync(user, "NhanVien");
        //    }
        //    return new ResponseDto
        //    {
        //        Message = "",
        //    };
        //}

        public async Task<ResponseDto> SignUpNhanVienAsync(SignUpDto model)
        {
            //// Kiểm tra xem tên người dùng đã tồn tại chưa
            //var existingUser = await _userManager.FindByNameAsync(model.TenNguoiDung);
            //if (existingUser != null)
            //{
            //    return new ResponseDto
            //    {
            //        IsSuccess = false,
            //        Code = 400,
            //        Message = "Tên người dùng đã được sử dụng. Vui lòng chọn tên khác."
            //    };
            //}

            //// Kiểm tra xem email đã tồn tại chưa
            ////existingUser = await _userManager.FindByEmailAsync(model.Email);
            //var existingEMail = _context.NguoiDungs.FirstOrDefault(x => x.Email == model.Email);

            //if (existingEMail != null)
            //{
            //    return new ResponseDto
            //    {
            //        IsSuccess = false,
            //        Code = 400,
            //        Message = "Email đã được sử dụng để đăng ký. Vui lòng sử dụng email khác."
            //    };
            //}

            //// Kiểm tra xem số điện thoại có đúng định dạng không
            //bool isValidPhoneNumber = IsValidPhoneNumber(model.SDT);
            //if (!isValidPhoneNumber)
            //{
            //    return new ResponseDto
            //    {
            //        IsSuccess = false,
            //        Code = 400,
            //        Message = "Số điện thoại không hợp lệ."
            //    };
            //}

            // Tiếp tục thực hiện đăng ký người dùng
            var user = new NguoiDung
            {
                MaNguoiDung = GenerateUserIdAsync(model.TenNguoiDung),
                TenNguoiDung = model.TenNguoiDung,
                UserName = model.UserName,
                Email = model.Email,
                SoDienThoai = model.SDT
            };

            var result = await _userManager.CreateAsync(user, model.PassWord);
            if (result.Succeeded)
            {
                // Kiểm tra role "NhanVien" đã tồn tại chưa
                if (!await _roleManager.RoleExistsAsync("NhanVien"))
                {
                    // Nếu chưa tồn tại, tạo mới chức vụ "NhanVien"
                    var newRole = new ChucVu
                    {
                        Name = "NhanVien",
                        MaChucVu = "1" + Guid.NewGuid(),
                        TenChucVu = "Nhân Viên",
                        TrangThai = 1 // Set trạng thái mặc định cho chức vụ
                    };
                    await _roleManager.CreateAsync(newRole);
                }

                // Gán chức vụ "NhanVien" cho người dùng
                await _userManager.AddToRoleAsync(user, "NhanVien");

                return new ResponseDto
                {
                    Message = "Đăng ký thành công.",
                };
            }
            else
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Đăng ký không thành công. Vui lòng kiểm tra lại thông tin."
                };
            }
        }

		public async Task<ResponseDto> SignUpKhacHangAsync(SignUpDto model)
		{
			// Kiểm tra xem email đã tồn tại chưa
			//existingUser = await _userManager.FindByEmailAsync(model.Email);
			var existingEMail = _context.NguoiDungs.FirstOrDefault(x => x.Email == model.Email);

			if (existingEMail != null)
			{
				return new ResponseDto
				{
					IsSuccess = false,
					Code = 400,
					Message = "Email đã được sử dụng để đăng ký. Vui lòng sử dụng email khác."
				};
			}

            var existingUserName = _context.NguoiDungs.FirstOrDefault(x => x.UserName == model.UserName);

            if (existingUserName != null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "username đã được sử dụng để đăng ký. Vui lòng sử dụng username khác."
                };
            }

            // Tiếp tục thực hiện đăng ký người dùng
            var user = new NguoiDung
			{
				MaNguoiDung = GenerateUserIdAsync(model.TenNguoiDung),
				TenNguoiDung = model.TenNguoiDung,
				UserName = model.UserName,
				Email = model.Email,
				SoDienThoai = model.SDT
			};


			// Kiểm tra mật khẩu
			if (!model.PassWord.Any(char.IsUpper))
			{
				return new ResponseDto
				{
					IsSuccess = false,
					Code = 400,
					Message = "Mật khẩu phải chứa ít nhất một ký tự in hoa."
				};
			}
			if (!model.PassWord.Any(char.IsDigit))
			{
				return new ResponseDto
				{
					IsSuccess = false,
					Code = 400,
					Message = "Mật khẩu phải chứa ít nhất một chữ số."
				};
			}
			if (!model.PassWord.Any(char.IsSymbol) && !model.PassWord.Any(char.IsPunctuation))
			{
				return new ResponseDto
				{
					IsSuccess = false,
					Code = 400,
					Message = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt."
				};
			}

			var result = await _userManager.CreateAsync(user, model.PassWord);
			if (result.Succeeded)
			{
				// Kiểm tra role "NhanVien" đã tồn tại chưa
				if (!await _roleManager.RoleExistsAsync("KhachHang"))
				{
					// Nếu chưa tồn tại, tạo mới chức vụ "NhanVien"
					var newRole = new ChucVu
					{
						Name = "KhachHang",
						MaChucVu = "1" + Guid.NewGuid(),
						TenChucVu = "Khách Hàng",
						TrangThai = 1 // Set trạng thái mặc định cho chức vụ
					};
					await _roleManager.CreateAsync(newRole);
				}

				// Gán chức vụ "NhanVien" cho người dùng
				await _userManager.AddToRoleAsync(user, "KhachHang");

				return new ResponseDto
				{
                    IsSuccess = true,
                    Code = 200,
                    Message = "Đăng ký thành công.",
				};
			}
			else
			{
				return new ResponseDto
				{
					IsSuccess = false,
					Code = 400,
					Message = "Đăng ký không thành công. Vui lòng kiểm tra lại thông tin."
				};
			}
		}

		// For Admin
		public async Task<ResponseDto> XacNhanTaoTkChoNhanVienAsync(SignUpDto model, string maxacnhan, string emailAdmin)
        {


            //var user = await _userManager.FindByEmailAsync(emailAdmin);
            var user1 = _context.NguoiDungs.FirstOrDefault(x => x.Email == emailAdmin);

            if (user1 != null)
            {
                if (user1.VerificationCode == maxacnhan && user1.VerificationCodeExpiry > DateTime.Now)
                {

                    // Kiểm tra xem tên người dùng đã tồn tại chưa
                    var existingUser = await _userManager.FindByNameAsync(model.UserName);
                    if (existingUser != null)
                    {
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            Code = 400,
                            Message = "Tên đăng nhập người dùng đã được sử dụng. Vui lòng nhập tên khác."
                        };
                    }

                    // Kiểm tra xem email đã tồn tại chưa
                    //existingUser = await _userManager.FindByEmailAsync(model.Email);
                    var existingEMail = _context.NguoiDungs.FirstOrDefault(x => x.Email == model.Email);

                    if (existingEMail != null)
                    {
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            Code = 400,
                            Message = "Email đã được sử dụng để đăng ký. Vui lòng sử dụng email khác."
                        };
                    }

                    // Kiểm tra xem số điện thoại có đúng định dạng không
                    bool isValidPhoneNumber = IsValidPhoneNumber(model.SDT);
                    if (!isValidPhoneNumber)
                    {
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            Code = 400,
                            Message = "Số điện thoại không hợp lệ."
                        };
                    }




                    var taotknhanvien = await SignUpNhanVienAsync(model);

                    //if (taotknhanvien.Message== "Tên người dùng đã được sử dụng. Vui lòng chọn tên khác.")
                    //{
                    //    return new ResponseDto
                    //    {
                    //        IsSuccess = false,
                    //        Code = 400,
                    //        Message = "Tên người dùng đã được sử dụng. Vui lòng chọn tên khác."
                    //    };
                    //}

                    //if (taotknhanvien.Message == "Số điện thoại không hợp lệ.")
                    //{
                    //    return new ResponseDto
                    //    {
                    //        IsSuccess = false,
                    //        Code = 400,
                    //        Message = "Số điện thoại không hợp lệ."
                    //    };
                    //} 


                    //if (taotknhanvien.Message == "Email đã được sử dụng để đăng ký. Vui lòng sử dụng email khác.")
                    //{
                    //    return new ResponseDto
                    //    {
                    //        IsSuccess = false,
                    //        Code = 400,
                    //        Message = "Email đã được sử dụng để đăng ký. Vui lòng sử dụng email khác."
                    //    };
                    //}

                    user1.VerificationCode = "";
                    _context.NguoiDungs.Update(user1);
                    await _context.SaveChangesAsync();

                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Code = 200,
                        Message = "Thành công"
                    };
                }
                else return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Mã xác nhận đã được sử dụng hoặc đã hết hạn !"
                };

            }
            else return new ResponseDto
            {
                IsSuccess = false,
                Code = 400,
                Message = "Đăng ký không thành công. Vui lòng kiểm tra lại thông tin.2"
            };


        }


        //public async Task<Task> SendEmailAsync1(/*string _fromMail,string _passFromMail,*/string _Toemail, string? subject, string message)
        //{
        //    subject = "Mã xác thực của bạn là:";
        //    var mail = "sucvathk001@gmail.com";
        //    var pw = "@Chamhet003";
        //    var client = new SmtpClient("smtp-mail.outlook.com", 587)
        //    {
        //        EnableSsl = true,
        //        Credentials = new NetworkCredential(mail, pw)
        //    };
        //    // Create an instance of Random class

        //    var user = _context.NguoiDungs.FirstOrDefault(x => x.Email == _Toemail);

        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    Random random = new Random();

        //    // Generate a random integer between 100000 and 999999 for the authentication code
        //    int authenticationCode = new Random().Next(100000, 999999);

        //    // Set expiration time for the authentication code (e.g., 5 minutes from now)
        //    DateTime expirationTime = DateTime.Now.AddMinutes(10); // Set expiration time to 5 minutes from now
        //                                                          // Lưu mã xác nhận và thời gian hết hạn vào cơ sở dữ liệu
        //    user.VerificationCode = authenticationCode.ToString();
        //    user.VerificationCodeExpiry = expirationTime;
        //    _context.NguoiDungs.Update(user);
        //     _context.SaveChanges();



        //    return client.SendMailAsync(
        //        new MailMessage(from: mail,
        //                          to: _Toemail,
        //                           subject,
        //                             authenticationCode.ToString()
        //                               ));
        //}


        //public async Task<Task> SendEmailAsync2(/*string _fromMail,string _passFromMail,*/string _Toemail, string? subject, string message)
        //{
        //    subject = "Mã xác thực của bạn là:";
        //    var mail = "roryconal099@gmail.com"; //roryconal099@gmail.com
        //    var pw = "@Chamhet03";
        //    var client = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com", 587)
        //    {
        //        EnableSsl = true,
        //        Credentials = new NetworkCredential(mail, pw)
        //    };
        //    // Create an instance of Random class


        //    Random random = new Random();

        //    // Generate a random integer between 100000 and 999999 for the authentication code
        //    int authenticationCode = new Random().Next(100000, 999999);





        //    return client.SendMailAsync(
        //        new MailMessage(from: mail,
        //                          to: _Toemail,
        //                           subject,
        //                             authenticationCode.ToString()
        //                               ));
        //}



        // For Admin
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {

                var user = _context.NguoiDungs.FirstOrDefault(x => x.Email == toEmail);

                if (user == null)
                {
                    return false;
                }
                Random random = new Random();

                // Generate a random integer between 100000 and 999999 for the authentication code
                int authenticationCode = new Random().Next(100000, 999999);

                // Set expiration time for the authentication code (e.g., 5 minutes from now)
                DateTime expirationTime = DateTime.Now.AddMinutes(10); // Set expiration time to 5 minutes from now
                                                                       // Lưu mã xác nhận và thời gian hết hạn vào cơ sở dữ liệu
                user.VerificationCode = authenticationCode.ToString();
                user.VerificationCodeExpiry = expirationTime;
                _context.NguoiDungs.Update(user);
                _context.SaveChanges();

                // Tạo message
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Website Bán Mũ", "roryconal099@gmail.com")); // Thay thế bằng thông tin của bạn
                message.To.Add(new MailboxAddress("", toEmail)); // Không có tên người nhận
                message.Subject = "Mã xác nhận:";

                // Tạo phần nội dung
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "Mã xác nhận của bạn là: " + authenticationCode.ToString();
                message.Body = bodyBuilder.ToMessageBody();

                // Thiết lập kết nối SMTP
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("roryconal099@gmail.com", "radk xkjj ukxx zzbh"); // Thay thế bằng tên người dùng và mật khẩu của bạn
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true; // Gửi email thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false; // Gửi email thất bại
            }
        }


    }
}
