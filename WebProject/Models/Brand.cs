using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public Brand() { }

    }
}
