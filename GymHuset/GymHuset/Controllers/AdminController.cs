using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Messaging;
using GymHuset.Models;

namespace GymHuset.Controllers
{

    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        DataClasses1DataContext db = new DataClasses1DataContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminSidan()
        {
            return View();
        }

        public ActionResult AdderaProdukt(string ProductName, string ProductPrice, string ProductQuantity, string ProductDesc, string ProductType)
        {
            bool status = false; // Meddelande indikator
            List<tbProduct> products = getProducts();

            try
            {
                if (ProductName != null)
                {
                    if (ProductPrice == null)
                    {
                        ViewBag.status = "Du måste ange ett pris.";
                        status = true;
                    }
                    if (ProductQuantity == null)
                    {
                        ViewBag.status = "Ange lagersaldo på produkt.";
                        status = true;
                    }
                    if (ProductDesc == null || ProductDesc == "")
                    {
                        ProductDesc = "";
                    }
                    if (!status)
                        foreach (var f in db.tbProducts)
                            if (f.sName.ToLower() == ProductName.ToLower())
                            {
                                status = true;
                                @ViewBag.status = "Produkten finns redan, Välj ett annat namn.";
                            }

                    if (!status)
                    {
                        tbProduct product = new tbProduct
                        {
                            sName = ProductName,
                            sDescription = ProductDesc,
                            iPrice = int.Parse(ProductPrice),
                            iStockBalance = int.Parse(ProductQuantity),
                            iProductType = int.Parse(ProductType)
                        };
                        db.tbProducts.InsertOnSubmit(product);
                        db.SubmitChanges();
                        ViewBag.status = "Du har nu lagt till " + ProductName + " till produktlistan";
                        status = true;
                    }
                }
            }
            catch 
            {
                ViewBag.status = "Du måste fylla i alla fält korrekt.";
                status = true;
                return View();
            }
            return View();
        }

        public ActionResult TaBortProdukt(string id)        // int borde användas som id
        {
            List<tbProduct> productlista = new List<tbProduct>();
            productlista = (from f in db.tbProducts
                            select f).ToList();

            var product2Delete = (from f in db.tbProducts               
                                  where f.sName == id
                                  select f).FirstOrDefault();

            if (id != null)
            {
                db.tbProducts.DeleteOnSubmit(product2Delete);
                db.SubmitChanges();

                List<tbProduct> productlista2 = new List<tbProduct>();
                productlista2 = (from f in db.tbProducts
                                 select f).ToList();

                return View(productlista2);
            }
            return View(productlista);
        }

        private List<tbProduct> getProducts()
        {
            List<tbProduct> products = new List<tbProduct>();
            products = (from f in db.tbProducts
                       select f).ToList();
            return products;
        }



    }
}
