using API.Infrastructure.Controller;
using API.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Categories;

[AllowAnonymous]
public class CategoriesController : ExpenseBaseController
{
    public CategoriesController(ExpenseContext context) : base(context) { }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid userId, CreateCategoryRequest request)
    {
        IdentityUser user = await _context.Users.FindAsync(userId)
            ?? throw new Exception($"User Not Found: {userId}");

        Category category = new(user,
            request.Name);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok($"Created Category: {category.Name}");
    }

    [HttpGet]
    public async Task<IActionResult> ReadyAsync(Guid userId)
    {
        IdentityUser user = await _context.Users.FindAsync(userId)
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
        Category category = await _context.Categories.FindAsync(categoryId)
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