using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Providers
{

    public class ProductsProvider : IProducts
    {
        private readonly IUserProvider _userprovider;
        private readonly ApplicationDbContext _context;
        public ProductsProvider(ApplicationDbContext context, IUserProvider userProvider)
        {
            _context = context;
            _userprovider = userProvider;
        }

        public void AddProducts(MenuItemsViewModel dto, IHostingEnvironment _hostingEnvironment)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;

            string _imageUrl = "";
            if (dto.Image != null && dto.Image.Any())
            {
                var PathWithFolderName = System.IO.Path.Combine(webRootPath, "Uploads");
                if (!Directory.Exists(PathWithFolderName))
                {
                    DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
                }

                foreach (var img in dto.Image)
                {
                    string fname = "";
                    // Checking for Internet Explorer  
                    fname = img.FileName;
                    var myUniqueFileName = Guid.NewGuid().ToString();
                    fname = myUniqueFileName + "-" + fname;
                    _imageUrl = "/Uploads/" + fname;

                    var _filePath = Path.Combine(PathWithFolderName, fname);
                    using (var fileStream = new FileStream(_filePath, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                }
            }
            Products item = new Products()
            {
                ItemName = dto.ItemName,
                Description = dto.Description,
                ImageUrl = _imageUrl,
                Price = Convert.ToDecimal(dto.Price)
            };

            _context.Products.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(MenuItemsViewModel dto, IHostingEnvironment _hostingEnvironment)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var item = _context.Products.Find(dto.Id);
            string _imageUrl = "";
            if (dto.Image != null && dto.Image.Any())
            {
                var PathWithFolderName = System.IO.Path.Combine(webRootPath, "Uploads");
                if (!Directory.Exists(PathWithFolderName))
                {
                    DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
                }

                foreach (var img in dto.Image)
                {
                    string fname = "";
                    // Checking for Internet Explorer  
                    fname = img.FileName;
                    var myUniqueFileName = Guid.NewGuid().ToString();
                    fname = myUniqueFileName + "-" + fname;
                    _imageUrl = "/Uploads/" + fname;

                    var _filePath = Path.Combine(PathWithFolderName, fname);
                    using (var fileStream = new FileStream(_filePath, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                    if (item != null)
                    {
                        item.ImageUrl = _imageUrl;
                    }
                }
            }


            if (item != null)
            {
                item.ItemName = dto.ItemName;
                item.Description = dto.Description;
                item.Price = Convert.ToDecimal(dto.Price);
                _context.SaveChanges();
            }
        }

        public MenuItemsViewModel EditItem(int id)
        {

            MenuItemsViewModel item = new MenuItemsViewModel();
            var data = _context.Products.Find(id);
            if (data != null)
            {
                item.ItemName = data.ItemName;
                item.Description = data.Description;
                item.Price = data.Price.ToString();
                item.Id = data.Id;
            }
            return item;
        }

        public List<MenuItemsViewModel> ItemsListing()
        {
            List<MenuItemsViewModel> list = _context.Products.OrderByDescending(x => x.Id).AsEnumerable().Select(x => new MenuItemsViewModel
            {
                ItemName = x.ItemName,
                Id = x.Id,
                Description = x.Description,
                imageurl = !string.IsNullOrEmpty(x.ImageUrl) ? x.ImageUrl : "../assets/images/noimage.jpg",
                Price = x.Price.ToString()
            }).ToList();
            return list;
        }

        public bool DeleteItem(int id)
        {
            var item = _context.Products.Find(id);
            if (item != null)
            {
                _context.Products.Remove(item);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public MenuItemsViewModel GetItemDetails(int ItemId)
        {
            return _context.Products.Where(x => x.Id == ItemId).AsEnumerable().Select(x => new MenuItemsViewModel
            {
                ItemName = x.ItemName,
                Id = x.Id,
                Description = x.Description,
                imageurl = !string.IsNullOrEmpty(x.ImageUrl) ? x.ImageUrl : "../assets/img/noimage.jpg",
                Price = x.Price.ToString()
            }).FirstOrDefault();
        }
        public void AddItemToBucket(UsersOrdersViewModel dto, string email)
        {
            int _ItemId = dto.Id;
            var _foodItem = _context.Products.Find(_ItemId);
            Decimal price = _foodItem.Price;
            string _UserID = _userprovider.GetUserId(email);
            Cart order = new Cart()
            {
                OrderDate = DateTime.Now,
                Price = price,
                ProductId = _ItemId,
                UserId = _UserID,
                Quantity = dto.Quantity,
            };

            _context.Cart.Add(order);
            _context.SaveChanges();
        }

        public List<UsersOrdersViewModel> GetCostumerOrder(string email)
        {
            string _UserID = _userprovider.GetUserId(email);
            return _context.Orders.Where(x => x.UserId == _UserID).AsEnumerable().Select(x => new UsersOrdersViewModel
            {
                Id = x.Id,
                TotalPrice = (x.Quantity * x.Price),
                OrderDate = x.OrderDate.ToString("MMM dd yyyy hh:MM tt"),
                Price = x.Price,
                Quantity = x.Quantity,
                Product = _context.Products.Find(x.ProductId) != null ? _context.Products.Find(x.ProductId).ItemName : string.Empty
            }).ToList();
        }

        public List<UsersOrdersViewModel> GetAllOrders()
        {
            return _context.Orders.OrderBy(x => x.Id).AsEnumerable().Select(x => new UsersOrdersViewModel
            {
                Id = x.Id,
                TotalPrice = (x.Quantity * x.Price),
                OrderDate = x.OrderDate.ToString("MMM dd yyyy hh:MM tt"),
                Price = x.Price,
                Quantity = x.Quantity,
                Product = _context.Products.Find(x.ProductId) != null ? _context.Products.Find(x.ProductId).ItemName : string.Empty
            }).ToList();
        }


        public List<Newsletter> GetAllNewsletter()
        {
            return _context.NewsLetter.OrderBy(x => x.Id).AsEnumerable().Select(x => new Newsletter
            {
                Id = x.Id,
                NewsLetter = x.News,
                NewsLetterDate = x.NewsDate.ToShortDateString()
            }).ToList();
        }

        public void AddNewsLetter(Newsletter dto)
        {
            NewsLetter news = new NewsLetter()
            {
                News = dto.NewsLetter,
                NewsDate = DateTime.Now
            };
            _context.NewsLetter.Add(news);
            _context.SaveChanges();
        }

        public void UpdateNewsLetter(Newsletter dto)
        {
            var item = _context.NewsLetter.Find(dto.Id);
            if (item != null)
            {
                item.News = dto.NewsLetter;
                item.NewsDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public Newsletter EditNewsletter(int id)
        {
            Models.Newsletter newsletter = new Newsletter();
            var item = _context.NewsLetter.Find(id);
            if (item != null)
            {
                newsletter.Id = item.Id;
                newsletter.NewsLetter = item.News;
            }
            return newsletter;
        }

        public bool DeleteNewsLetter(int id)
        {
            var item = _context.NewsLetter.Find(id);
            if (item != null)
            {
                _context.NewsLetter.Remove(item);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<CartViewModels> GetCostumerCartOrder(string email)
        {
            string _UserID = _userprovider.GetUserId(email);
            return _context.Cart.Where(x => x.UserId == _UserID).AsEnumerable().Select(x => new CartViewModels
            {
                Id = x.Id,
                TotalPrice = (x.Quantity * x.Price),
                OrderDate = x.OrderDate.ToString("MMM dd yyyy hh:MM tt"),
                Price = x.Price,
                Quantity = x.Quantity,
                Product = _context.Products.Find(x.ProductId) != null ? _context.Products.Find(x.ProductId).ItemName : string.Empty
            }).ToList();
        }


        public bool DeleteCartItem(int id)
        {
            var item = _context.Cart.Find(id);
            if (item != null)
            {
                _context.Cart.Remove(item);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PerformCheckout(string email)
        {
            string _UserID = _userprovider.GetUserId(email);
            var _cartItems = _context.Cart.Where(x => x.UserId == _UserID).AsEnumerable().ToList();
            if (_cartItems.Any())
            {
                foreach (var cartItem in _cartItems)
                {
                    Orders _order = new Orders()
                    {
                        OrderDate = DateTime.Now,
                        Price = cartItem.Price,
                        ProductId = cartItem.ProductId,
                        UserId = _UserID,
                        Quantity = cartItem.Quantity,
                    };
                    _context.Orders.Add(_order);
                    _context.SaveChanges();
                    _context.Cart.Remove(cartItem);
                    _context.SaveChanges();
                }
            }
        }
    }

}
