using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Providers;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProducts _products;

        public HomeController(IProducts products)
        {
            _products = products;
        }

        public IActionResult Index()
        {
            ViewBag.Newsletters = _products.GetAllNewsletter();
            ViewBag.BakeryItems = _products.ItemsListing().Take(2).ToList();
            return View();
        }

        public IActionResult Store()
        {
            ViewBag.Newsletters = _products.GetAllNewsletter();
            ViewBag.BakeryItems = _products.ItemsListing();
            return View();
        }

        [HttpGet]
        public ActionResult Detail(int Id)
        {
            return View(_products.GetItemDetails(Id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Order(UsersOrdersViewModel dto)
        {
            try
            {
                _products.AddItemToBucket(dto, User.Identity.Name);
                return View("Cart");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Cart()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCartItem(int id)
        {
            try
            {
                bool result = _products.DeleteCartItem(id);
                if (result)
                {
                    return Json(new { key = true, value = "Cart Item deleted successfully" });
                }
                else
                {
                    return Json(new { key = false, value = "Unable to find Cart Item Record or Cart Item Record is already deleted" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request please contact to your admin" });
            }
        }

        public ActionResult CartListing()
        {
            return PartialView("_CartListing", _products.GetCostumerCartOrder(User.Identity.Name));
        }


        [Authorize]
        [HttpGet]
        public ActionResult Orders()
        {
            return View("Orders", _products.GetCostumerOrder(User.Identity.Name));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Checkout()
        {
            try
            {
                _products.PerformCheckout(User.Identity.Name);
                return Json(new { key = true, value = "Product Checkout successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request please contact to your admin." });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
