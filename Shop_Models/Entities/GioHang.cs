using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("GioHang")]
    public class GioHang
    {
        [Key]
        public Guid IdNguoiDung{ get; set; }
        public int TrangThai{ get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }
    }
}
