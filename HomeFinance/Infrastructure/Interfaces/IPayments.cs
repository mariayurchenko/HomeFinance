using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Infrastructure.Interfaces
{
    public interface IPayments
    {
        IEnumerable<Payment> AllPayments { get; }
        IEnumerable<Payment> GetDayPayments(DateTime date);
        IEnumerable<Payment> GetMonthPayments(int month, int year);
        void CreatePayment(Payment payment);
        void DeletePayment(int id);
    }
}
