using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Anh")]
    public class Anh
    {
        [Key]
        public Guid Guid { get; set; }
        public Guid ChiTietSanPhamId { get; set; }
        public string? MaAnh { get; set; }
        public string? URL { get; set; }
        public virtual ChiTietSanPham? ChiTietSanPham { get; set; }

    }
}
