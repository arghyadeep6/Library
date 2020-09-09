using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Allocation
    {
        [Key]
        public int allocationid { get; set; }
        public int bookid { get; set; }
        public int studentid { get; set; }

    }
}
