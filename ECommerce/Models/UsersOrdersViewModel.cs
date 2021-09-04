using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class UsersOrdersViewModel
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Total")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Date")]
        public string OrderDate { get; set; }
    }
}
