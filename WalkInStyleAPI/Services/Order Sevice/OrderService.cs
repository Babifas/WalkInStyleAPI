using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel;
using WalkInStyleAPI.Data;
using WalkInStyleAPI.JWTVerification;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Order;

namespace WalkInStyleAPI.Services.Order_Sevice
{
    public class OrderService : IOrderService
    {
        private readonly ApDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IJWTService _jWTService;
        private readonly string HostUrl;
        public OrderService(ApDbContext dbContext, IMapper mapper,IConfiguration configuration,IJWTService jWTService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _jWTService= jWTService;
            HostUrl = configuration["HostUrl:Url"];
        }
        public async Task<bool> AddNewOrder(string token)
        {
            int userid = _jWTService.GetUserIdFromToken(token);
            var user = await _dbContext.Users.Include(u => u.cart)
                .ThenInclude(u => u.carts)
                .ThenInclude(u => u.Product)
                .FirstOrDefaultAsync(u => u.UserId == userid);
            if (user == null)
            {
                throw new Exception("User id not valid");
            }
            Order order = new Order
            {
                UserId = userid

            };
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            var cartitems = user.cart?.carts.Select(x => new OrderItem
            {
                OrderId = order.OrderId,
                ProductId = x.ProductId,
                OrderStatus = "pending",
                Quantity = x.Quantity,
                TotalPrice = x.Product.OfferPrice * x.Quantity,
            });
            if (cartitems == null)
            {
                return false;
            }

            foreach (var item in cartitems)
            {
                await _dbContext.OrderItems.AddAsync(item);
            }
            _dbContext.Carts.Remove(user.cart);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<OrderViewUser>> OrderDetails(string token)
        {
            int userid = _jWTService.GetUserIdFromToken(token);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userid);
            if (user == null)
            {
                throw new Exception("User id not valid");
            }
            var order = await _dbContext.Orders.Include(o => o.OrderItems)
                .ThenInclude(o => o.product)
                .FirstOrDefaultAsync(o => o.UserId == userid);
            if (order == null)
            {
                return new List<OrderViewUser>();
            }
            var orderitems = order.OrderItems.Select(oi => new OrderViewUser
            {
                ProductName = oi.product.ProductName,
                ProductImage =HostUrl+oi.product.Image,
                Quantity = oi.Quantity,
                TotalPrice = oi.TotalPrice,
            }).ToList();
            return orderitems;

        }

        public async Task<decimal> TotalRevanue()
        {
            decimal total = await _dbContext.OrderItems.SumAsync(x => x.TotalPrice);
            return total;
        }
        public async Task<int> TotalProductsPurchased()
        {
            int total=await _dbContext.OrderItems.SumAsync(x=>x.Quantity);
            return total;
        }

    }
}
