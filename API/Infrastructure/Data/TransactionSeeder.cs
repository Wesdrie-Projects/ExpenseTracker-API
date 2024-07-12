using API.Domain;

namespace API.Infrastructure.Data;

public class TransactionSeeder
{
    public Transaction Salary { get; set; } = null!;
    public Transaction SavingsInterest { get; set; } = null!;
    public Transaction CreditInterest { get; set; } = null!;
    public Transaction Medical { get; set; } = null!;
    public Transaction CarPayment { get; set; } = null!;

    public TransactionSeeder(AccountSeeder accountSeeder, CategorySeeder categorySeeder)
    {
        Salary = new(accountSeeder.Debit,
            categorySeeder.Income,
            DateOnly.FromDateTime(DateTime.Now.AddDays(-25)),
            61056.25m);
        SavingsInterest = new(accountSeeder.Debit,
            categorySeeder.Interest,
            DateOnly.FromDateTime(DateTime.Now.AddDays(-23)),
            137.56m);
        CreditInterest = new(accountSeeder.Credit,
            categorySeeder.Debt,
            DateOnly.FromDateTime(DateTime.Now.AddDays(-23)),
            -99.41m);
        Medical = new(accountSeeder.Debit,
            categorySeeder.Bills,
            DateOnly.FromDateTime(DateTime.Now.AddDays(-21)),
            -1299.89m);
        CarPayment = new(accountSeeder.Debit,
            categorySeeder.Bills,
            DateOnly.FromDateTime(DateTime.Now.AddDays(-20)),
            -3789.45m);
    }

    public void Seed(ExpenseContext context)
    {
        context.Transactions.Add(Salary);
        context.Transactions.Add(SavingsInterest);
        context.Transactions.Add(CreditInterest);
        context.Transactions.Add(Medical);
        context.Transactions.Add(CarPayment);
    }
}
