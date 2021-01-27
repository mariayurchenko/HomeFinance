using Infrastructure;
using Infrastructure.Repository;
using Model;
using System;

namespace Services.DAL
{
    public class UnitOfWork : IDisposable
    {
        private AppDBContext context;
        private GenericRepository<Admission> admissionRepository;
        private GenericRepository<Expense> expenseRepository;
        private GenericRepository<Income> incomeRepository;
        private GenericRepository<Payment> paymentRepository;

        public UnitOfWork(AppDBContext context)
        {
            this.context = context;
        }

        public GenericRepository<Admission> AdmissionRepository
        {
            get
            {

                if (this.admissionRepository == null)
                {
                    this.admissionRepository = new GenericRepository<Admission>(context);
                }
                return admissionRepository;
            }
        }

        public GenericRepository<Expense> ExpenseRepository
        {
            get
            {

                if (this.expenseRepository == null)
                {
                    this.expenseRepository = new GenericRepository<Expense>(context);
                }
                return expenseRepository;
            }
        }
        public GenericRepository<Income> IncomeRepository
        {
            get
            {

                if (this.incomeRepository == null)
                {
                    this.incomeRepository = new GenericRepository<Income>(context);
                }
                return incomeRepository;
            }
        }
        public GenericRepository<Payment> PaymentRepository
        {
            get
            {

                if (this.paymentRepository == null)
                {
                    this.paymentRepository = new GenericRepository<Payment>(context);
                }
                return paymentRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
