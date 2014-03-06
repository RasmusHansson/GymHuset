﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
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
            if (Session["cartList"] == null)
            {
                return View("EmptyCart");
            }
            else if (((List<tbProduct>) Session["cartList"]).Count == 0)
            {
                return View("EmptyCart");
            }
            
            
                var cartList = (List<tbProduct>) Session["cartList"];
                return View(cartList);
            
        }
      
        //Tar bort produkt från kundkorgen
        public ActionResult CartRemove(int? id)
        {
          
                ((List<tbProduct>)Session["cartList"]).RemoveAll(c => c.iID == id);
           
            return RedirectToAction("Kundkorg");
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
