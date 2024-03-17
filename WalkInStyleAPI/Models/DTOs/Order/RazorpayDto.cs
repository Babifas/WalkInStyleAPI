namespace WalkInStyleAPI.Models.DTOs.Order
{
    public class RazorpayDto
    {
        public string razorpay_payment_id { get; set; }

        public string razorpay_order_id { get; set; }

        public string razorpay_signature { get; set; }

    }
}
