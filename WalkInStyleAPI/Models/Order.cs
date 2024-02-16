using System.ComponentModel.DataAnnotations;

namespace WalkInStyleAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId {  get; set; }
        public int UserId {  get; set; }
        public User user { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
