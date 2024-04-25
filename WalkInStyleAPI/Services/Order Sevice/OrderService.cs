using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Razorpay.Api;
using System.ComponentModel;
using WalkInStyleAPI.Data;
using WalkInStyleAPI.JWTVerification;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.Order;

namespace WalkInStyleAPI.Services.Order_Sevice
{
    public class OrderService : IOrderService
    {
        private readonly IConfiguration _configuration;
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
            _configuration=configuration;
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
            Orders order = new Orders
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
                .Where(o => o.UserId == userid).ToListAsync();
            if (order == null)
            {
                return new List<OrderViewUser>();
            }
            List<OrderViewUser> orders = new List<OrderViewUser>();
            foreach( var orderitem in order)
            {
                foreach( var item in orderitem.OrderItems)
                {
                    orders.Add(new OrderViewUser
                    {
                        ProductName = item.product.ProductName,
                        ProductImage = HostUrl + item.product.Image,
                        OrderStatus = item.OrderStatus,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                    }
                    );

                }
            }
 
            return orders;

        }
        public async Task<List<OrderViewUser>> OrderDetailsAdmin(int userid)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userid);
            if (user == null)
            {
                throw new Exception("User id not valid");
            }
            var order = await _dbContext.Orders.Include(o => o.OrderItems)
                .ThenInclude(o => o.product)
                .Where(o => o.UserId == userid).ToListAsync();
            if (order == null)
            {
                return new List<OrderViewUser>();
            }
            List<OrderViewUser> orders = new List<OrderViewUser>();
            foreach (var orderitem in order)
            {
                foreach (var item in orderitem.OrderItems)
                {
                    orders.Add(new OrderViewUser
                    {
                        ProductName = item.product.ProductName,
                        ProductImage = HostUrl + item.product.Image,
                        OrderStatus = item.OrderStatus,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                    }
                    );

                }
            }

            return orders;

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
        public bool Payment(RazorpayDto razorpay)
        {
            if (razorpay == null ||
        razorpay.razorpay_payment_id == null ||
        razorpay.razorpay_order_id == null ||
        razorpay.razorpay_signature == null)
            {
                return false;
            }
            RazorpayClient client = new RazorpayClient(_configuration["Razorpay:KeyId"], _configuration["Razorpay:KeySecret"]);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razorpay_payment_id", razorpay.razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay.razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay.razorpay_signature);

            Utils.verifyPaymentSignature(attributes);


            return true;
        }
        public async Task<string> OrderCreate(long price)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            Random random = new Random();
            string TrasactionId = random.Next(0, 1000).ToString();
            input.Add("amount", Convert.ToDecimal(price) * 100);
            input.Add("currency", "INR");
            input.Add("receipt", TrasactionId);

            string key = _configuration["Razorpay:KeyId"];
            string secret = _configuration["Razorpay:KeySecret"];

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            var OrderId = order["id"].ToString();

            return OrderId;
        }

    }
}
