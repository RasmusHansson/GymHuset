using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        public ActionResult Index()
        {
            List<tbProduct> productList = new List<tbProduct>();

            foreach (tbProduct p in db.tbProducts)
            {
                productList.Add(p);
            }

            return View(productList);
        }
        public ActionResult TestCSS()
        {
            return View();
        }
        public ActionResult Klader(string id)
        {
            return View();
        }

        public ActionResult Kosttillskott(string id)
        {
            return View();
        }

        public ActionResult Search(string searchString)
        {
            return View();
        }

     
     

    }
}
