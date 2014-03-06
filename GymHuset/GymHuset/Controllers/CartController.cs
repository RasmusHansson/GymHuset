using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymHuset.Models;

namespace GymHuset.Controllers
{
    public class CartController : Controller
    {
        
        DataClasses1DataContext db = new DataClasses1DataContext();
        List<tbProduct> basket = new List<tbProduct>();

        //Kundkorg.cshtml, listar vad som finns i kundkorgen.
        public ActionResult Kundkorg()
        {
            var cartList = (List<tbProduct>)Session["cartList"];
            return View(cartList);
        }
      
        //Tar bort produkt från kundkorgen
        public ActionResult CartRemove(int? id)
        {
            foreach (var r in ((List<tbProduct>)Session["cartList"]).Where(c => c.iID == id))
            {
                ((List<tbProduct>)Session["cartList"]).Remove(r);
            }
            return RedirectToAction("CartAdd");
        }
        //Lägger till produkt i kundkorgen
        public ActionResult CartAdd(int? id)
        {
        

            var findProduct = (from f in db.tbProducts.Where(c => c.iID == id) select f).FirstOrDefault();
            if (Session["cartList"] == null)
            {
                Session["cartList"] = basket;
            }
            ((List<tbProduct>)Session["cartList"]).Add(findProduct);
            ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Count;

            return RedirectToAction("Index", "Produkter");
        }

    }
}
