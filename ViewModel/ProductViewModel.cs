using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagmentApp.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string? Color { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Category { get; set; } = default!;
    }
}
