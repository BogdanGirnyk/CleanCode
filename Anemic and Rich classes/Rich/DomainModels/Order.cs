using CleanCode.Rich.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCode.AnemicRich.Rich.DomainModels
{
    public class Order
    {
        public int Id { get; private set; }

        public int DiscountPercent { get; private set; }

        public Customer Customer { get; private set; }

        public int TotalPrice { get; private set; }

        public List<OrderItem> OrderItems { get; private set; }

        public Order(Customer customer, List<OrderItem> orderItems)
        {
            Validate(customer, orderItems);

            Customer = customer;
            OrderItems = orderItems;

            CalculateAmounts();
        }

        public void ApplyDiscount(int discountPercent)
        {
            foreach (OrderItem item in OrderItems)
                item.ApplyDiscount(discountPercent);
            CalculateAmounts();
        }

        private void Validate(Customer customer, List<OrderItem> orderItems)
        {
            if (customer == null)
                throw new ArgumentException(nameof(Customer));
            if (orderItems is null || OrderItems.Count == 0)
                throw new ArgumentException(nameof(OrderItems));
        }

        private void CalculateAmounts()
        {
            TotalPrice = OrderItems.Sum(e => e.Amount);
        }
    }
}
