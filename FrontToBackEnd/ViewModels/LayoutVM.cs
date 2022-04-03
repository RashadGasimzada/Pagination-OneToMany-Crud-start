using FrontToBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.ViewModels
{
    public class LayoutVM
    {
        public List<Category> Categories { get; set; }
        public List<CategoryName> CategoryNames { get; set; }
        public List<Fruit> Fruits { get; set; }
        public List<Beverage> Beverages { get; set; }
    }
}
