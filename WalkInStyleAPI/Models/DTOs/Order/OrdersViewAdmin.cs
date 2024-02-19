namespace WalkInStyleAPI.Models.DTOs.Order
{
    public class OrdersViewAdmin
    {
        public int OrderId { get; set; }
        public string? UserName {  get; set; }
        public string? ProductName { get; set; }
        public int Quantity {  get; set; }
        public decimal TotalPrice { get; set; }
        public string? OrderStatus { get; set; }

    }
}
