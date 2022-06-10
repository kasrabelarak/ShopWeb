using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class ProductController : Controller
    {
        MyContext context = new MyContext();
        
        public IActionResult Index(string name, string categoryname,double price)
        {
             
            var products = context.Products.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(a => a.Name.StartsWith(name)).ToList();
            }
            if (!string.IsNullOrEmpty(categoryname))
            {
                products = products.Where(a => a.Category==categoryname).ToList();
            }
            if (price != 0)
            {
                products = products.Where(a => a.Price <price).ToList();
            }
            

            return View(products);
        }
        public IActionResult Add()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("index");
               
        }


        public IActionResult Edit(int id)
        {

            ViewBag.item = context.Products.Find(id);

            return View();
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult Delete(int id)
        {
            context.Products.Remove(new Product() { ProductId = id });
            context.SaveChanges();
            return RedirectToAction("index");
        }














    }
}