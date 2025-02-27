namespace Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
  private readonly AppDbContext _context;

  public CategoriesController(AppDbContext context)
  {
    _context = context;
  }

  // GET: api/categories
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
  {
    return await _context.Categories.ToListAsync();
  }

  // GET: api/categories/{id}
  [HttpGet("{id}")]
  public async Task<ActionResult<Category>> GetCategory(Guid id)
  {
    var category = await _context.Categories.FindAsync(id);
    if (category == null)
    {
      return NotFound();
    }
    return category;
  }

  // POST: api/categories
  [HttpPost]
  public async Task<ActionResult<Category>> CreateCategory(Category category)
  {
    _context.Categories.Add(category);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
  }

  // PUT: api/categories/{id}
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateCategory(Guid id, Category category) // ✅ id agora é Guid
  {
    if (category.Id != id) return BadRequest(); // ✅ Comparação correta

    _context.Entry(category).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!_context.Categories.Any(e => e.Id == id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    return NoContent();
  }


  // DELETE: api/categories/{id}
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCategory(int id)
  {
    var category = await _context.Categories.FindAsync(id);
    if (category == null)
    {
      return NotFound();
    }

    _context.Categories.Remove(category);
    await _context.SaveChangesAsync();

    return NoContent();
  }
}
