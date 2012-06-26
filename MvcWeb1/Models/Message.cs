using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcWeb1.Models
{
    public class Message
    {
        public int MessageID { get; set; }

        [Required(ErrorMessage = "MessageSubject is required")]
        [DisplayName("MessageSubject")]
        [StringLength(50)]
        public string MessageSubject { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [DisplayName("FromUser")]
        public string FromUser { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [DisplayName("ToUser")]
        public string ToUser { get; set; }

        [Required(ErrorMessage = "MessageBody required")]
        [DisplayName("MessageBody")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string MessageBody { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public System.DateTime DateSend { get; set; }

        public virtual User User { get; set; }

    }
}