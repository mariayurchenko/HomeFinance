using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Services.DAL;
using Services.Interfaces;

namespace Services.Repository
{
    public class PaymentsService: IPaymentsService
    {
        private readonly UnitOfWork unitOfWork;
        public PaymentsService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Payment> AllPayments => unitOfWork.PaymentRepository.Get();
        public IEnumerable<Payment> GetDayPayments(DateTime date) => unitOfWork.PaymentRepository.Get().Where(p => p.Date.Day == date.Day && p.Date.Month == date.Month && p.Date.Year == date.Year);
        public IEnumerable<Payment> GetMonthPayments(int month, int year) => unitOfWork.PaymentRepository.Get().Where(p => p.Date.Month == month && p.Date.Year==year);
        public void CreatePayment(Payment payment)
        {
            unitOfWork.PaymentRepository.Insert(payment);
            unitOfWork.Save();
        }

        public void DeletePayment(int id)
        {
            if (unitOfWork.PaymentRepository.GetByID(id) != null)
                unitOfWork.PaymentRepository.Delete(id);
            unitOfWork.Save();
        }
    }
}
