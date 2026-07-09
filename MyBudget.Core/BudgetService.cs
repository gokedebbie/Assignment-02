
namespace MyBudget.Core
{
    public class BudgetService : IBudgetService
    {

        public decimal MonthlyLimit { get; private set; }
        public void SetMonthlyLimit(decimal limit)
        {
            if (limit <= 0 || limit > 1000000)
            {
                throw new InvalidExpenseException(
                    "Monthly limit must be greater than 0 and less than or equal to 1,000,000.");
            }
            
            MonthlyLimit = decimal.Round(limit); 
        }
        public decimal Remaining(decimal totalSpent)
        {
            return MonthlyLimit - totalSpent;
        }
        public BudgetStatus Evaluate(decimal totalSpent)
        {
            if (MonthlyLimit <= 0)
            {
                return BudgetStatus.NotSet;
            }
            else if (Remaining(totalSpent) < 0)
            {
                return BudgetStatus.OverBudget;
            }
            else if (Remaining(totalSpent) < MonthlyLimit * 0.10m)
            {
                return BudgetStatus.AlmostOut;
            }
            else
            {
                return BudgetStatus.OnTrack;
            }
            
        }
    }


}

