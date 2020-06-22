using System;
using TEKApp.Models;
using TEKApp.Services;

namespace TEKApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceConfiguration.ConfigureContainer();

            LoadSchedules();

            WriteFlightSchedulesToConsole();

            LoadOrders();

            WriteOrdersToConsole();

            Console.Read();
        }

        static void LoadSchedules()
        {
            var service = ServiceConfiguration.GetService<ILoaderService<FlightSchedule>>();
            service.Load(@"App_Data/Flights.json");
        }

        static void WriteFlightSchedulesToConsole()
        {
            var service = ServiceConfiguration.GetService<IFlightScheduleService>();
            var flightSchedules = service.GetAll();

            foreach (var flightSchedule in flightSchedules)
            {
                Console.WriteLine($"Flight: {flightSchedule.Flight.ID}, departure: {flightSchedule.Flight.Source}, arrival: {flightSchedule.Flight.Destination}, day: {flightSchedule.Schedule}");
            }
            Console.WriteLine();
        }
        static void LoadOrders()
        {
            var service = ServiceConfiguration.GetService<ILoaderService<Order>>();
            service.Load(@"App_Data/coding-assigment-orders.json");
        }

        static void WriteOrdersToConsole()
        {
            var service = ServiceConfiguration.GetService<IOrderService>();
            var orders = service.GetAll();

            foreach (var order in orders)
            {
                if (order.FlightSchedule != null)
                {
                    Console.WriteLine($"order: {order.OrderId}, flightNumber: {order.FlightSchedule.Flight.ID}, departure: {order.FlightSchedule.Flight.Source}, arrival: {order.FlightSchedule.Flight.Destination}, day: {order.FlightSchedule.Schedule}");
                }
                else
                {
                    Console.WriteLine($"order: {order.OrderId}, flightNumber: not scheduled");
                }
            }
            Console.WriteLine();
        }

    }
}
