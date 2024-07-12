using API.Infrastructure.Controller;
using API.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Accounts;

[AllowAnonymous]
public class AccountsController : ExpenseBaseController
{
    public AccountsController(ExpenseContext context) : base(context) { }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid userId, CreateAccountRequest request)
    {
        IdentityUser user = await _context.Users.FindAsync(userId)
            ?? throw new Exception($"User Not Found: {userId}");

        Account account = new(user,
            request.Name);

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return Ok($"Created Account: {account.Name}");
    }

    [HttpGet]
    public async Task<IActionResult> ReadyAsync(Guid userId)
    {
        IdentityUser user = await _context.Users.FindAsync(userId)
            ?? throw new Exception($"User Not Found: {userId}");

        var accounts = await _context.Accounts
            .AsNoTracking()
            .Include(a => a.Transactions)
            .Where(a => a.UserId == userId.ToString())
            .Select(a => new ReadAccountVm
            {
                Id = a.Id,
                Name = a.Name,
                TotalTransaction = a.Transactions.Count,
                TotalAmount = a.Transactions.Sum(t => t.Amount)
            })
            .ToListAsync();

        return Ok(accounts);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid accountId)
    {
        Account account = await _context.Accounts.FindAsync(accountId)
            ?? throw new Exception($"Account Not Found: {accountId}");

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        return Ok($"Deleted Account");
    }
}

public record CreateAccountRequest
{
    public required string Name { get; init; }
}

public record ReadAccountVm
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required int TotalTransaction { get; init; }
    public required decimal TotalAmount { get; init; }
}
