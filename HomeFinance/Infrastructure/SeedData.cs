using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class SeedData
    {
        private static Dictionary<string, Expense> expense;
        private static Dictionary<string, Expense> Expenses
        {
            get
            {
                if (expense == null)
                {
                    var list = new Expense[]
                    {
                        new Expense {Name = "Без категории"},
                        new Expense {Name = "Жилье"},
                        new Expense {Name = "Отдых"},
                        new Expense {Name = "Питание"},
                        new Expense {Name = "Покупки"},
                        new Expense {Name = "Связь"},
                        new Expense {Name = "Транспорт"}
                    };

                    expense = new Dictionary<string, Expense>();
                    foreach (Expense el in list)
                        expense.Add(el.Name,el);
                }

                return expense;
            }
        }
        private static Dictionary<string, Income> income;
        private static Dictionary<string, Income> Incomes
        {
            get
            {
                if (income == null)
                {
                    var list = new Income[]
                    {
                        new Income {Name = "Без категории"},
                        new Income {Name = "Зарплата"}
                    };

                    income = new Dictionary<string, Income>();
                    foreach (Income el in list)
                        income.Add(el.Name, el);
                }

                return income;
            }
        }


        public static void Initial(AppDBContext content)
        {
            if (!content.Expense.Any())
                content.Expense.AddRange(Expenses.Select(c => c.Value));

            if (!content.Income.Any())
                content.Income.AddRange(Incomes.Select(c => c.Value));
            
            content.SaveChanges();
        }
    }
}
