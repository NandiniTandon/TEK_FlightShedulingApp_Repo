using System.Collections.Generic;
using TEKApp.Data;
using TEKApp.Models;
 
namespace TEKApp.Services
{
    public class OrderService : IOrderService
    {
        
        private readonly IFlightScheduleService _flightScheduleService;
        private readonly IRepository<Order> _repository;
        public OrderService(IFlightScheduleService flightScheduleService, IRepository<Order> repository)
        {
            _flightScheduleService = flightScheduleService;
            _repository = repository;
        }

        public void SaveOrder(string orderId, string destinationAirport)
        {
            var order = new Order(orderId)
            {
                FlightSchedule = _flightScheduleService.GetNextAvailableFlightSchedule(destinationAirport)
            };
            if (order.FlightSchedule != null)
            {                 
                _flightScheduleService.AcceptOrder(order.OrderId, order.FlightSchedule);
            }
            _repository.Add(order);
        }

        public IList<Order> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
