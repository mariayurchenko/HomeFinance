using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
