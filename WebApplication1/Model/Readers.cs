using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Readers
    {
        [Key]
        public int Id_Reader { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
