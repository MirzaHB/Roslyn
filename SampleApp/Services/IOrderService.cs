using SampleApp.Models;

namespace SampleApp.Services;

public interface IOrderService
{
    Order? Find(int id);
    Order Place(string customerEmail, decimal total);
}
