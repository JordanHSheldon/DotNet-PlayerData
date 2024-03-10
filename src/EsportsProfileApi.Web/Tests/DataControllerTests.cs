namespace Tests;

using EsportsProfileWebApi.CROSSCUTTING.Requests.Data;
using EsportsProfileWebApi.CROSSCUTTING.Responses.Data;
using EsportsProfileWebApi.Web.Controllers;
using EsportsProfileWebApi.Web.Orchestrators;
using Moq;
using NUnit.Framework;

[TestFixture]
public class DataControllerTests
{
    private readonly Mock<IDataOrchestrator> mockDataOrchestrator;
    private readonly DataController _dataController;

    public DataControllerTests()
    {
        mockDataOrchestrator = new Mock<IDataOrchestrator>();
        _dataController = new DataController(mockDataOrchestrator.Object);
    }

    [Test]
    public void GetAllDataAsync_ValidRequest_ReturnsGetDataResponse()
    {
        // Arrange
        var request = new List<GetDataResponse>();
        mockDataOrchestrator.Setup(test => test.GetAllDataAsync()).ReturnsAsync(request);

        // Act
        var result = _dataController.GetAllDataAsync();

        // Assert
        Assert.IsNotNull(result);
        mockDataOrchestrator.Verify(verify => verify.GetAllDataAsync(), Times.Once);
    }

    [Test]
    public void GetUserDataByUserName_ValidRequest_ReturnsGetDataResponse()
    {
        // Arrange
        var request = new GetDataRequest();
        mockDataOrchestrator.Setup(test => test.GetUserDataByAlias(It.IsAny<GetDataRequest>())).ReturnsAsync(new GetDataResponse());

        // Act
        var result = _dataController.GetDataByUserName(request);

        // Assert
        Assert.IsNotNull(result);
        mockDataOrchestrator.Verify(verify => verify.GetUserDataByAlias(request), Times.Once);
    }
    //GetDataById

    //[Test]
    //public void GetDataById_ValidRequest_ReturnsGetDataResponse()
    //{
    //    // Arrange
    //    var request = new GetDataRequest();
    //    mockDataOrchestrator.Setup(test => test.GetUserDataByAlias(It.IsAny<GetDataRequest>())).ReturnsAsync(new GetDataResponse());

    //    // Act
    //    var result = _dataController.GetDataById(request);

    //    // Assert
    //    Assert.IsNotNull(result);
    //    mockDataOrchestrator.Verify(verify => verify.GetUserDataByAlias(request), Times.Once);
    //}
}