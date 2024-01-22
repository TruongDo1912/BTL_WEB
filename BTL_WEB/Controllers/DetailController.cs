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
        BANDONGHOEntities db = new BANDONGHOEntities();
        // GET: Detail
        public ActionResult Index(int id)
        {
            SANPHAM ketQua = db.SANPHAMs.Find(id);
            return View(ketQua);
        }
    }
}