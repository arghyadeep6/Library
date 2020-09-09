using LibraryApp.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class LibraryContext:DbContext
    {
        public LibraryContext()
        {
                
        }
        public LibraryContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Book> books { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Allocation> allocations { get; set; }

    }
}
