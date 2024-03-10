using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class LoginResponseDto
    {
        public string? Token { get; set; } = null;
        public object? Result { get; set; } = null;
        public string? Role { get; set; }
        public int? Code { get; set; } = 200;
        public string? Message { get; set; } = "Đăng Nhập Thành Công";
        public bool? Success { get; set; } = true;

    }
}
