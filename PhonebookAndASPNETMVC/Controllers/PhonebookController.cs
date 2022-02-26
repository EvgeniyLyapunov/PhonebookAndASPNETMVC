using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonebookAndASPNETMVC.Data;
using PhonebookAndASPNETMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookAndASPNETMVC.Controllers
{
    public class PhonebookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public PhonebookController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult AllContacts(string searchQuery)
        {
            List<FullContact> allContacts = new List<FullContact>();
            if (_db.Contact.ToList().Count == 0)
            {
                return View(allContacts);
            }

            if (_db.Contact.ToList().Count != 0)
            {
                allContacts = _db.Contact.Join(_db.Picture,
                contact => contact.PictureId,
                picture => picture.Id,
                (contact, picture) => new FullContact
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    MiddleName = contact.MiddleName,
                    PhoneNumber = contact.PhoneNumber,
                    Address = contact.Address,
                    Info = contact.Info,
                    PictureName = picture.Name,
                    PicturePath = picture.Path
                }).ToList();
            }

            if (searchQuery != null)
            {
                allContacts = allContacts.Where(
                    p => p.LastName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.FirstName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                return View(allContacts);
            }

            return View(allContacts);
        }

        public IActionResult SearchContacts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            return RedirectToAction("AllContacts", new { searchQuery = query });
        }



        //GET - NewContact
        public IActionResult NewContact()
        {
            return View();
        }

        //POST - NewContact
        [HttpPost]
        public async Task<IActionResult> NewContact(NewContact obj)
        {
            if (obj.PictureFile != null)
            {
                // путь к папке pic
                string path = "/pic/" + obj.PictureFile.FileName;

                // сохраняем файл в папку pic в каталоге wwwroot
                using (FileStream fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    await obj.PictureFile.CopyToAsync(fileStream);
                }

                 _db.Picture.Add(new Picture()
                {
                    Name = obj.PictureFile.FileName,
                    Path = path
                });
                _db.SaveChanges();

                _db.Contact.Add(new Contact()
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    MiddleName = obj.MiddleName,
                    PhoneNumber = obj.PhoneNumber,
                    Address = obj.Address,
                    Info = obj.Info,
                    PictureId = _db.Picture.FirstOrDefault(p => p.Name == obj.PictureFile.FileName).Id
                });
                _db.SaveChanges();
            }
            else
            {
                _db.Contact.Add(new Contact()
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    MiddleName = obj.MiddleName,
                    PhoneNumber = obj.PhoneNumber,
                    Address = obj.Address,
                    Info = obj.Info,
                    PictureId = 1 
                });
                _db.SaveChanges();
            }
            return RedirectToAction("AllContacts");
        }

        public IActionResult Contact(int? id)
        {
            Contact contact = _db.Contact.FirstOrDefault(p => p.Id == id);
            Picture picture = _db.Picture.FirstOrDefault(p => p.Id == contact.PictureId);
            FullContact fullContact = new FullContact()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                MiddleName = contact.MiddleName,
                PhoneNumber = contact.PhoneNumber,
                Address = contact.Address,
                Info = contact.Info,
                PicturePath = picture.Path
            };
            return View(fullContact);
        }

        //GET - EditContact
        public IActionResult EditContact(int? id)
        {
            Contact contact = _db.Contact.FirstOrDefault(p => p.Id == id); ;
            Picture picture = _db.Picture.FirstOrDefault(p => p.Id == contact.PictureId);
            FullContact fullContact = new FullContact()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                MiddleName = contact.MiddleName,
                PhoneNumber = contact.PhoneNumber,
                Address = contact.Address,
                Info = contact.Info,
                PicturePath = picture.Path
            };
            ViewBag.SelectedContact = fullContact;
            return View();
        }

        //POST - EditContact
        [HttpPost]
        public async Task<IActionResult> EditContactAsync(int? id, NewContact obj)
        {
            if(id != null)
            {
                Contact contact = _db.Contact.FirstOrDefault(p => p.Id == id);
                Picture picture = _db.Picture.FirstOrDefault(p => p.Id == contact.PictureId);

                //Пользователь меняет не дефолтную картинку на другую
                //Здесь удаляется файл старой картинки
                if (obj.PictureFile != null && contact.PictureId != 1)
                {
                    FileInfo file = new FileInfo(_env.WebRootPath + picture.Path);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }

                //Пользователь меняет не дефолтную картинку на другую
                //Сохраняется новый файл картинки и записывается о нём инфа в таблицу картинок по старому Id
                if (obj.PictureFile != null && contact.PictureId != 1)
                { 
                    string path = "/pic/" + obj.PictureFile.FileName;
                    using (FileStream fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                    {
                        await obj.PictureFile.CopyToAsync(fileStream);
                    }

                    picture.Name = obj.PictureFile.FileName;
                    picture.Path = path;
                    _db.Picture.Update(picture);
                    _db.SaveChanges();

                    if (contact.FirstName != obj.FirstName) contact.FirstName = obj.FirstName;
                    if (contact.LastName != obj.LastName) contact.LastName = obj.LastName;
                    if (contact.MiddleName != obj.MiddleName) contact.MiddleName = obj.MiddleName;
                    if (contact.PhoneNumber != obj.PhoneNumber) contact.PhoneNumber = obj.PhoneNumber;
                    if (contact.Address != obj.Address) contact.Address = obj.Address;
                    if (contact.Info != obj.Info && obj.Info != null) contact.Info = obj.Info;
                    _db.Contact.Update(contact);
                    _db.SaveChanges();
                }
                //Пользователь меняет дефолтную картинку на другую
                //Сохраняется новый файл картинки, создаётся новая запись в таблице картинок
                else if (obj.PictureFile != null && contact.PictureId == 1)
                {
                    string path = "/pic/" + obj.PictureFile.FileName;
                    using (FileStream fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                    {
                        await obj.PictureFile.CopyToAsync(fileStream);
                    }

                    _db.Picture.Add(new Picture()
                    {
                        Name = obj.PictureFile.FileName,
                        Path = path
                    });
                    _db.SaveChanges();

                    //Получаем только что сохранённую запись из таблицы картинок
                    //и перезаписываем Id в поле PicturId редактируемого контакта
                    Picture newPicture = _db.Picture.FirstOrDefault(p => p.Name == obj.PictureFile.FileName);

                    if (contact.FirstName != obj.FirstName) contact.FirstName = obj.FirstName;
                    if (contact.LastName != obj.LastName) contact.LastName = obj.LastName;
                    if (contact.MiddleName != obj.MiddleName) contact.MiddleName = obj.MiddleName;
                    if (contact.PhoneNumber != obj.PhoneNumber) contact.PhoneNumber = obj.PhoneNumber;
                    if (contact.Address != obj.Address) contact.Address = obj.Address;
                    if (contact.Info != obj.Info && obj.Info != null) contact.Info = obj.Info;
                    contact.PictureId = newPicture.Id;
                    _db.Contact.Update(contact);
                    _db.SaveChanges();
                }
                else if (obj.PictureFile == null)
                {
                    if (contact.FirstName != obj.FirstName) contact.FirstName = obj.FirstName;
                    if (contact.LastName != obj.LastName) contact.LastName = obj.LastName;
                    if (contact.MiddleName != obj.MiddleName) contact.MiddleName = obj.MiddleName;
                    if (contact.PhoneNumber != obj.PhoneNumber) contact.PhoneNumber = obj.PhoneNumber;
                    if (contact.Address != obj.Address) contact.Address = obj.Address;
                    if (contact.Info != obj.Info && obj.Info != null) contact.Info = obj.Info;

                    _db.Contact.Update(contact);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("AllContacts");
        }

        [HttpPost]
        public IActionResult DeleteContact(int? id)
        {
            if(id != null)
            {
                Contact selectedContact = _db.Contact.FirstOrDefault(p => p.Id == id); 
                Picture pictureOfSelectedContact = _db.Picture.FirstOrDefault(p => p.Id == selectedContact.PictureId);
                FileInfo file = new FileInfo(_env.WebRootPath + pictureOfSelectedContact.Path);
                if (file.Exists && pictureOfSelectedContact.Id != 1)
                {
                   file.Delete();
                    _db.Picture.Remove(pictureOfSelectedContact);
                    _db.SaveChanges();
                }
                if (selectedContact != null)
                {
                    _db.Contact.Remove(selectedContact);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("AllContacts");
        }


    }
}
