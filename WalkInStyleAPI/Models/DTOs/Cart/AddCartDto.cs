namespace WalkInStyleAPI.Models.DTOs.Cart
{
    public class AddCartDto
    {
     
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
