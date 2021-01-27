using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int ExpenseID { get; set; }
        public string Description { get; set; }
        public Expense Expense { get; set; }
    }
}
