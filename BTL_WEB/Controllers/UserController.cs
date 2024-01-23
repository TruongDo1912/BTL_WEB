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
            List<SANPHAM> danhSachSanPham = db.SANPHAMs.Take(8).ToList();
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