using WalkInStyleAPI.Models.DTOs.Order;

namespace WalkInStyleAPI.Services.Order_Sevice
{
    public interface IOrderService
    {
        Task<bool> AddNewOrder(int userid);
        Task<List<OrderViewUser>> OrderDetails(int userid);
    }
}
