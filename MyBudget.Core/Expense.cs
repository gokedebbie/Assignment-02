
namespace MyBudget.Core
{
    public abstract record Expense(Guid Id, string Description, decimal Amount, ExpenseCategory Category, DateOnly Date) : IReportable

    {
        public abstract decimal MonthlyImpact { get; }
        public virtual string ToReportLine()
        {
            return $"{Date:yyyy-MM-dd} | {Description,-20} | {Amount,10:C} | {Category} | {GetType().Name}";
        }
    
    }
}
