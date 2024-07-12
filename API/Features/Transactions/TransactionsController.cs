using API.Infrastructure.Controller;
using API.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Transactions;

public class TransactionsController : ExpenseBaseController
{
    public TransactionsController(ExpenseContext context) : base(context) { }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid accountId, Guid categoryId, CreateTransactionRequest request)
    {
        var account = await _context.Accounts.FindAsync(accountId)
            ?? throw new Exception($"Account Not Found: {accountId}");

        var category = await _context.Categories.FindAsync(categoryId)
            ?? throw new Exception($"Category Not Found: {accountId}");

        Transaction transaction = new(account,
            category,
            request.Description,
            request.Date,
            request.Amount);

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok($"Created Transaction: {category.Name}");
    }

    [HttpGet]
    public async Task<IActionResult> ReadyAsync(Guid accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId)
            ?? throw new Exception($"Account Not Found: {accountId}");

        var transactions = await _context.Transactions
            .AsNoTracking()
            .Include(t => t.Category)
            .Where(t => t.AccountId == accountId)
            .Select(t => new ReadTransactionVm
            {
                Id = t.Id,
                Category = t.Category.Name,
                Description = t.Description,
                Date = t.Date,
                Amount = t.Amount
            })
            .ToListAsync();

        return Ok(transactions);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid transactionId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId)
            ?? throw new Exception($"Transaction Not Found: {transactionId}");

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return Ok($"Deleted Transaction");
    }
}

public record CreateTransactionRequest
{
    public required string Description { get; init; }
    public required DateOnly Date { get; init; }
    public required decimal Amount { get; init; }
}

public record ReadTransactionVm
{
    public required Guid Id { get; init; }
    public required string Category { get; init; }
    public required string Description { get; init; }
    public required DateOnly Date { get; init; }
    public required decimal Amount { get; init; }
}
