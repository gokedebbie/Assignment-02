
namespace MyBudget.Core
{
    public record OneTimeExpense(Guid Id, string Description, decimal Amount, ExpenseCategory Category, DateOnly Date) : 
        Expense(Id, Description, Amount, Category, Date)
    {
        public override decimal MonthlyImpact => Amount;
    
        public override string ToReportLine()

        {
            return $"{Date:yyyy-MM-dd} | {Description,-20} | {Amount,10:C} | {Category} | {GetType().Name}";
        }
    }
}
