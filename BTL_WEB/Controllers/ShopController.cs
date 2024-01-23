using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_WEB.Models;
using PagedList;

namespace BTL_WEB.Controllers
{
    public class ShopController : Controller
    {
        private BANDONGHOEntities db = new BANDONGHOEntities();

        // GET: Shop
        public ActionResult Index(int? page, string keyword,string loai)
        {
            int pageSize = 6; // Số sản phẩm hiển thị trên mỗi trang
            int pageNumber = (page ?? 1);
            // Lấy danh sách sản phẩm từ database
            List<SANPHAM> danhSachSanPham = db.SANPHAMs.ToList();

            // Nếu có từ khóa tìm kiếm, lọc danh sách theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                danhSachSanPham = danhSachSanPham
                //StringComparison.OrdinalIgnoreCase : hàm so sánh chữ hoa với chữ thường
                .Where(s => s.TENSP.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 || s.TENSP.Any(c => c.ToString().IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();
            }
            if (!string.IsNullOrEmpty(loai))
            {
                danhSachSanPham = danhSachSanPham
                    .Where(s => s.LOAISANPHAM.TENLOAISP == loai)
                    .ToList();
            }
            // Phân Trang
            IPagedList<SANPHAM> pagedList = danhSachSanPham.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        public ActionResult XemTheoLoai(string TenLoai)
        {
            ViewBag.TenLoai = TenLoai;
            return View();
        }
    }
}