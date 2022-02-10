using System;

namespace CleanCode.AnemicRich.Rich.DomainModels
{
    public class OrderItem
    {
        public OrderItem(int productId, int price, int quantity)
        {
            Validate(productId, price, quantity);

            ProductId = productId;
            Price = price;
            Quantity = quantity;
            Amount = quantity * price;
        }

        private void Validate(int productId, int price, int quantity)
        {
            if (productId == 0)
                throw new ArgumentException(nameof(productId));
            if (quantity == 0)
                throw new ArgumentException(nameof(quantity));
            if (price == 0)
                throw new ArgumentException(nameof(price));
        }

        public void ApplyDiscount(int discountPercent)
        {
            Amount = Price * (discountPercent / 100);
        }

        public int ProductId { get; private set; }

        public int Price { get; private set; }

        public int Quantity { get; private set; }

        public int Amount { get; private set; }
    }
}
