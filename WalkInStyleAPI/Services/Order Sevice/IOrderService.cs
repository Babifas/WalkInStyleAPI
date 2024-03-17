using WalkInStyleAPI.Models.DTOs.Order;

namespace WalkInStyleAPI.Services.Order_Sevice
{
    public interface IOrderService
    {
        Task<bool> AddNewOrder(string token);
        Task<List<OrderViewUser>> OrderDetails(string token);
        Task<decimal> TotalRevanue();
        Task<int> TotalProductsPurchased();
        bool Payment(RazorpayDto razorpay);
        Task<string> OrderCreate(long price);
    }
}
