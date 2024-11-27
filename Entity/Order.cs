namespace OrderInquiry.Entity
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
