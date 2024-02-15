using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int CartId {  get; set; }
        public Cart cart { get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }

    }
}
