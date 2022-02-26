using PhonebookAndASPNETMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookAndASPNETMVC.Models
{
    public static class DefaultContactPicture
    {
        public static void Initialize(ApplicationDbContext db)
        {
            if(!db.Picture.Any())
            {
                db.Picture.AddRange(new Picture() { Name = "defaultAvatar.png", Path = "/pic/defaultAvatar.png" });
                db.SaveChanges();
            }
        }
    }
}
