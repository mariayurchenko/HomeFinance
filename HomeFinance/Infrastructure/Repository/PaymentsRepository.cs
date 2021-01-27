using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Interfaces;
using Model;

namespace Infrastructure.Repository
{
    public class PaymentsRepository: IPayments
    {
        private readonly AppDBContext appDBContext;

        public PaymentsRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }


        public IEnumerable<Payment> AllPayments => appDBContext.Payment;
        public IEnumerable<Payment> GetDayPayments(DateTime date) => appDBContext.Payment.Where(p => p.Date.Day == date.Day && p.Date.Month == date.Month && p.Date.Year == date.Year);
        public IEnumerable<Payment> GetMonthPayments(int month, int year) => appDBContext.Payment.Where(p => p.Date.Month == month && p.Date.Year==year);
        public void CreatePayment(Payment payment)
        {
            appDBContext.Payment.Add(payment);
            appDBContext.SaveChanges();
        }

        public void DeletePayment(int id)
        {
            var payment = (from s in appDBContext.Payment.ToList() where s.Id == id select s).First();
            if (payment != null)
                appDBContext.Payment.Remove(payment);
            appDBContext.SaveChanges();
        }
    }
}
