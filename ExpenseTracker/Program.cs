// =====================================================================
//  Program.cs  —  the interactive console UI for MyBudget (Assignment 1).
//  Target framework: .NET 10 (LTS), language C# 14.
//
//  >>> BUILD THE MENU-DRIVEN UI HERE (Modules 1-3). <<<
//
//  Once you have implemented BudgetRules.cs (so the unit tests pass), wire it
//  up to a console interface that meets the assignment brief:
//
//    * Print a banner (try a raw string literal).
//    * Loop a menu until the user exits, using a switch on the choice:
//        1) Add an expense   2) View summary   3) Set monthly budget   4) Exit
//    * Read and VALIDATE input, re-prompting on bad data (decimal.TryParse,
//      BudgetRules.NormalizeCategory, a date parse, non-empty text).
//    * Keep running totals in simple variables (no collections / no classes).
//    * Use BudgetRules.ValidateAmount / ClassifyAmount / BudgetStatus /
//      FormatCurrency for all logic and formatting.
//    * Handle bad input with try / catch / finally and InvalidExpenseException.
//
//  See section 6 of the assignment brief for a sample run to aim for.
// =====================================================================
using ExpenseTracker;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.Design;

int result = 0;
string option = "";
string description = "";
decimal expense = 0.00m;
decimal monthlyBudget = 0.00m;
decimal totalExpense = 0.00m;
bool wantsToContinue = true;
string? category = "";
string? categoryNormalize = null;
DateTime ExpenseDay = DateTime.Now;

decimal foodExpense = 0.00m;
decimal transportExpense = 0.00m;
decimal utilitiesExpense = 0.00m;
decimal entertainmentExpense = 0.00m;
decimal otherExpense = 0.00m;

decimal budget = 0.00m;
decimal remaining = 0.00m;
do
{
    Console.WriteLine("1) Add an expense   2) View summary   3) Set monthly budget   4) Exit");
    option = Console.ReadLine();
    if (!int.TryParse(option, out result))
    {
        Console.WriteLine("output the right option");
        //throw new Exception("Invalidated income");       
    }

    switch (result)

    {
        case 1:
            Console.WriteLine("Please, output detail expense");
            description = Console.ReadLine();
            //Console.Clear();
            Console.WriteLine("How much is the expense ?");
            bool isValidItem = (!decimal.TryParse(Console.ReadLine(), out expense));

            try
            {
                expense = BudgetRules.ValidateAmount(expense);
                totalExpense += expense;
            }
            catch (InvalidExpenseException er)
            {
                Console.WriteLine($"{er.Message}");
            }


            while (true)
            {
                Console.WriteLine("Please, What Category ?");
                Console.WriteLine("Category    : [Food/Transport/Utilities/Entertainment/Other]");
                category = Console.ReadLine();
                categoryNormalize = BudgetRules.NormalizeCategory(category);
                if (categoryNormalize != null)
                {
                    category = categoryNormalize;
                    Console.WriteLine($"Added: {category}");
                    break;
                }
                else
                {
                    Console.WriteLine("output valid category");
                }
            }
            while (true)
            {
                Console.Write("Enter date (YYYY-MM-DD) or make entry the for today: ");
                string stringDate = Console.ReadLine();
                if (stringDate == "")
                {
                    Console.WriteLine($"Your Date is {ExpenseDay.ToString("yyyy-MM-dd")}");
                    break;

                }
                else if (DateTime.TryParse(stringDate, out ExpenseDay))
                {
                    Console.WriteLine($"Your Date is {ExpenseDay.ToString("yyyy-MM-dd")}");
                    break;
                }
                else
                {
                    Console.WriteLine("Please, output the correct Date ");
                }
            }

            Console.Write("Note (optional):");
            string userNote = Console.ReadLine();

            Console.WriteLine($"Recorded: {expense:c2} | {category}| {ExpenseDay}");


            if (category == "Food") { foodExpense += expense; }
            else if (category == "Transport") { transportExpense += expense; }
            else if (category == "Utilities") { utilitiesExpense += expense; }
            else if (category == "Entertainment") { entertainmentExpense += expense; }
            else if (category == "Other") { otherExpense += expense; }

            try
            {
                remaining = (monthlyBudget - totalExpense);
                string status = BudgetRules.BudgetStatus(remaining, monthlyBudget);
                Console.WriteLine(status);
            }
            catch (InvalidExpenseException er)
            {
                Console.WriteLine(er.Message);
            }


            break;
        case 2:
            Console.WriteLine($"""
                Food Total:{foodExpense}
                Transport Total:{transportExpense}
                Utitlities Total:{utilitiesExpense}
                Entertainment Total:{entertainmentExpense}
                Other Total: {otherExpense}

                Total : {totalExpense}
                """);
            break;
        case 3:
            while (true)
            {
                Console.Write("Please enter the monthly budget: ");
                bool woof = (!decimal.TryParse(Console.ReadLine(), out monthlyBudget));

                try
                {
                    monthlyBudget = BudgetRules.ValidateAmount(monthlyBudget);
                    break;
                }
                catch (InvalidExpenseException er)
                {
                    Console.WriteLine($"{er.Message}");
                }
                try
                {
                    remaining = (monthlyBudget - totalExpense);
                    string status = BudgetRules.BudgetStatus(remaining, monthlyBudget);
                    Console.WriteLine(status);
                }
                catch (InvalidExpenseException er)
                {
                    Console.WriteLine(er.Message);
                }
            }
            break;
        case 4:
            wantsToContinue = false;
            break;
        default:
            Console.WriteLine("input a valid number");
            break;
    }

} while (wantsToContinue); //(result <= 1 || result >= 4) ||;

Console.Clear();

Console.WriteLine("Thank you for using the Expense Tracker! Bye.");

//Console.WriteLine("MyBudget — TODO: make an interactive menu UI (see the brief).");

// Delete the line above and implement the application here.
