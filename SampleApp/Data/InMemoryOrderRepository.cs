using System.Collections.Generic;
using System.Linq;
using SampleApp.Models;

namespace SampleApp.Data;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();

    public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public void Save(Order order)
    {
        order.Id = _orders.Count + 1;
        _orders.Add(order);
    }
}
