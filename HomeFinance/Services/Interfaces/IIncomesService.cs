using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Services.Interfaces
{
    public interface IIncomesService
    {
        IEnumerable<Income> AllIncomes { get; }
        Income GetIncome(int id);
        void CreateIncome(Income income);
        void DeleteIncome(int id);
    }
}
