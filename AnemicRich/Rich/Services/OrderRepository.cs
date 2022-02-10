using CleanCode.AnemicRich.Rich.DomainModels;

namespace CleanCode.AnemicRich.Rich.Services
{
    public interface IOrderRepository
    {
        public Order Get(int Id);
        public void Save(Order order);
    }
}
