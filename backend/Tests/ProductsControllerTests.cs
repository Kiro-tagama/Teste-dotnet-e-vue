using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly AppDbContext _dbContext;

    public ProductsControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new AppDbContext(options);
        _controller = new ProductsController(_dbContext);
    }

    private async Task ResetDatabase()
    {
        _dbContext.Products.RemoveRange(_dbContext.Products);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetProducts_ReturnsAllProducts()
    {
        await ResetDatabase();
        _dbContext.Products.AddRange(
            new Product { Id = Guid.NewGuid(), Name = "Product 1" },
            new Product { Id = Guid.NewGuid(), Name = "Product 2" }
        );
        await _dbContext.SaveChangesAsync();

        var result = await _controller.GetProducts();
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
        var products = Assert.IsType<List<Product>>(actionResult.Value);
        Assert.Equal(2, products.Count);
    }

    [Fact]
    public async Task GetProduct_ReturnsProduct_WhenFound()
    {
        await ResetDatabase();
        var product = new Product { Id = Guid.NewGuid(), Name = "Test Product" };
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        var result = await _controller.GetProduct(product.Id);
        var actionResult = Assert.IsType<ActionResult<Product>>(result);
        var returnedProduct = Assert.IsType<Product>(actionResult.Value);
        Assert.Equal("Test Product", returnedProduct.Name);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        await ResetDatabase();
        var result = await _controller.GetProduct(Guid.NewGuid());
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateProduct_ReturnsCreatedAtAction()
    {
        await ResetDatabase();
        var newProduct = new Product { Name = "New Product", Price = 10, Quantity = 5, CategoryId = Guid.NewGuid() };
        var result = await _controller.CreateProduct(newProduct);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedProduct = Assert.IsType<Product>(createdAtActionResult.Value);
        Assert.Equal("New Product", returnedProduct.Name);
    }
    
    #region put e delete
    [Fact]
    public async Task UpdateProduct_ReturnsNoContent_WhenSuccessful()
    {
        await ResetDatabase();
        var product = new Product { Id = Guid.NewGuid(), Name = "Old Name", Price = 20 };
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        var updatedProduct = new Product { Id = product.Id, Name = "Updated Name", Price = 25 };
        var result = await _controller.UpdateProduct(product.Id, updatedProduct);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        await ResetDatabase();
        var updatedProduct = new Product { Id = Guid.NewGuid(), Name = "Updated" };
        var result = await _controller.UpdateProduct(updatedProduct.Id, updatedProduct);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent_WhenSuccessful()
    {
        await ResetDatabase();
        var product = new Product { Id = Guid.NewGuid(), Name = "To Delete" };
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        var result = await _controller.DeleteProduct(product.Id);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        await ResetDatabase();
        var result = await _controller.DeleteProduct(Guid.NewGuid());
        Assert.IsType<NotFoundObjectResult>(result);
    }

    #endregion
}
