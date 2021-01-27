using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Interfaces;
using Model;

namespace Infrastructure.Repository
{
    public class ExpensesRepository: IExpenses
    {
        private readonly AppDBContext appDBContext;

        public ExpensesRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public IEnumerable<Expense> AllExpenses => appDBContext.Expense;
        public Expense GetExpense(int id) => appDBContext.Expense.FirstOrDefault(p => p.Id == id);

        public void CreateExpense(Expense expense)
        {
            appDBContext.Expense.Add(expense);
            appDBContext.SaveChanges();
        }

        public void DeleteExpense(int id)
        {
            var sum = from s in appDBContext.Payment.ToList() where s.ExpenseID == id select s;
            var expence = (from s in appDBContext.Expense.ToList() where s.Id == id select s).First();

            if (!sum.Any()) { 
                if (expence!=null)
                {
                    appDBContext.Expense.Remove(expence);
                }
            }
            else
            {
                throw new ArgumentException("It is impossible to delete a expence if it contains payments");
            }
            appDBContext.SaveChanges();
        }
    }
}
