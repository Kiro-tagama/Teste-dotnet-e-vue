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

public class CategoriesControllerTests
{
  private AppDbContext GetDbContext()
  {
    var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    var dbContext = new AppDbContext(options);
    dbContext.Categories.AddRange(new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Category 1", Description = "Desc 1" },
            new Category { Id = Guid.NewGuid(), Name = "Category 2", Description = "Desc 2" }
        });
    dbContext.SaveChanges();

    return dbContext;
  }

  [Fact]
  public async Task GetCategories_ReturnsAllCategories()
  {
    var dbContext = GetDbContext();
    var controller = new CategoriesController(dbContext);

    var result = await controller.GetCategories();

    Assert.NotNull(result);
    Assert.Equal(2, result.Value.Count());
  }

  [Fact]
  public async Task GetCategoryById_ReturnsCategory_WhenFound()
  {
    var dbContext = GetDbContext();
    var category = dbContext.Categories.First();
    var controller = new CategoriesController(dbContext);

    var result = await controller.GetCategory(category.Id);

    var actionResult = Assert.IsType<ActionResult<Category>>(result);
    var returnedCategory = Assert.IsType<Category>(actionResult.Value);
    Assert.Equal(category.Id, returnedCategory.Id);
  }

  [Fact]
  public async Task GetCategoryById_ReturnsNotFound_WhenCategoryDoesNotExist()
  {
    var dbContext = GetDbContext();
    var controller = new CategoriesController(dbContext);

    var result = await controller.GetCategory(Guid.NewGuid());

    var actionResult = Assert.IsType<ActionResult<Category>>(result);
    Assert.IsType<NotFoundResult>(result.Result);
  }

  [Fact]
  public async Task CreateCategory_ReturnsCreatedAtAction()
  {
    var dbContext = GetDbContext();
    var controller = new CategoriesController(dbContext);

    var newCategory = new Category { Id = Guid.NewGuid(), Name = "New Category", Description = "New Desc" };
    var result = await controller.CreateCategory(newCategory);

    var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
    var createdCategory = Assert.IsType<Category>(actionResult.Value);
    Assert.Equal(newCategory.Id, createdCategory.Id);
  }

  #region  put e delete

  [Fact]
  public async Task UpdateCategory_ReturnsNoContent_WhenSuccessful()
  {
    using var dbContext = GetDbContext();

    var category = new Category { Id = Guid.NewGuid(), Name = "Test Category", Description = "Test Desc" };
    dbContext.Categories.Add(category);
    await dbContext.SaveChangesAsync();

    dbContext.Entry(category).State = EntityState.Detached;

    var updatedCategory = new Category { Id = category.Id, Name = "Updated Name", Description = "Updated Desc" };

    var controller = new CategoriesController(dbContext);

    var result = await controller.UpdateCategory(category.Id, updatedCategory);

    Assert.IsType<NoContentResult>(result);
  }

  [Fact]
  public async Task UpdateCategory_ReturnsBadRequest_WhenIdsDoNotMatch()
  {
    var dbContext = GetDbContext();
    var controller = new CategoriesController(dbContext);
    var result = await controller.UpdateCategory(Guid.NewGuid(), new Category { Id = Guid.NewGuid(), Name = "Invalid Update" });

    Assert.IsType<BadRequestResult>(result);
  }

  [Fact]
  public async Task DeleteCategory_ReturnsNoContent_WhenSuccessful()
  {
    var dbContext = GetDbContext();
    var controller = new CategoriesController(dbContext);
    var categoryId = dbContext.Categories.First().Id;

    var result = await controller.DeleteCategory(categoryId);
    Assert.IsType<NoContentResult>(result);
  }

  [Fact]
  public async Task DeleteCategory_ReturnsNotFound_WhenNotFound()
  {
    var dbContext = GetDbContext();
    var controller = new CategoriesController(dbContext);
    var result = await controller.DeleteCategory(Guid.NewGuid());

    Assert.IsType<NotFoundResult>(result);
  }

  #endregion
}