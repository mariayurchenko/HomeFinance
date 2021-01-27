using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CalculateBalance
    {
        private decimal admission;
        private decimal payment;
        private decimal balance;
        private CashAccount cashAccount;
        public CalculateBalance(List<Admission> admissions, List<Payment> payments)
        {
            Calculate(admissions, payments);
            cashAccount = new CashAccount { Admission = admission, Balance = balance, Payment = payment };
        }
        public CashAccount GetCashAccount()
        {
            return cashAccount;
        }
        private void Calculate(List<Admission> admissions, List<Payment> payments)
        {
            admission = 0;
            payment = 0;
            if (admissions != null) 
            { 
                foreach (Admission admission in admissions)
                {
                    this.admission += admission.Amount;
                } 
            }
            if (payments != null)
            {
                foreach (Payment payment in payments)
                {
                    this.payment += payment.Amount;
                }
            }
            balance = admission - payment;
        }
    }
}
