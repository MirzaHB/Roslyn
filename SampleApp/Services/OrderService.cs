using System;
using SampleApp.Data;
using SampleApp.Models;

namespace SampleApp.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IEmailSender _email;

    public OrderService(IOrderRepository repository, IEmailSender email)
    {
        _repository = repository;
        _email = email;
    }

    public Order? Find(int id) => _repository.GetById(id);

    public Order Place(string customerEmail, decimal total)
    {
        Validate(total);

        var order = new Order { CustomerEmail = customerEmail, Total = total };
        _repository.Save(order);
        _email.Send(customerEmail, $"Order {order.Id} confirmed");
        return order;
    }

    private static void Validate(decimal total)
    {
        if (total <= 0)
            throw new ArgumentException("Total must be positive", nameof(total));
    }
}
