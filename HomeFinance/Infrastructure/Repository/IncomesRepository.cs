using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Interfaces;
using Model;

namespace Infrastructure.Repository
{
    public class IncomesRepository : IIncomes
    {
        private readonly AppDBContext appDBContext;
        public IncomesRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        public IEnumerable<Income> AllIncomes => appDBContext.Income;
        public Income GetIncome(int id) => appDBContext.Income.FirstOrDefault(p => p.Id == id);
        public void CreateIncome(Income income)
        {
            appDBContext.Income.Add(income);
            appDBContext.SaveChanges();
        }

        public void DeleteIncome(int id)
        {
            var sum = from s in appDBContext.Admission.ToList() where s.IncomeID == id select s;
            var income = (from s in appDBContext.Income.ToList() where s.Id == id select s).First();

            if (!sum.Any())
            {
                if (income != null)
                {
                    appDBContext.Income.Remove(income);
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
