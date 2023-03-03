using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public Model() { }
    }
}
