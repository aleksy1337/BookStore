using Microsoft.AspNetCore.Identity;
using BookStore1.Data;
using BookStore1.Models;
using System.Diagnostics;
using System.Net;

namespace BookStore1.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Title ="Lalka",
                            Author = "Bolesław Prud",
                            Price = 50
                        },
                         new Book()
                        {
                            Title = "Pan Tadeusz",
                            Author = "Adam Mickiewicz",
                            Price = 50
                        },
                    });
                    context.SaveChanges();
                }
                
                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(new List<Customer>()
                    {
                        new Customer()
                        {
                            Name = "Filip",
                            Surname = "Copija",
                            DateOfBirth =new DateTime(2001,01,17),
                        },
                    });
                    context.SaveChanges();
                }
            }
        }

    }
}
