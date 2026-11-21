using SampleApp.Framework;
using SampleApp.Models;
using SampleApp.Services;

namespace SampleApp.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orders;

    public OrdersController(IOrderService orders)
    {
        _orders = orders;
    }

    [HttpGet("{id}")]
    public Order? GetById(int id) => _orders.Find(id);

    [HttpPost]
    public Order Create(string customerEmail, decimal total)
    {
        return _orders.Place(customerEmail, total);
    }

    // No Http attribute, so this is not an endpoint.
    public string Health() => "ok";
}
