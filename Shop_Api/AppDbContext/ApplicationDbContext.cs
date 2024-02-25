using Shop_Models.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Shop_API.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }
        public virtual DbSet<Khuyenmai> KhuyenMais { get; set; }


    }
}
