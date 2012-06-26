using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcWeb1.Models
{
    public class Friendship
    {
        public int FriendshipID { get; set; }

        [Required(ErrorMessage = "FriendshipName is required")]
        [DisplayName("FriendshipName")]
        [StringLength(100)]
        public string FriendshipName { get; set; }

        [Required(ErrorMessage = "UserName1 is required")]
        [DisplayName("UserName1")]
        [StringLength(100)]
        public string UserName1 { get; set; }

        [Required(ErrorMessage = "UserName2 is required")]
        [DisplayName("UserName2")]
        [StringLength(100)]
        public string UserName2 { get; set; }

        public System.DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Status1 is required")]
        [DisplayName("Status1")]
        public int Status1 { get; set; }

        [Required(ErrorMessage = "Status2 is required")]
        [DisplayName("Status2")]
        public int Status2 { get; set; }

    }
}