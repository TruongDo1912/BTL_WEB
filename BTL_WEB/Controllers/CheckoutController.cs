using BTL_WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_WEB.Controllers
{  
    public class CheckoutController : Controller
    {
        // GET: Checkout

        private BANDONGHOEntities db = new BANDONGHOEntities();
      
        public ActionResult Index()
        {
            /* List<ProductViewModel> products = new List<ProductViewModel>();
             foreach (var item in dataFromLocalStorage)
             {
                 var product = db.SANPHAMs.FirstOrDefault(p => p.MASP == item.ID);

                 if (product != null)
                 {
                     var viewModel = new ProductViewModel
                     {
                         Product = product,
                         Quantity = item.Quality
                     };

                     products.Add(viewModel);
                 }
             }*/
            return View();
        }
        [HttpPost]
        public ActionResult Index(List<LocalStorageItem> dataFromLocalStorage)
            {
            Console.WriteLine("Data from LocalStorage: " + JsonConvert.SerializeObject(dataFromLocalStorage));
            List<ProductViewModel> products = new List<ProductViewModel>();
            foreach (var item in dataFromLocalStorage)
            {
                var product = db.SANPHAMs.FirstOrDefault(p => p.MASP == item.ID);
                
                if (product != null)
                {
                    var viewModel = new ProductViewModel
                    {
                        Product = product,
                        Quantity = item.Quality
                    };

                    products.Add(viewModel);
                }
            }
            return View(products);
        }

    }
}