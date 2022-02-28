using System.ComponentModel.DataAnnotations;

namespace EFDatabaseLibrary.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
