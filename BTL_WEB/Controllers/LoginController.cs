using BTL_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_WEB.Controllers
{
    public class LoginController : Controller
    {
        private BANDONGHOEntities db = new BANDONGHOEntities();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["nameuser"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View("~/Views/Dashboard/Index.cshtml");
            }

        }
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TAIKHOAN tk, string tennd, string matkhau)
        {
            // Lấy đối tượng TAIKHOAN từ cơ sở dữ liệu dựa trên tên đăng nhập và mật khẩu
            var user = db.TAIKHOANs.SingleOrDefault(m => m.TENDN.ToLower() == tennd.ToLower() && m.MATKHAU.ToLower() == matkhau.ToLower());

            if (user != null)
            {
                // Kiểm tra điều kiện trang thái và loại tài khoản
                if (user.TRANGTHAI == true && user.MALOAITK == "LK00002")
                {
                    Session["nameuser"] = tennd;
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["erorr"] = "Tài khoản không có quyền đăng nhập!";
                    return View("Login", tk);
                }
            }
            else
            {
                TempData["erorr"] = "Tên người dùng hoặc mật khẩu không chính xác!";
            }

            // Trả về view login với model tk (nếu cần)
            return View(tk);
        }
        public ActionResult Logout()
        {
            Session.Remove("nameuser");
            return View("~/Views/Login/Login.cshtml");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(TAIKHOAN tk)
        {
            if (ModelState.IsValid)
            {
                if (db.TAIKHOANs.Any(m => m.TENDN == tk.TENDN))
                {
                    TempData["erorr1"] = "Tên người dùng đã tồn tại";
                    return View(tk);
                }
                tk.MALOAITK = "LK00002";
                tk.TRANGTHAI = true;
                tk.NGAYDANGKY = DateTime.Now;
                db.TAIKHOANs.Add(tk);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return View(tk);
        }

        public ActionResult ActionRes()
        {
            return View();
        }
    }
}