using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Services.DAL;
using Services.Interfaces;

namespace Services.Repository
{
    public class ExpensesService: IExpensesService
    {
        private readonly UnitOfWork unitOfWork;

        public ExpensesService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Expense> AllExpenses => unitOfWork.ExpenseRepository.Get();
        public Expense GetExpense(int id) => unitOfWork.ExpenseRepository.Get().FirstOrDefault(p => p.Id == id);

        public void CreateExpense(Expense expense)
        {
            unitOfWork.ExpenseRepository.Insert(expense);
            unitOfWork.Save();
        }

        public void DeleteExpense(int id)
        {
            var sum = from s in unitOfWork.PaymentRepository.Get().ToList() where s.ExpenseID == id select s;

            if (!sum.Any()) { 
                if (unitOfWork.ExpenseRepository.GetByID(id) != null)
                {
                    unitOfWork.ExpenseRepository.Delete(id);
                }
            }
            else
            {
                throw new ArgumentException("It is impossible to delete a expence if it contains payments");
            }
            unitOfWork.Save();
        }
    }
}
