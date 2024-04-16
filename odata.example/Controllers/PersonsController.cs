using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using odata.example.DbContext;
using OdataEntities = odata.example.Entities;

namespace odata.example.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly OdataExampleDbContext _context;

        public PersonsController(OdataExampleDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = await _context.Persons.ToArrayAsync();
            return Ok(persons.AsQueryable());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromQuery] int quantity = 1)
        {
            if(quantity <= 0)
                quantity = 1;

            if(quantity > 1000)
                quantity = 1000;

            var persons = new List<OdataEntities.Person>();

            for (int i = 0; i < quantity; i++)
            {
                var faker = new Faker();
                var person = new OdataEntities.Person();

                person.Id = Guid.NewGuid();
                person.FirstName = faker.Person.FirstName;
                person.LastName = faker.Person.LastName;
                person.Email = faker.Person.Email;
                person.Phone = faker.Person.Phone;

                persons.Add(person);
            }

            await _context.AddRangeAsync(persons);
            await _context.SaveChangesAsync();

            return Ok(persons);
        }
    }
}