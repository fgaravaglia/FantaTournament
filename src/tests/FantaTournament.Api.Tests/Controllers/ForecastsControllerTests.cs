namespace FantaTournament.Api.Tests.Controllers;

[TestFixture]
public class ForecastsControllerTests
{
    private IForecastQueries _forecastQueries;
    private IForecastCommands _forecastCommands;
    private ForecastsController _controller;

    [SetUp]
    public void SetUp()
    {
        //****************** ARRANGE
        _forecastQueries = Substitute.For<IForecastQueries>();
        _forecastCommands = Substitute.For<IForecastCommands>();
        _controller = new ForecastsController(_forecastQueries, _forecastCommands);
    }

    [Test]
    public async Task GetForecastById_WithExistingId_ReturnsOkWithForecast()
    {
        //****************** ARRANGE
        var forecastId = "f1";
        var forecast = new ForecastDto { Id = forecastId, UserId = "u1", BoardId = "b1" };
        _forecastQueries.GetForecastByIdAsync(forecastId).Returns(Result<ForecastDto>.Success(forecast));

        //****************** ACT
        var result = await _controller.GetForecastById(forecastId);

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(forecast));
        Assert.Pass();
    }

    [Test]
    public async Task GetForecastsByUserId_WithExistingUser_ReturnsOkWithForecasts()
    {
        //****************** ARRANGE
        var userId = "u1";
        var forecasts = new List<ForecastDto> { new ForecastDto { Id = "f1", UserId = userId, BoardId = "b1" } };
        _forecastQueries.GetForecastsByUserIdAsync(userId).Returns(Result<IEnumerable<ForecastDto>>.Success(forecasts));

        //****************** ACT
        var result = await _controller.GetForecastsByUserId(userId);

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(forecasts));
        Assert.Pass();
    }

    [Test]
    public async Task UpdateForecast_WithValidData_ReturnsOkWithId()
    {
        //****************** ARRANGE
        var id = "f1";
        var forecast = new ForecastDto { Id = id, UserId = "u1", BoardId = "b1" };
        _forecastCommands.UpdateForecastAsync(forecast).Returns(Result<string>.Success(id));

        //****************** ACT
        var result = await _controller.UpdateForecast(id, forecast);

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(id));
        Assert.Pass();
    }

    [Test]
    public async Task DeleteForecast_WithExistingId_ReturnsOkWithId()
    {
        //****************** ARRANGE
        var id = "f1";
        _forecastCommands.DeleteForecastAsync(id).Returns(Result<string>.Success(id));

        //****************** ACT
        var result = await _controller.DeleteForecast(id);

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(id));
        Assert.Pass();
    }
}
