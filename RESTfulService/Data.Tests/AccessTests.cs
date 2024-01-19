using Microsoft.Extensions.Configuration;
using Data;
using Data.DatabaseLayer;
using Xunit.Abstractions;

namespace Data.Tests;

public class AccessTests
{
    private readonly ISignAccess _signAccess;
    private readonly IStadiumAccess _stadiumAccess;
    private readonly IEventAccess _eventAccess;
    private readonly IBookingAccess _bookingAccess;
    private readonly IBookingLineAccess _bookingLineAccess;
    private readonly ITestOutputHelper _outputHelper;

    public AccessTests(ITestOutputHelper output)
    {
        _outputHelper = output;
        IConfiguration inConfig = TestConfigHelper.GetIConfigurationRoot();
        _signAccess = new SignAccess(inConfig);
        _stadiumAccess = new StadiumAccess(inConfig);
        _eventAccess = new EventAccess(inConfig);
        _bookingAccess = new BookingAccess(inConfig);
        _bookingLineAccess = new BookingLineAccess(inConfig);
    }

    [Fact]
    public void GetSignById_ExistingId_ReturnsSign()
    {
        // Arrange
        int signId = 1;

        // Act
        var result = _signAccess.GetSignById(signId);

        // Assert
        Assert.NotNull(result);
        _outputHelper.WriteLine($"Found sign with id {result!.Id}");
    }

    [Fact]
    public void GetStadiumById_ExistingId_ReturnsStadium()
    {
        //Arrange
        int stadiumId = 1;

        //Act
        var result = _stadiumAccess.GetStadiumById(stadiumId);

        //Assert
        Assert.NotNull(result);
        _outputHelper.WriteLine($"Found stadium with id {result!.Id}");
    }

    [Fact]
    public void GetEventById_ExistingId_ReturnsEvent()
    {
        //Arrange
        int eventId = 1;

        //Act
        var result = _eventAccess.GetEventById(eventId);

        //Assert
        Assert.NotNull(result);
        _outputHelper.WriteLine($"Found event with id: {result!.Id}");
    }

    [Fact]
    public void GetBookingById_ExistingId_ReturnsBooking()
    {
        //Arrange
        int bookingId = 1;

        //Act
        var result = _bookingAccess.GetBookingById(bookingId);

        //Assert
        Assert.NotNull(result);
        _outputHelper.WriteLine($"Found booking with id: {result!.Id}");
    }

    [Fact] 
    public void GetBookingLineById_ExistingId_ReturnsBookingLine()
    {
        //Arrange
        int bookingLineId = 1;

        //Act
        var result = _bookingLineAccess.GetBookingLineById(bookingLineId);

        //Assert
        Assert.NotNull(result);
        _outputHelper.WriteLine($"Found booking line with id: {result!.Id}");
    }
}
