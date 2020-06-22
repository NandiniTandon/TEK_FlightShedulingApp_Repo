using System.Collections.Generic;
using TEKApp.Models;
using TEKApp.Models.Parameters;

namespace TEKApp.Services
{
    public interface IFlightScheduleService
    {
        void SaveFlightSchedule(DayScheduleEnum scheduleId, Flight flight);

        IList<FlightSchedule> GetAll();

        FlightSchedule GetNextAvailableFlightSchedule(string orderDestination);
        public bool CanAcceptOrder(string orderDestination, FlightSchedule flightSchedule);

        public void AcceptOrder(string orderId, FlightSchedule flightSchedule);


    }
}
