using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Shop_Models.Entities;

namespace AdminApp.Controllers
{
    public class ExportFileExcel : Controller
    {
        public async Task<IActionResult> ExportToExcel()
        {
            // Check if ViewData.Model is not null and is of the correct type
            if (ViewData.Model is IEnumerable<ChiTietSanPham> data)
            {
                // Tạo một package Excel
                ExcelPackage excelPackage = new ExcelPackage();
                var worksheet = excelPackage.Workbook.Worksheets.Add("Thống kê doanh thu");

                // Thêm tiêu đề cột
                string[] headers = { "Mã sản phẩm", "Giá Nhập", "Giá bán", "Giá thực tế", "Số lượng tồn", "Số lượng đã bán", "Tổng doanh thu", "Lãi", "Lỗ" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                // Thêm dữ liệu từ view vào Excel
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.MaSanPham;
                    worksheet.Cells[row, 2].Value = item.GiaNhap;
                    worksheet.Cells[row, 3].Value = item.GiaBan;
                    worksheet.Cells[row, 4].Value = item.GiaThucTe;
                    worksheet.Cells[row, 5].Value = item.SoLuongTon;
                    worksheet.Cells[row, 6].Value = item.SoLuongDaBan;

                    decimal revenue = Convert.ToDecimal(item.GiaBan * item.SoLuongDaBan);
                    decimal profit = Convert.ToDecimal((item.GiaBan - item.GiaNhap) * item.SoLuongDaBan);
                    decimal loss = Convert.ToDecimal((item.GiaNhap - item.GiaBan) * item.SoLuongDaBan);

                    worksheet.Cells[row, 7].Value = revenue;
                    worksheet.Cells[row, 8].Value = profit >= 0 ? profit : 0;
                    worksheet.Cells[row, 9].Value = profit < 0 ? profit : 0;

                    row++;
                }

                // Xuất file Excel
                var stream = new MemoryStream();
                excelPackage.SaveAs(stream);
                stream.Position = 0;

                // Trả về file Excel như một file download
                return File(stream, "\"C:\\Users\\admin\\Desktop\\file\"", "ThongKeDoanhThu.xlsx");
            }
            else
            {
                // Handle the case where ViewData.Model is null or not of the correct type
                // Return an appropriate response, such as a view with an error message
                return NotFound();
            }
        }
    }
}