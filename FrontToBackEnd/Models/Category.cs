using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Models
{
    public class Category
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu hisseni bosh buraxmayin")]
        [StringLength(20, ErrorMessage = "Uzunluq chox ola bilmez")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
