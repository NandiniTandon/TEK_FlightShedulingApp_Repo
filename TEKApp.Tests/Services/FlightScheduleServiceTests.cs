using System.Collections.Generic;
using Moq;
using TEKApp.Data;
using TEKApp.Models;
using TEKApp.Services;
using Xunit;

namespace TEKApp.Tests.Services
{
    public class FlightScheduleServiceTests
    {
        private readonly Flight _flight;
        public FlightScheduleServiceTests()
        {
            _flight = new Flight
            {
                ID = 1,
                Destination = new Airport { Destination = "XYZ" }.Destination,
                Source = new Airport { Destination = "YUL" }.Destination

            };

        }
              
 
        [Fact]
        public void SaveFlightSchedule_CallSaveFromRepository()
        {
            var mockRepo = new Mock<IRepository<FlightSchedule>>();
            mockRepo.Setup(x => x.Add(It.IsAny<FlightSchedule>())).Verifiable();

            var mockAppSettingService = new Mock<IAppSettingService>();
            var flightScheduleService = new FlightScheduleService(mockRepo.Object, mockAppSettingService.Object);
            flightScheduleService.SaveFlightSchedule(TEKApp.Models.Parameters.DayScheduleEnum.Day1, _flight);

            mockRepo.Verify(x => x.Add(It.IsAny<FlightSchedule>()), Times.Once);
            mockRepo.VerifyAll();
        }

        [Fact]
        public void GetAll_ReturnAllSchedule()
        {
            var mockRepo = new Mock<IRepository<FlightSchedule>>();
            var mockAppSettingService = new Mock<IAppSettingService>();
            mockRepo.Setup(x => x.GetAll()).Returns(new List<FlightSchedule>()
            {
                new FlightSchedule(_flight, Models.Parameters.DayScheduleEnum.Day1)
            });
            var flightScheduleService = new FlightScheduleService(mockRepo.Object, mockAppSettingService.Object);

            var result = flightScheduleService.GetAll();
            Assert.Single(result);
        }

        [Fact]
        public void GetNextAvailableFlightSchedule_ReturnValueComingFromRepo()
        {
            var mockRepo = new Mock<IRepository<FlightSchedule>>();
            mockRepo.Setup(x => x.GetAll()).Returns(new List<FlightSchedule>()
            {
                new FlightSchedule(_flight,Models.Parameters.DayScheduleEnum.Day1)

            });
           var mock = new Mock<IAppSettingService>();
            
            mock.Setup(x => x.MaxOrder).Returns(20);            
            var flightScheduleService = new FlightScheduleService(mockRepo.Object, mock.Object );

            var result = flightScheduleService.GetNextAvailableFlightSchedule("XYZ");

            Assert.NotNull(result);
            Assert.Equal( Models.Parameters.DayScheduleEnum.Day1, result.Schedule);
        }

        [Fact]
        public void CanAcceptOrder_False_AcceptedOrderLessThanOr20ButDifferentFlightDestination()
        {
            var flightSchedule = new FlightSchedule(_flight, Models.Parameters.DayScheduleEnum.Day1);
            var mockRepo = new Mock<IRepository<FlightSchedule>>();
            mockRepo.Setup(x => x.GetAll()).Returns(new List<FlightSchedule>()
            {
                new FlightSchedule(_flight,Models.Parameters.DayScheduleEnum.Day1)

            });
            var mock = new Mock<IAppSettingService>();

            mock.Setup(x => x.MaxOrder).Returns(20);
            var flightScheduleService = new FlightScheduleService(mockRepo.Object, mock.Object);


            //Add less than 20 orders
           
            flightSchedule.AcceptedOrders = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
         
            var actual = flightScheduleService.CanAcceptOrder("ABC", flightSchedule);

            Assert.False(actual);
        }

        [Fact]
        public void CanAcceptOrder_True_AcceptedOrderLessThan20ForSelectedDestination()
        {
           
           var flightSchedule = new FlightSchedule(_flight, Models.Parameters.DayScheduleEnum.Day1);
            var mockRepo = new Mock<IRepository<FlightSchedule>>();
            mockRepo.Setup(x => x.GetAll()).Returns(new List<FlightSchedule>()
            {
                new FlightSchedule(_flight,Models.Parameters.DayScheduleEnum.Day1)

            });
            var mock = new Mock<IAppSettingService>();

            mock.Setup(x => x.MaxOrder).Returns(20);
            var flightScheduleService = new FlightScheduleService(mockRepo.Object, mock.Object);

            //Add less than 20 orders
            flightSchedule.AcceptedOrders = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };

            var actual = flightScheduleService.CanAcceptOrder("XYZ", flightSchedule);
            Assert.True(actual);
        }


        
    }
}
