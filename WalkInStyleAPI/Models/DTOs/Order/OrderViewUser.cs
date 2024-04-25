namespace WalkInStyleAPI.Models.DTOs.Order
{
    public class OrderViewUser
    {
        public string ProductName {  get; set; }
        public string ProductImage {  get; set; }
        public string OrderStatus {  get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
