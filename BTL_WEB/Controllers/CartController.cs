using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_WEB.Models;
namespace BTL_WEB.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index(List<CHITIETDONHANG> sanPhams)
        {
            return View(sanPhams);
        }
        [HttpPost]
        public ActionResult LoadSanPham(List<CHITIETDONHANG> sanPhams)
        {
            return PartialView("_LoadSanPhamRow", sanPhams);
        }

        public ActionResult ThongTinSanPham(int idSanPham, int SoLuong)
        {
            ViewBag.SoLuong = SoLuong;
            return PartialView("_SanPhamRow", new mapSanPham().ChiTiet(idSanPham));
        }

    }
}