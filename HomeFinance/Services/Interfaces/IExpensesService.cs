using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Services.Interfaces
{
    public interface IExpensesService
    {
        IEnumerable<Expense> AllExpenses { get; }
        Expense GetExpense(int id);
        void CreateExpense(Expense expense);
        void DeleteExpense(int id);
    }
}
