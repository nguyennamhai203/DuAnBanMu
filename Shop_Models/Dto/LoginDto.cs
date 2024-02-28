using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class LoginDto
    {
        [Required]
        //[DataType(DataType.EmailAddress)]
        public string? NameAccount { get; set; }
        [Required]
        //[DataType(DataType.Password)]
        public string? Password { get; set; }
        //[Required]
        //public string? UserName { get; set; }
        //[Required]
        //public string? FullName { get; set; }
    }
}
