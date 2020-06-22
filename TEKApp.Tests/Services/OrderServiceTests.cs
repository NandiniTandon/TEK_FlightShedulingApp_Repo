using System.Collections.Generic;
using Moq;
using TEKApp.Data;
using TEKApp.Models;
using TEKApp.Services;
using Xunit;

namespace TEKApp.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly FlightSchedule flightSchedule;
        public OrderServiceTests()
        {
            flightSchedule = new FlightSchedule(
                new Flight
                {
                    ID = 1,
                    Destination = new Airport { Destination = "XYZ" }.Destination,
                    Source = new Airport { Destination = "YUL" }.Destination
                },
                Models.Parameters.DayScheduleEnum.Day1);
        }

        [Fact]
        public void GetAll_ReturnAllOrders()
        {
            var mockRepo = new Mock<IRepository<Order>>();
            mockRepo.Setup(x => x.GetAll()).Returns(new List<Order>()
            {
                new Order("O001")
                {
                    FlightSchedule=flightSchedule
                }
            });
            OrderService orderService = new OrderService(null, mockRepo.Object);
            var result = orderService.GetAll();
            Assert.Single(result);
        }


        [Fact]
        public void SaveOrder_CallAcceptOrderAndAdd_NotNullScheduledOrder()
        {
            var mockFlightSchedule = new Mock<IFlightScheduleService>();
            mockFlightSchedule.Setup(x => x.GetNextAvailableFlightSchedule("XYZ")).Returns(flightSchedule).Verifiable();
            var mockRepo = new Mock<IRepository<Order>>();
            mockRepo.Setup(x => x.Add(It.IsAny<Order>())).Verifiable();
            OrderService orderService = new OrderService(mockFlightSchedule.Object, mockRepo.Object);
            orderService.SaveOrder("O001", new Airport { Destination = "XYZ" }.Destination);


            mockFlightSchedule.VerifyAll();
            mockFlightSchedule.Verify(x => x.GetNextAvailableFlightSchedule("XYZ"), Times.Once);
            mockRepo.VerifyAll();
            //mockRepo.Verify(x => x.Add(It.IsAny<Order>()), Times.Once);
            mockRepo.Verify(x => x.Add(new Order("O001")
            {
                FlightSchedule = flightSchedule
            }), Times.Once);

        }

        [Fact]
        public void SaveOrder_CallAcceptOrderAndAdd_WithNullFlightScheduled()
        {
            var mockFlightSchedule = new Mock<IFlightScheduleService>();
            mockFlightSchedule.Setup(x => x.GetNextAvailableFlightSchedule("XYZ")).Returns(flightSchedule).Verifiable();
            var mockRepo = new Mock<IRepository<Order>>();
            mockRepo.Setup(x => x.Add(It.IsAny<Order>()));
            OrderService orderService = new OrderService(mockFlightSchedule.Object, mockRepo.Object);
            //Call service for different destination
            orderService.SaveOrder("O001", new Airport { Destination = "ABC" }.Destination);

            mockFlightSchedule.Verify(x => x.GetNextAvailableFlightSchedule("ABC"), Times.Once);
            mockRepo.VerifyAll();
            //mockRepo.Verify(x => x.Add(It.IsAny<Order>()), Times.Once);
            mockRepo.Verify(x => x.Add(new Order("O001")
            {
                FlightSchedule = null
            }), Times.Once);

        }
    }
}
