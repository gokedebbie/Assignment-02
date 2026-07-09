
namespace MyBudget.Core
{
    public class ExpenseRepository : IExpenseRepository
    {

        // Private backing field
        private readonly List<Expense> _expenses;

        // Store used for loading and saving expenses
        private readonly IExpenseStore _store;

        // Constructor
        public ExpenseRepository(IExpenseStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));

            // Load existing expenses from the store
            _expenses = _store.Load().ToList();
        }

        // Returns all expenses ordered by date
        public IReadOnlyList<Expense> GetAll()
        {
            return _expenses;
        }

        // Adds a new expense
        public void Add(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            _expenses.Add(expense);
        }

        // Returns the total monthly impact of all expenses
        public decimal Total()
        {
            return _expenses.Sum(e => e.MonthlyImpact);
        }

        // Returns totals grouped by category
        public IReadOnlyDictionary<ExpenseCategory, decimal> TotalsByCategory()
        {
            return _expenses
                .GroupBy(e => e.Category)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(e => e.MonthlyImpact)
                );
        }

        // Returns expenses in a specific category ordered by date
        public IReadOnlyList<Expense> InCategory(ExpenseCategory category)
        {
            return _expenses
                .Where(e => e.Category == category)
                .OrderBy(e => e.Date).ToList();
        }

        // Saves all expenses through the store
        public void Save()
        {
            _store.Save(_expenses);
        }
    }
}