#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsidTestApplication.Models;

namespace ConsidTestApplication.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<ConsidTestApplication.Models.LibraryItem> LibraryItem { get; set; }

        public DbSet<ConsidTestApplication.Models.Category> Category { get; set; }

        public DbSet<ConsidTestApplication.Models.Employees> Employees { get; set; }
    }
}
