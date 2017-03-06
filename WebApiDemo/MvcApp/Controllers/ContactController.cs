using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServices.Models;
using System.Web.Http;

namespace MvcApp.Controllers
{
public class ContactController : ApiController
{
    private static List<Contact> contacts = new List<Contact>
    {
        new Contact{ Id="001", FirstName = "San", LastName="Zhang", PhoneNo="123", EmailAddress="zhangsan@gmail.com"},
        new Contact{ Id="002",FirstName = "Si", LastName="Li", PhoneNo="456", EmailAddress="lisi@gmail.com"}
    };
      
    public IEnumerable<Contact> Get()
    {
        return contacts;
    }

    public Contact Get(string id)
    {
        return contacts.FirstOrDefault(c => c.Id == id);
    }

    public void Put(Contact contact)
    {
        if (string.IsNullOrEmpty(contact.Id))
        {
            contact.Id = Guid.NewGuid().ToString();
        }
        contacts.Add(contact);
    }

    public void Post(Contact contact)
    {
        Delete(contact.Id);
        contacts.Add(contact);
    }

    public void Delete(string id)
    {
        Contact contact = contacts.FirstOrDefault(c => c.Id == id);
        contacts.Remove(contact);
    }
}
}
