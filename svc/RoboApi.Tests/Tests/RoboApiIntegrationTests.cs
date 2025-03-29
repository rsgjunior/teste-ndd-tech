using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RoboApi.Models.Dtos;
using RoboApi.Models.Enums;
using RoboApi.Services;

namespace RoboApi.Tests;

public class RoboApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public RoboApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

        RoboService.Reset();
    }

    [Fact]
    public async Task GetShouldReturnRoboWithInitialState()
    {
        // Act
        var response = await _client.GetAsync("/robo");
        var jsonString = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Rest", jsonString);
    }

    [Fact]
    public async Task RotateHeadShouldReturnOkWhenValidRotation()
    {
        // Arrange
        var dto = new HeadRotationDto { Position = HeadRotation.Rotation45 };
        var content = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync("/robo/head/rotate", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task RotateHeadShouldReturnBadRequestWhenHeadIsDown()
    {
        // Arrange
        var inclineDto = new HeadInclinationDto { Position = HeadInclination.Down };
        var inclineContent = new StringContent(
            JsonSerializer.Serialize(inclineDto),
            Encoding.UTF8,
            "application/json");
        await _client.PutAsync("/robo/head/incline", inclineContent);

        var rotateDto = new HeadRotationDto { Position = HeadRotation.Rotation45 };
        var rotateContent = new StringContent(
            JsonSerializer.Serialize(rotateDto),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync("/robo/head/rotate", rotateContent);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(ArmSide.Left)]
    [InlineData(ArmSide.Right)]
    public async Task MoveWristShouldReturnBadRequestWhenElbowNotContracted(ArmSide side)
    {
        // Arrange
        var dto = new WristPositionDto { Position = WristPosition.Rotation45 };
        var content = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/robo/arm/{side}/wrist", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task MoveWristShouldReturnOkWhenElbowIsContracted()
    {
        // Arrange - First contract the elbow
        ElbowPosition[] positions = [
            ElbowPosition.SlightlyContracted,
            ElbowPosition.Contracted,
            ElbowPosition.StronglyContracted,
        ];

        foreach (ElbowPosition position in positions)
        {
            var elbowDto = new ElbowPositionDto { Position = position };
            var elbowContent = new StringContent(
                JsonSerializer.Serialize(elbowDto),
                Encoding.UTF8,
                "application/json");
            await _client.PutAsync("/robo/arm/left/elbow", elbowContent);
        }


        // Act - Try to move the wrist
        var wristDto = new WristPositionDto { Position = WristPosition.Rotation45 };
        var wristContent = new StringContent(
            JsonSerializer.Serialize(wristDto),
            Encoding.UTF8,
            "application/json");
        var response = await _client.PutAsync("/robo/arm/left/wrist", wristContent);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}