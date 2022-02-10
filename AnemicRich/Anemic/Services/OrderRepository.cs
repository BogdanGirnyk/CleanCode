using CleanCode.AnemicRich.Anemic.DomainModels;

namespace CleanCode.AnemicRich.Anemic.Services
{
    public interface IOrderRepository
    {
        public Order Get(int Id);
        public void Save(Order order);
    }
}
