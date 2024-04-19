using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Shop_Models.Heplers;
using System.Xml.Linq;
using System;
using System.Data.Entity;

namespace Shop_Api.Repository
{
    public class ThongKeViewModelRepository : IThongKeViewModelRepository
    {
        ApplicationDbContext context;
        public ThongKeViewModelRepository(ApplicationDbContext ct)
        {
            context = ct;
        }

        public async Task<List<ThongKeViewModel>> ThongKeSanPham()
        {
            try
            {
                var result = await context.ThongKeViewModels.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thực hiện thống kê sản phẩm theo ngày: {ex.Message}");
            }
        }


        public Task<List<ThongKeViewModel>> ThongKeSanPhamTheoNam(int nam)
        {
            // ghi nhớ đây chỉ lớp repository sinh từ interface chưa phải api controller
            // gợi ý về thống kê sản phẩm theo năm thanh toán của hóa đơn
            // trên controller tôi muốn nhập dữ liệu năm có sẵn trong databasse
            // sau khi nhập năm thanh toán tôi muốn hiển thị các sản phẩm theo năm đó
            throw new NotImplementedException();
        }

        public async Task<List<ThongKeViewModel>> ThongKeSanPhamTheoNgay(int ngay)
        {
            // ghi nhớ đây chỉ lớp repository sinh từ interface chưa phải api controller
            // gợi ý về thống kê sản phẩm theo ngày thanh toán của hóa đơn
            // trên controller tôi muốn nhập dữ liệu ngày có sẵn trong databasse
            // sau khi nhập ngày thanh toán tôi muốn hiển thị các sản phẩm theo ngày đó
            throw new NotImplementedException();
        }

        public Task<List<ThongKeViewModel>> ThongKeSanPhamTheoThang(int thang)
        {
            // ghi nhớ đây chỉ lớp repository sinh từ interface chưa phải api controller
            // gợi ý về thống kê sản phẩm theo tháng thanh toán của hóa đơn
            // trên controller tôi muốn nhập dữ liệu tháng có sẵn trong databasse
            // sau khi nhập tháng thanh toán tôi muốn hiển thị các sản phẩm theo tháng đó
            throw new NotImplementedException();
        }
    }
}