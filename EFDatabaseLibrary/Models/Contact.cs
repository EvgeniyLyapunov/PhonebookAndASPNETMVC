using System.ComponentModel.DataAnnotations;

namespace EFDatabaseLibrary.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
        public int PictureId { get; set; }
    }
}
