using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWeb1.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [DisplayName("UserName")]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Adress is required")]
        [DisplayName("Adress")]
        [StringLength(100)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Name")]
        [StringLength(15)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Bio is required, Come on, at least write a few word")]
        [DisplayName("Bio")]
        [StringLength(500)]
        public string Bio { get; set; }

        [Required(ErrorMessage = "URL is required")]
        [DisplayName("UserPic")]
        [StringLength(300)]
        [DataType(DataType.ImageUrl)]
        public string UserPic { get; set; }

        public System.DateTime DateUpdated { get; set; }

    }
}