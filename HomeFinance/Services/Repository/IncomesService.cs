using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Services.DAL;
using Services.Interfaces;

namespace Services.Repository
{
    public class IncomesService : IIncomesService
    {  
        private readonly UnitOfWork unitOfWork;
        public IncomesService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Income> AllIncomes => unitOfWork.IncomeRepository.Get();
        public Income GetIncome(int id) => unitOfWork.IncomeRepository.Get().FirstOrDefault(p => p.Id == id);
        public void CreateIncome(Income income)
        {
            unitOfWork.IncomeRepository.Insert(income);
            unitOfWork.Save();
        }

        public void DeleteIncome(int id)
        {
            var sum = from s in unitOfWork.AdmissionRepository.Get().ToList() where s.IncomeID == id select s;

            if (!sum.Any())
            {
                if (unitOfWork.IncomeRepository.GetByID(id) != null)
                {
                    unitOfWork.IncomeRepository.Delete(id);
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
