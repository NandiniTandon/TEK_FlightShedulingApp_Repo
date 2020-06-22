using System.Collections.Generic;
using TEKApp.Models;

namespace TEKApp.Services
{
    public interface IOrderService
    {
        IList<Order> GetAll();
        void SaveOrder(string orderId, string destinationAirport);
    }
}