
namespace Advantage.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public long Placed { get; set; }
        public long Fulfilled { get; set; }
        public string Status { get; set; }
    }
}
