using FrontToBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryName> CategoryNames { get; set; }
        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Poster> Posters { get; set; }


    }
}
