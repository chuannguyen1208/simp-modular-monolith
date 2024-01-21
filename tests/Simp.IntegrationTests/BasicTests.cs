using Simp.Modules.Cshop.Domain.Entities;

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

        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal("Modular monolith api", responseString);
    }

    [Fact]
    public async Task GetIngredients()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/ingredients");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        var ingredients = await response.Content.ReadFromJsonAsync<IEnumerable<Ingredient>>();
    }
}
