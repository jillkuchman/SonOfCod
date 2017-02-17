using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SonOfCod.Models
{
    public class NewsletterRecipient
    {
        [Key]
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
    }
}
