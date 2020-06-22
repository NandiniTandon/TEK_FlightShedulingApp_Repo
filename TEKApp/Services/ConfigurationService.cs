using Microsoft.Extensions.Configuration;
using System;
using TEKApp.Data;
using TEKApp.Models;
using Unity;

namespace TEKApp.Services
{
    public static class ServiceConfiguration
    {
        static readonly IUnityContainer _container = new UnityContainer();
        public static void ConfigureContainer()
        {
            _container.RegisterSingleton<IAppSettingService, AppSettingService>();
            _container.RegisterSingleton<IRepository<Order>, Repository<Order>>();
            _container.RegisterSingleton<IRepository<Flight>, Repository<Flight>>();
            _container.RegisterSingleton<IRepository<FlightSchedule>, Repository<FlightSchedule>>();
            _container.RegisterSingleton<IFlightScheduleService, FlightScheduleService>();
            _container.RegisterSingleton<IOrderService, OrderService>();
            _container.RegisterSingleton<ILoaderService<FlightSchedule>, FlightScheduleLoaderService>();
            _container.RegisterSingleton<ILoaderService<Order>, OrderLoaderService>();
        }

        public static T GetService<T>()
        {
            return _container.Resolve<T>();
        }
       
    }
}
