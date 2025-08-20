using System.Net;
using System.Net.Http.Json;
using ECommerce.Api.Dtos;

namespace ECommerce.Api.Tests;

public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccessAndCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/products");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task GetProducts_ReturnsListOfProducts()
    {
        // Act
        var products = await _client.GetFromJsonAsync<List<ProductDto>>("/api/products");

        // Assert
        Assert.NotNull(products);
        Assert.True(products.Count > 0);
        Assert.Contains(products, p => p.Name == "Laptop Pro");
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFoundForInvalidId()
    {
        // Act
        var response = await _client.GetAsync("/api/products/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}