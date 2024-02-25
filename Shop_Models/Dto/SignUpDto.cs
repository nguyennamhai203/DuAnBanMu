using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class SignUpDto
    {
        //[Required]
        //public string FirstName { get; set; } = null!; 
        //[Required]
        //public string LastName { get; set; } = null!;

        [Required]
        public string? TenNguoiDung { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; } 
        [Required]
        public string? UserName { get; set; } 
        [Required]
        public string? PassWord { get; set; } 
        [Required]
        public string? ConfirmPassWord { get; set; } 
    }
}
