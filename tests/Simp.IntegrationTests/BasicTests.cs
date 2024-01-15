namespace Simp.IntegrationTests;

public class BasicTests(BootstrapperWebApplicationFactory<Program> factory) : IClassFixture<BootstrapperWebApplicationFactory<Program>>
{
    private readonly BootstrapperWebApplicationFactory<Program> _factory = factory;

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
