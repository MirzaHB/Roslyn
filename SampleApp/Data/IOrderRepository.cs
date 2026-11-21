using SampleApp.Models;

namespace SampleApp.Data;

public interface IOrderRepository
{
    Order? GetById(int id);
    void Save(Order order);
}
