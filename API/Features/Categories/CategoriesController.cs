using API.Infrastructure.Controller;
using API.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Features.Categories;

public class CategoriesController : ExpenseBaseController
{
    public CategoriesController(ExpenseContext context) : base(context) { }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCategoryRequest request)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
            ?? throw new Exception("Unable To Get User Information");

        var user = await _context.Users.FindAsync(userId)
            ?? throw new Exception($"User Not Found: {userId}");

        Category category = new(user, request.Name);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok($"Created Category: {category.Name}");
    }

    [HttpGet]
    public async Task<IActionResult> ReadyAsync()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
            ?? throw new Exception("Unable To Get User Information");

        var user = await _context.Users.FindAsync(userId)
            ?? throw new Exception($"User Not Found: {userId}");

        var categories = await _context.Categories
            .AsNoTracking()
            .Where(c => c.UserId == userId.ToString())
            .Select(c => new ReadCategoryVm
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

        return Ok(categories);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId)
            ?? throw new Exception($"Category Not Found: {categoryId}");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return Ok($"Deleted Category");
    }
}

public record CreateCategoryRequest
{
    public required string Name { get; init; }
}

public record ReadCategoryVm
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}