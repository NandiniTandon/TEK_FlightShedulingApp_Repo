using System.Collections.Generic;
using System.Linq;
using TEKApp.Data;
using TEKApp.Models;
using TEKApp.Models.Parameters;

namespace TEKApp.Services
{
    public class FlightScheduleService : IFlightScheduleService
    {
        private readonly IRepository<FlightSchedule> _repository;
        
        private readonly IAppSettingService _appSettingService;
        public FlightScheduleService(IRepository<FlightSchedule> repository, IAppSettingService appSettingService)
        {
            _repository = repository;
            _appSettingService = appSettingService;

        }
        public void SaveFlightSchedule(DayScheduleEnum scheduleId, Flight flight)
        {           
            _repository.Add(new FlightSchedule(flight, scheduleId));
        }

        public IList<FlightSchedule> GetAll()
        {
            return _repository.GetAll();
        }

        public FlightSchedule GetNextAvailableFlightSchedule(string orderDestination)
        {
             return _repository.GetAll().FirstOrDefault(fs => CanAcceptOrder(orderDestination,fs));
        }


        public bool CanAcceptOrder(string orderDestination, FlightSchedule flightSchedule)
        {
            return flightSchedule.AcceptedOrders.Count < _appSettingService.MaxOrder && orderDestination == flightSchedule.Flight.Destination;
        }

        public void AcceptOrder(string orderId, FlightSchedule flightSchedule)
        {
            flightSchedule.AcceptedOrders.Add(orderId);
        }


    }
}
