namespace WalkInStyleAPI.Models
{
    public class Whishlist
    {
        public int WhishlistId { get; set; }
        public int UserId {  get; set; }
        public int ProductId {  get; set; }
        public User User { get; set; }
        public List<Product> Products { get; set;}
    }
}
