using CleanCode.Rich.Models;

namespace CleanCode.AnemicRich.Anemic.Dtos
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
