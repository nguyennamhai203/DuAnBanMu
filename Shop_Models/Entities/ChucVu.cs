using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    public class ChucVu : IdentityRole<Guid>
    {
        public string? MaChucVu { get; set; }
        public string? TenChucVu { get; set; }
        public int? TrangThai { get; set; }
    }
}
