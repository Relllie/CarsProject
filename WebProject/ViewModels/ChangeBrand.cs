using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels
{
    public class ChangeBrand
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public ChangeBrand() { }
    }
}
