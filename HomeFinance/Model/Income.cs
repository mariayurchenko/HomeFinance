using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Income
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
