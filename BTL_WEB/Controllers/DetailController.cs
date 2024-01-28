using BTL_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_WEB.Controllers
{
    public class DetailController : Controller
    {
        private BANDONGHOEntities db = new BANDONGHOEntities();

        // GET: Detail
        public ActionResult Index(int id)
        {
            SANPHAM ketQua = db.SANPHAMs.Find(id);

            double giaHienThi = 0;

            // Kiểm tra xem sản phẩm có phải là sản phẩm seller không
            if (ketQua.MALOAISP == "LP00003" && ketQua.MAKM.HasValue)
            {
                // Lấy thông tin khuyến mãi từ bảng KHUYENMAI
                KHUYENMAI khuyenMai = db.KHUYENMAIs.FirstOrDefault(km => km.MAKM == ketQua.MAKM);

                if (khuyenMai != null && khuyenMai.PHAMTRAM.HasValue)
                {
                    // Lấy giá seller nếu là sản phẩm seller, ngược lại sử dụng giá gốc
                    giaHienThi = ketQua.DONGIA ?? 0;
                    giaHienThi *= (1 - khuyenMai.PHAMTRAM.Value / 100);
                }
            }
            else
            {
                // Sử dụng giá gốc nếu không phải là sản phẩm seller
                giaHienThi = ketQua.DONGIA ?? 0;
            }

            ViewBag.GiaHienThi = giaHienThi;
            var sanPhamLienQuan = db.SANPHAMs
            .Where(sp => sp.LOAISANPHAM.TENLOAISP == ketQua.LOAISANPHAM.TENLOAISP && sp.MASP != id)
            .Take(5)
            .ToList();

            ViewBag.SanPhamLienQuan = sanPhamLienQuan;

            return View(ketQua);
        }
    }
}