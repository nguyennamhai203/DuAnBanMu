using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class FavoriteStatus
    {
        public bool IsFavorite { get; set; } // Trạng thái yêu thích của sản phẩm
        //true nếu sản phẩm đã được người dùng yêu thích, ngược lại là false
        public int FavoriteCount { get; set; } // Số lượng người dùng yêu thích sản phẩm
    }
}