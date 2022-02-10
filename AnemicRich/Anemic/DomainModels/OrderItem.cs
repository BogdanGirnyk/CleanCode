namespace CleanCode.AnemicRich.Anemic.DomainModels
{
    public class OrderItem
    {
        public int ProductId { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int Amount { get; set; }
    }
}
