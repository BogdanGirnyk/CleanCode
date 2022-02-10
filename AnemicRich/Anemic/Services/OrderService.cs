using CleanCode.AnemicRich.Anemic.DomainModels;
using CleanCode.AnemicRich.Anemic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCode.AnemicRich.Anemic.Services
{
    public class OrderService
    {
        public IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public void CreateOrder(Customer Customer, IEnumerable<OrderItemDto> itemsDtos)
        {
            var order = new Order
            {
                Customer = Customer,
            };

            order.OrderItems = itemsDtos.Select(e =>
                new OrderItem
                {
                    ProductId = e.ProductId,
                    Quantity = e.Quantity,
                    Price = e.Price,
                    Amount = e.Price * e.Quantity,
                }).ToList();

            var sumOfItems = order.OrderItems.Sum(e => e.Amount);
            order.TotalPrice = sumOfItems;

            _repo.Save(order);
        }

        public void ApplyDiscount(int id, int discountPercent)
        {
            var order = _repo.Get(id);

            order.DiscountPercent = discountPercent;

            foreach (OrderItem item in order.OrderItems)
                item.Amount = item.Quantity * item.Price * (discountPercent / 100);

            order.TotalPrice = order.OrderItems.Sum(e => e.Amount);

            _repo.Save(order);
        }
    }
}
