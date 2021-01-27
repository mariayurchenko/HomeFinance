using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DAL;
using Xunit;


namespace Test
{
    public class ServicesTest
    {
        [Fact]
        public void DeleteExpense_NoExpenseIsDeleted_WhenExpenseIsNotFound()
        {
            using (var db = new AppDBContext(Utilities.TestDbContextOptions()))
            {
                SeedData.Initial(db);
                UnitOfWork unitOfWork = new UnitOfWork(db);
                var recId = 50;
                var expected = db.Expense.AsNoTracking().ToList(); 
                try
                {
                    unitOfWork.ExpenseRepository.Delete(unitOfWork.ExpenseRepository.GetByID(recId));
                }
                catch
                {
                    // recId doesn't exist
                }
                unitOfWork.Save();
                
                var actual = unitOfWork.ExpenseRepository.Get().ToList();
                Assert.Equal(
                    expected.OrderBy(m => m.Id).Select(m => m.Name),
                    actual.OrderBy(m => m.Id).Select(m => m.Name));
            }
        }

        [Fact]
        public void DeleteExpense_NoExpenseIsDeleted_WhenExpenseIsFound()
        {
            using (var db = new AppDBContext(Utilities.TestDbContextOptions()))
            {
                SeedData.Initial(db);
                UnitOfWork unitOfWork = new UnitOfWork(db);
                db.SaveChanges();   
                var students = db.Expense.AsNoTracking().ToList();
                var recId = 1;

                var expected = students.Where(expense => expense.Id != recId).ToList();

                unitOfWork.ExpenseRepository.Delete(unitOfWork.ExpenseRepository.GetByID(recId));
                unitOfWork.Save();

                var actual = unitOfWork.ExpenseRepository.Get().ToList();
                Assert.Equal(
                        expected.OrderBy(m => m.Id).Select(m => m.Name),
                        actual.OrderBy(m => m.Id).Select(m => m.Name));
            }
        }
    }
}
