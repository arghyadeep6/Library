using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Book
    {
        [Key]
        public int bookid { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string publication { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public long ISBN { get; set; }

    }
}
