using System.Collections.Generic;

namespace CleanCode.AnemicRich.Anemic.DomainModels
{
    public class Order
    {
        public int Id { get; set; } 

        public Customer Customer { get; set; }

        public int DiscountPercent { get; set; }

        public int TotalPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
