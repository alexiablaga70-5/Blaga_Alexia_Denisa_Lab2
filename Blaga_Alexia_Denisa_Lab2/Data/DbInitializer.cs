using Blaga_Alexia_Denisa_Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Blaga_Alexia_Denisa_Lab2.Data
{
 public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Blaga_Alexia_Denisa_Lab2Context
           (serviceProvider.GetRequiredService
            <DbContextOptions<Blaga_Alexia_Denisa_Lab2Context>>()))
            {
                if (context.Book.Any())
                {
                    return; // BD a fost creata anterior
                }
                var a1 = new Author { FirstName = "Mihail", LastName = "Sadoveanu" };
                var a2 = new Author { FirstName = "George", LastName = "Calinescu" };
                var a3 = new Author { FirstName = "Mircea", LastName = "Eliade" };

                context.Authors.AddRange(a1, a2, a3);
                context.SaveChanges();

                context.Book.AddRange(
                new Book { Title = "Baltagul", AuthorID = a1.ID, Price = Decimal.Parse("22") },

                new Book { Title = "Enigma Otiliei", AuthorID = a2.ID, Price = Decimal.Parse("18") },

                new Book { Title = "Maytrei", AuthorID = a3.ID, Price = Decimal.Parse("27") }
             );

                context.Genre.AddRange(
               new Genre { Name = "Roman" },
               new Genre { Name = "Nuvela" },
               new Genre { Name = "Poezie" }
                );
                context.Customer.AddRange(
                new Customer
                {
                    Name = "Popescu Marcela",
                    Adress = "Str. Plopilor, nr. 24",
                    BirthDate = DateTime.Parse("1979-09-01")
                },
                new Customer
                {
                    Name = "Mihailescu Cornel",
                    Adress = "Str. Bucuresti, nr.45, ap. 2",
                    BirthDate=DateTime.Parse("1969 - 07 - 08")}
               
                );

                context.SaveChanges();
            }
        }
    }
}

