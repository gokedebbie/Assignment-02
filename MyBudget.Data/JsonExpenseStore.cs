
using MyBudget.Core;
using System.Text.Json;

namespace MyBudget.Data
{
    public class JsonExpenseStore : IExpenseStore
    {
        // Stores the file path
        private readonly string _path;

        // Constructor
        public JsonExpenseStore(string path)
        {
            _path = path;
        }

        // Load expenses from the JSON file
        public IReadOnlyList<Expense> Load()
              
        {
            // Return an empty list if the file doesn't exist
            if (!File.Exists(_path))
            {
                return new List<Expense>();
            }

            string json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Expense>>(json) ?? Enumerable.Empty<Expense>().ToList();
            // Return an empty list if the file is empty
            //else if (string.IsNullOrWhiteSpace(json))
            //{
            //    return new List<Expense>();
            //}

            // Deserialize the JSON into a List<Expense>
            //List<Expense>? expenses = JsonSerializer.Deserialize<List<Expense>>(json);

            // Return an empty list if deserialization returns null
            //return expenses ?? new List<Expense>();
        }

        // Save expenses to the JSON file
        public void Save(IEnumerable<Expense> expenses)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(expenses, options);

            File.WriteAllText(_path, json);
        }
    }
}