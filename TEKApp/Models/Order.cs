namespace TEKApp.Models
{
    public class Order
    {
        public Order(string orderId)
        {
            OrderId = orderId;
        }
        public string OrderId { get; }
        public FlightSchedule FlightSchedule { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as Order;
            if (other == null) return false;
            return OrderId == other.OrderId;
        }
        public override int GetHashCode()
        {
            return OrderId.GetHashCode();
        }
    }
}
