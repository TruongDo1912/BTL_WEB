using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_WEB.Models
{
    public class mapSanPham
    {
        public SANPHAM ChiTiet(int id)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.SANPHAMs.Find(id);
        }
    }
}