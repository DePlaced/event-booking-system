using RESTfulService.BusinesslogicLayer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Data.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Data.Tests
{
    public class LogicAccessTest
    {
        private readonly ITestOutputHelper _outputHelper;
        readonly private ISignAccess _signAccess;
        readonly private ISignLogic _signLogic;
        readonly private IStadiumAccess _stadiumAccess;
        readonly private IStadiumLogic _stadiumLogic;
        private readonly IEventAccess _eventAccess;
        private readonly IEventLogic _eventLogic;
        private readonly IBookingAccess _bookingAccess;
        private readonly IBookingLogic _bookingLogic;
        private readonly IBookingLineAccess _bookingLineAccess;
        private readonly IBookingLineLogic _bookingLineLogic;

        public LogicAccessTest(ITestOutputHelper output)
        {
            _outputHelper = output;
            IConfiguration inConfig = TestConfigHelper.GetIConfigurationRoot();
            _signAccess = new SignAccess(inConfig);
            _signLogic = new SignLogic(_signAccess);
            _stadiumAccess = new StadiumAccess(inConfig);
            _stadiumLogic = new StadiumLogic(_stadiumAccess);
            _eventAccess = new EventAccess(inConfig);
            _eventLogic = new EventLogic(_eventAccess);
            _bookingAccess = new BookingAccess(inConfig);
            _bookingLogic = new BookingLogic(_bookingAccess);
            _bookingLineAccess = new BookingLineAccess(inConfig);
            _bookingLineLogic = new BookingLineLogic(_bookingLineAccess, _eventAccess);

        }

        [Fact]
        public async Task GetAllSigns_WithExistingStadiumId()
        {
            //arrange
            int stadiumId = 1;

            //act
            var result = await _signLogic.GetAll(stadiumId);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _outputHelper.WriteLine($"There is {result!.Count} signs in stadium with id: {stadiumId}");
        }

        [Fact]
        public async Task GetAllSigns()
        {
            //Arrange

            //Act
            var result = await _stadiumLogic.GetAll();

			//Assert
			Assert.NotNull(result);
			Assert.NotEmpty(result);
            _outputHelper.WriteLine($"There are {result!.Count} stadium(s)");
        }

        [Fact]
        public async Task GetAllEvents()
        {
            //Arrange
            int signId =-1;

            //Act
            var result = await _eventLogic.GetAll(signId);

			//Assert
			Assert.NotNull(result);
			Assert.NotEmpty(result);
            _outputHelper.WriteLine($"There are {result!.Count} event(s)");
        }

        [Fact]
        public async Task GetAllBookings_WithExistingUserId()
        {
            //Arrange
            int userId = 1;

            //Act
            var result = await _bookingLogic.GetAllByUserId(userId);

			//Assert
			Assert.NotNull(result);
			Assert.NotEmpty(result);
            _outputHelper.WriteLine($"User with id: {userId} has {result!.Count} booking(s)");
        }

        [Fact]
        public async Task GetAllBookingLines_withExistingBookingId() { 
            
            //Arrange
            int bookingId = 1;

            //Act
            var result = await _bookingLineLogic.GetAll(bookingId);

			//Assert
			Assert.NotNull(result);
			Assert.NotEmpty(result);
            _outputHelper.WriteLine($"Booking with id: {bookingId} has {result!.Count} booking line(s)");
        }
    }
}
