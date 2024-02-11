using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models.Product
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public int OrginalPrice { get; set; }
        [Required]
        public int OfferPrice { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public int Stock { get; set; }

    }
}
