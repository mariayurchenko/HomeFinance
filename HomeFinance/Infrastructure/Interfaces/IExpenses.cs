using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Infrastructure.Interfaces
{
    public interface IExpenses
    {
        IEnumerable<Expense> AllExpenses { get; }
        Expense GetExpense(int id);
        void CreateExpense(Expense expense);
        void DeleteExpense(int id);
    }
}
