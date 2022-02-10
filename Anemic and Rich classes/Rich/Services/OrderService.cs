using CleanCode.AnemicRich.Rich.DomainModels;
using CleanCode.AnemicRich.Rich.Dtos;
using CleanCode.Rich.Models;
using System.Collections.Generic;
using System.Linq;

namespace CleanCode.AnemicRich.Rich.Services
{
    public class OrderService
    {
        public IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public void CreateOrder(Customer customer, IEnumerable<OrderItemDto> itemsDtos)
        {
            var orderItems = itemsDtos.Select(e => new OrderItem(e.ProductId, e.Price, e.Quantity)).ToList();
            var order = new Order(customer, orderItems);

            _repo.Save(order);
        }
        public void ApplyDiscount(int id, int discountPercent)
        {
            var order = _repo.Get(id);

            order.ApplyDiscount(discountPercent);

            _repo.Save(order);
        }
    }
}
