using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookAndASPNETMVC.Models
{
    public class NewContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
        public IFormFile PictureFile { get; set; }
    }
}
