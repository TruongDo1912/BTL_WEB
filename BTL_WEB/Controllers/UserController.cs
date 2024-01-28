using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_WEB.Models;

namespace BTL_WEB.Controllers
{
    public class UserController : Controller
    {
        private BANDONGHOEntities db = new BANDONGHOEntities();
        // GET: User
        public ActionResult Index(string loai)
        {
            // Lấy danh sách sản phẩm từ database
            List<SANPHAM> danhSachSanPham = db.SANPHAMs.Take(8).ToList();

            // Lấy danh sách giá của seller
            var sellerPrices = db.SANPHAMs.Where(sp => sp.MALOAISP == "LP00003").ToDictionary(sp => sp.MASP, sp => sp.GIASELLER);

            // Cập nhật giá của seller trong danh sách sản phẩm
            foreach (var item in danhSachSanPham)
            {
                int? maKM = item.MAKM;

                if (maKM.HasValue)
                {
                    // Lấy thông tin khuyến mãi từ bảng KHUYENMAI
                    KHUYENMAI khuyenMai = db.KHUYENMAIs.FirstOrDefault(km => km.MAKM == maKM);

                    if (khuyenMai != null && khuyenMai.PHAMTRAM.HasValue)
                    {
                        // Áp dụng giảm giá từ bảng KHUYENMAI
                        item.GIASELLER *=  (1 - khuyenMai.PHAMTRAM.Value / 100);
                    }
                }
            }

            // Nếu có loại sản phẩm được chọn, lọc danh sách theo loại
            if (!string.IsNullOrEmpty(loai))
            {
                danhSachSanPham = danhSachSanPham
                    .Where(s => s.LOAISANPHAM.TENLOAISP == loai)
                    .ToList();
            }

            return View(danhSachSanPham);
        }


    }
}