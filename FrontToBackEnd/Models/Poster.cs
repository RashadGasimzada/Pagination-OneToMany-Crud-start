using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Models
{
    public class Poster
    {
        public int Id { get; set; }
        public string Discount { get; set; }
        public string Cookie { get; set; }
        public string WeekCount { get; set; }
        public string Image { get; set; }

        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }

    }
}
