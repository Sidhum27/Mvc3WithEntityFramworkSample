using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcWeb1.Models
{
    public class Item
    {
        public int ItemID { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [DisplayName("CategoyID")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [DisplayName("UserName")]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Name")]
        [StringLength(30)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Info is required")]
        [DisplayName("Info")]
        [StringLength(200)]
        public string Info { get; set; }

        [Required(ErrorMessage = "Item Price is required")]
        [DisplayName("ItemPrice")]
        [DataType(DataType.Currency)]
        public decimal ItemPrice { get; set;  }

        [Required(ErrorMessage = "Date is required")]
        public System.DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "URL is required")]
        [DisplayName("ItemPic")]
        [StringLength(200)]
        [DataType(DataType.ImageUrl)]
        public string ItemPic { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }

    }
}