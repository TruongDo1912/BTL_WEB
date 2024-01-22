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
        public ActionResult Index()
        {
            List<SANPHAM> danhSachSanPham = db.SANPHAMs.Take(8).ToList();
            return View(danhSachSanPham);
        }
       
    }
}