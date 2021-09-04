using ECommerce.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Providers
{
    public interface IProducts
    {
        void AddProducts(MenuItemsViewModel dto, IHostingEnvironment _hostingEnvironment);
        void UpdateItem(MenuItemsViewModel dto, IHostingEnvironment _hostingEnvironment);
        MenuItemsViewModel EditItem(int id);
        List<MenuItemsViewModel> ItemsListing();
        bool DeleteItem(int id);
        MenuItemsViewModel GetItemDetails(int ItemId);
        void AddItemToBucket(UsersOrdersViewModel dto, string email);
        List<UsersOrdersViewModel> GetCostumerOrder(string email);
        List<UsersOrdersViewModel> GetAllOrders();
        List<Newsletter> GetAllNewsletter();
        void AddNewsLetter(Newsletter dto);
        Newsletter EditNewsletter(int id);
        void UpdateNewsLetter(Newsletter dto);
        bool DeleteNewsLetter(int id);
        List<CartViewModels> GetCostumerCartOrder(string email);
        bool DeleteCartItem(int id);
        void PerformCheckout(string email);

    }
}
