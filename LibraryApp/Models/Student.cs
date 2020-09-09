using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Controllers
{
    public class Student
    {
        [Key]
        public int studentid { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int cls { get; set; }
        [Required]
        public long phno { get; set; }
    }
}
