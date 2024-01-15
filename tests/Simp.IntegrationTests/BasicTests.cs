using Microsoft.AspNetCore.Mvc.Testing;

namespace Simp.IntegrationTests;

public class BasicTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task Default()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        Assert.Equal("Modular monolith api", await response.Content.ReadFromJsonAsync<string>());
    }
}
