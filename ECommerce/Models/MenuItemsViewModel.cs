using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class MenuItemsViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Required]
        [Display(Name = "Price")]
        public string Price { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string imageurl { get; set; }
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        //[Required(ErrorMessage = "Please choose an Image to upload.")]
        public List<IFormFile> Image { get; set; }
    }
}
