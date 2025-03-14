using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ProductManagmentApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int  Price { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }

    }
}
