using System.Collections.Generic;
using TEKApp.Models.Parameters;

namespace TEKApp.Models
{    public class FlightSchedule
    {
        public IList<string> AcceptedOrders { get; set; } = new List<string>();
        public FlightSchedule(Flight flight, DayScheduleEnum schedule)
        {
            Flight = flight;
            Schedule = schedule;
        }
        public Flight Flight { get; }
        public DayScheduleEnum Schedule { get; }
    }

}

