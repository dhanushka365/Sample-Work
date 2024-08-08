using Contactly.Data;
using Contactly.Models.Domain;
using Contactly.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Contactly.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactlyDbContext dbContext;

        public ContactsController(ContactlyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contact = dbContext.Contacts.ToList();

            return Ok(contact);
        }

        [HttpPost]
        public IActionResult AddContact(AddRequestContactsDTO request)
        {
            var domainModelContext = new Contact 
            { 
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email= request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            dbContext.Contacts.Add(domainModelContext);
            dbContext.SaveChanges();
            return Ok(domainModelContext);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = dbContext.Contacts.Find(id);

            if(contact is not null)
            {
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();

            }


            return Ok(contact);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateContact(Guid id, [FromBody] UpdateRequestContactsDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                contact.Name = request.Name;
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                contact.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                contact.PhoneNumber = request.PhoneNumber;
            }

            dbContext.Contacts.Update(contact);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }



    }
}
