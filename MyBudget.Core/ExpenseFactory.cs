
using MyBudget.Core;

namespace MyBudget.Core
{
    public static class ExpenseFactory
    {

        public static string ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new InvalidExpenseException("Description cannot be empty.");
            }
            return description.Trim();
        }

        public static decimal ValidateAmount(decimal amount)
        {
            if (amount <= 0 || amount > 1000000)
            {
                throw new InvalidExpenseException(
                    "Amount must be greater than 0 and less than or equal to 1,000,000.");
            }

            return Math.Round(amount, 2);

        }

        public static OneTimeExpense CreateOneTime(String Description, Decimal Amount, ExpenseCategory Category, DateOnly Date)
        {
            return new OneTimeExpense(Guid.NewGuid(), ValidateDescription(Description), ValidateAmount(Amount), Category, Date);
        }

        public static RecurringExpense CreateRecurring(String Description, Decimal Amount, ExpenseCategory Category, DateOnly Date, int TimesPerMonth)
        {
            if (TimesPerMonth <= 0 || TimesPerMonth > 31)
            {
                throw new InvalidExpenseException(
                    "Frequency must be greater than 0 and less than or equal to 31.");
            }
            return new RecurringExpense(Guid.NewGuid(), ValidateDescription(Description), ValidateAmount(Amount), Category, Date, TimesPerMonth);
        }

    }
}