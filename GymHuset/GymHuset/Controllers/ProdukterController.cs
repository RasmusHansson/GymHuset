using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using GymHuset.Models;

namespace GymHuset.Controllers
{
    public class ProdukterController : Controller
    {
        //
        // GET: /Produkter/

        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult Index(string searchString)
        {
            var productList = db.tbProducts.ToList();

            if (searchString != null)
            {
                productList = (from prod in db.tbProducts.Where(c => c.sName.Contains(searchString)) select prod).ToList();
                return View(productList);
            }

            return View(productList);
        }
        public ActionResult TestCSS()
        {
            return View();
        }

        public ActionResult ProduktInfo(int id)
        {
            var singleProduct = from s in db.tbProducts.Where(c => c.iID == id) select s;

            return View(singleProduct);
        }

        public ActionResult TypeOfProduct(int id)
        {
            var typeList = db.tbProducts.Where(c => c.iProductType == id).ToList();
            return View("Index", typeList);


        }

    }
}
