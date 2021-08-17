using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class ManageController : Controller
    {
        private readonly IProducts _products;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ManageController(IProducts products, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _products = products;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateItem()
        {
            ViewBag.message = "Add New Product";
            return View(new MenuItemsViewModel());
        }

        public IActionResult EditItem(int Id)
        {
            ViewBag.message = "Update Product";
            return View("CreateItem", _products.EditItem(Id));
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Products()
        {
            return View(_products.ItemsListing());
        }

        [HttpPost("UploadFiles")]
        public ActionResult AddUpdateItem(MenuItemsViewModel dto)
        {
            try
            {
                if (dto.Id != 0)
                {
                    _products.UpdateItem(dto, _hostingEnvironment);
                    return Json(new { key = true, value = "Product updated successfully" });
                }
                else
                {
                    _products.AddProducts(dto, _hostingEnvironment);
                    return Json(new { key = true, value = "Product added Successfully" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request please contact to your admin." });
            }
        }

        public ActionResult DeleteItem(int id)
        {
            try
            {
                bool result = _products.DeleteItem(id);
                if (result)
                {
                    return Json(new { key = true, value = "Product deleted successfully" });
                }
                else
                {
                    return Json(new { key = false, value = "Unable to find Product Record or Product Record is already deleted" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request please contact to your admin" });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Orders()
        {
            return View("Orders", _products.GetAllOrders());
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Newsletter()
        {
            return View(_products.GetAllNewsletter());
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult AddNewsLetter()
        {
            ViewBag.message = "Add New Newsletter";
            return View(new Newsletter());
        }

        [HttpPost]
        public ActionResult AddUpdateNewsLetter(Newsletter dto)
        {
            try
            {
                if (dto.Id != 0)
                {
                    _products.UpdateNewsLetter(dto);
                    return Json(new { key = true, value = "Newsletter updated successfully" });
                }
                else
                {
                    _products.AddNewsLetter(dto);
                    return Json(new { key = true, value = "Newsletter added Successfully" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request please contact to your admin." });
            }
        }
        public IActionResult EditNewsletter(int Id)
        {
            ViewBag.message = "Update Newsletter";
            return View("AddNewsLetter", _products.EditNewsletter(Id));
        }

        public ActionResult DeleteNewsletter(int id)
        {
            try
            {
                bool result = _products.DeleteNewsLetter(id);
                if (result)
                {
                    return Json(new { key = true, value = "Newsletter deleted successfully" });
                }
                else
                {
                    return Json(new { key = false, value = "Unable to find Newsletter Record or Newsletter Record is already deleted" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request please contact to your admin" });
            }
        }
    }
}