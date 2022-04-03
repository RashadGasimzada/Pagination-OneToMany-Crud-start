using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Models
{
    public class Beverage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryNameId { get; set; }
        public CategoryName CategoryName { get; set; }
    }
}
