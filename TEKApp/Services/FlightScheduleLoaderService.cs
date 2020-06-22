using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TEKApp.Models;
using TEKApp.Models.Parameters;

namespace TEKApp.Services
{
    public class FlightScheduleLoaderService : ILoaderService<FlightSchedule>
    {
        private readonly IFlightScheduleService _flightScheduleService;
       

        public FlightScheduleLoaderService(IFlightScheduleService flightScheduleService)
        {
            _flightScheduleService = flightScheduleService;
        }
        public void Load(string input)
        {
            using var streamReader = new StreamReader(input);
            string inputData = streamReader.ReadToEnd();
            var definition = new[] { new { Day = 0, Flights = new List<Flight>() } };
            var loadedDataCollection = JsonConvert.DeserializeAnonymousType(inputData, definition);

            foreach (var loadedData in loadedDataCollection)
            {

                foreach (var flight in loadedData.Flights)
                {
                  
                    _flightScheduleService.SaveFlightSchedule((DayScheduleEnum)Enum.Parse(typeof(DayScheduleEnum), loadedData.Day.ToString()), flight);
                }
            }
        }

       
    }
}
