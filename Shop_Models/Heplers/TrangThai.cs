﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Heplers
{
    public class TrangThai
    {
        public enum TrangThaiSale
        {
            [Description("Hết hạn")]
            HetHan = 0,
            [Description("Đang bắt đầu")]
            DangBatDau = 1,
            [Description("Chưa bắt đầu")]
            ChuaBatDau = 2,
            [Description("Buộc dừng")]
            BuocDung = 3
        }
        public enum TrangThaiSaleDetail
        {
            [Description("Ngưng khuyến mãi")]
            NgungKhuyenMai = 0,
            [Description("Đang khuyến mãi")]
            DangKhuyenMai = 1,
            [Description("Đang khuyến mãi")]
            SapKhuyenMai = 2
        }
        public enum TrangThaiCoBan
        {
            [Description("Hoạt động")]
            HoatDong = 1,
            [Description("Không hoạt động")]
            KhongHoatDong = 0,
        }
        public enum TrangThaiSaleInProductDetail
        {
            [Description("Không thể áp dụng sale")]
            KhongApDungSale = 0,
            [Description("Được áp dụng sale")]
            DuocApDungSale = 1,
            [Description("Đã áp dụng sale")]
            DaApDungSale = 2
        }
    }
}
