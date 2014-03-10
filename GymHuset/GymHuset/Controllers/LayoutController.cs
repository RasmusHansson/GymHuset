using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GymHuset.Models;

namespace GymHuset.Controllers
{
    public class LayoutController : Controller
    {
      
        DataClasses1DataContext db = new DataClasses1DataContext();

        //Visar hur många varor det är i kundkorgen+total kostnad
        public ActionResult _CartSmall()
        {
           
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
                ViewBag.cartSum = ((List<tbProduct>) Session["cartList"]).Sum(prod => prod.iPrice * prod.iCount) + ":-";
               
                return View();
            }
            ViewBag.cartCount = "0";
            ViewBag.cartSum = "0:-";
            return View();
        }
        public ActionResult _PopProductsPartial()
        {
          
            //Välj ut alla produkter i databasen och sortera på antal sålda produkter, flest -> minst
            List<tbProduct> productList = (from p in db.tbProducts
                                           select p).OrderByDescending(p => p.iItemsSold).ToList();


            List<tbProduct> popularList = new List<tbProduct>();

            //Plocka ut de fem högst säljande produkterna och lägg i en ny lista 
            for (int i = 0; i < 5; i++)
            {
                popularList.Add((productList).First());
                productList.Remove((productList).First());
            }

            //Skicka med listan på top 5 populära produkter till vyn Shared/_PopProductsPartial.cshtml
            return PartialView(popularList);
        }

      

    }
}
