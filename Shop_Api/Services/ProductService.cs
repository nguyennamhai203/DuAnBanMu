using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Models.Heplers;
using static Shop_Models.Heplers.TrangThai;

namespace Shop_Api.Services
{

    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
            // Khởi tạo Timer với thời gian là 5 phút (300,000 milliseconds)
        }

        public void UpdatePromotionStatusAndProductPrice()
        {
            // 1. Kiểm tra và cập nhật trạng thái của khuyến mãi
            var activePromotions = _context.KhuyenMais.ToList();

            var allCTKM = _context.ChiTietKhuyenMais.ToList();

            foreach (var promotion in activePromotions)
            {
                if (promotion.TrangThai != (int)TrangThai.TrangThaiSale.BuocDung)
                {
                    var ProductsInSale = allCTKM.Where(x => x.KhuyenMaiId == promotion.Id).ToList();


                    if (promotion.NgayKetThuc < DateTime.Now)
                    {
                        // Nếu thời gian hiện tại đã vượt quá thời gian kết thúc của khuyến mãi, cập nhật trạng thái của khuyến mãi
                        promotion.TrangThai = 0;

                        _context.ChiTietKhuyenMais.RemoveRange(ProductsInSale);

                        foreach (var item in ProductsInSale)
                        {
                            var ProductDetails = _context.ChiTietSanPhams.FirstOrDefault(x => x.Id == item.ChiTietSanPhamId);
                            if (ProductDetails != null)
                            {
                                ProductDetails.TrangThaiKhuyenMai = (int)TrangThaiSaleInProductDetail.DuocApDungSale;
                                ProductDetails.GiaBan = ProductDetails.GiaThucTe;
                            }

                        }
                    }
                    else if (promotion.NgayBatDau <= DateTime.Now && promotion.NgayKetThuc >= DateTime.Now)
                    {
                        // Nếu thời gian hiện tại nằm trong khoảng thời gian của khuyến mãi, cập nhật trạng thái của khuyến mãi
                        promotion.TrangThai = 1;
                        foreach (var item in ProductsInSale)
                        {
                            var ProductDetails = _context.ChiTietSanPhams.FirstOrDefault(x => x.Id == item.ChiTietSanPhamId);
                            if (ProductDetails != null)
                            {
                                ProductDetails.TrangThaiKhuyenMai = (int)TrangThaiSaleInProductDetail.DaApDungSale;
                            }

                        }
                    }




                    _context.SaveChanges(); // Lưu thay đổi trạng thái của khuyến mãi
                }
            }

            // 2. Cập nhật giá sản phẩm
            var productsToUpdate = _context.ChiTietSanPhams
                .Where(p =>/* p.TrangThai == 1 &&*/ p.TrangThaiKhuyenMai == (int)TrangThaiSaleInProductDetail.DaApDungSale)
                .ToList();

            foreach (var product in productsToUpdate)
            {
                var CTKM = _context.ChiTietKhuyenMais.FirstOrDefault(x => x.ChiTietSanPhamId == product.Id);
                if (CTKM != null)
                {
                    var currentPromotion = _context.KhuyenMais.FirstOrDefault(x => x.Id == CTKM.KhuyenMaiId && x.TrangThai == 1);

                    if (currentPromotion != null)
                    {
                        // Kiểm tra xem sản phẩm có nằm trong danh sách sản phẩm của khuyến mãi không
                        var isInPromotion = currentPromotion.ChiTietKhuyenMais.Any(p => p.ChiTietSanPhamId == product.Id);
                        if (isInPromotion)
                        {
                            // Cập nhật giá sản phẩm dựa trên loại hình khuyến mãi
                            if (currentPromotion.LoaiHinhKhuyenMai == "Khuyến mại giảm giá")
                            {
                                product.GiaBan = product.GiaBan - (product.GiaBan * currentPromotion.MucGiam / 100);
                            }
                            else if (currentPromotion.LoaiHinhKhuyenMai == "Khuyến mại đồng giá")
                            {
                                product.GiaBan = currentPromotion.MucGiam;
                            }
                        }

                    }
                    else
                    {
                        // Nếu sản phẩm không nằm trong danh sách sản phẩm của khuyến mãi, cập nhật giá sản phẩm về giá thực tế
                        product.GiaBan = product.GiaThucTe;
                    }

                    if (product.TrangThai == 0)
                    {
                        product.TrangThaiKhuyenMai = 0;
                        product.GiaBan = product.GiaThucTe;

                        _context.Remove(CTKM);

                    }
                }
                else
                {
                    // Nếu sản phẩm không nằm trong danh sách sản phẩm của khuyến mãi, cập nhật giá sản phẩm về giá thực tế
                    product.GiaBan = product.GiaThucTe;
                    product.TrangThaiKhuyenMai = (int)TrangThaiSaleInProductDetail.DuocApDungSale;
                }
            }

            _context.SaveChanges(); // Lưu tất cả các thay đổi sau khi đã cập nhật trạng thái và giá của sản phẩm
        }



    }
}


