using FrontToBackEnd.Models;
using FrontToBackEnd.ViewModels.Admin.SliderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Card> Cards { get; set; }
        public List<Category> Categories { get; set; }
        public List<CategoryName> CategoryNames { get; set; }
        public List<Fruit> Fruits { get; set; }
        public List<Beverage> Beverages { get; set; }
        public List<Poster> Posters { get; set; }


    }
}
