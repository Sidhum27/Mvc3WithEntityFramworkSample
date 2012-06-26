using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcWeb1.Models
{
    public class Library
    {
        public int LibraryID { get; set; }

        [Required(ErrorMessage = "FriendshipID is required")]
        [DisplayName("FriendshipID")]
        public int FriendshipID { get; set; }

        [Required(ErrorMessage = "ItemID is required")]
        [DisplayName("ItemID")]
        public int ItemID { get; set; }

        public System.DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Note is required")]
        [DisplayName("Note")]
        [StringLength(100)]
        public string Note { get; set; }

        // foriegn key
        public virtual Friendship Friendship { get; set; }
        public virtual Item Item { get; set; }
    }
}