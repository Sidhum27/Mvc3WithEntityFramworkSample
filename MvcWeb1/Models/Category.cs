using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcWeb1.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Name")]
        [StringLength(15)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Info is required")]
        [DisplayName("Info")]
        [StringLength(100)]
        public string Info { get; set; }
    }
}