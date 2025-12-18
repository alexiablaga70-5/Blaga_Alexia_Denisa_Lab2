using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blaga_Alexia_Denisa_Lab2.Models;

namespace Blaga_Alexia_Denisa_Lab2.Data
{
    public class Blaga_Alexia_Denisa_Lab2Context : DbContext
    {
        public Blaga_Alexia_Denisa_Lab2Context (DbContextOptions<Blaga_Alexia_Denisa_Lab2Context> options)
            : base(options)
        {
        }
        public DbSet<Blaga_Alexia_Denisa_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Blaga_Alexia_Denisa_Lab2.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Blaga_Alexia_Denisa_Lab2.Models.Genre> Genre { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
    }
}
