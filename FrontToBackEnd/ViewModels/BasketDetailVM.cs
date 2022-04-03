using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.ViewModels
{
    public class BasketDetailVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int Percent { get; set; }
        public string Title { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int Count { get; set; }
    }
}
