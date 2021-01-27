using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Admission
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int IncomeID { get; set; }
        public string Description { get; set; }
        public Income Income { get; set; }
    }
}
