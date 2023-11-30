using CCKLDemo.Database;
using CCKLDemo.Database.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCKLDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AccountDbContext _context;

        public CustomersController(AccountDbContext context)
        {
            _context = context;
        }
        [HttpPost] 
        public async Task<IActionResult> Post(int n)
        {
            _context.Customers.AddRange(await GenerateCustomer(n));
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomers()
        {
           var customers= await _context.Customers.ToArrayAsync();
           await _context.BulkDeleteAsync(customers);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers.AsNoTracking().Take(1000).Select(x => new
            {
                x.Id,
                balance = x.Credits.Sum(x => x.Amount) - x.Purchases.Sum(x => x.Amount),
                x.DOB,
                x.Country.Name
            }).ToArrayAsync();

            return Ok(customers);
        }

        private async Task<Customer[]> GenerateCustomer(int n)
        {
            var countries=await _context.Countries.AsNoTracking().OrderBy(x=>x.Continent).Take(10).Select(x=>x.Id).ToArrayAsync();
            var rdm = new Random();
            Customer[] customers = new Customer[n];
            for (int i = 0; i < n; i++)
            {
                int cid = rdm.Next(1,9);
                DateTime dob= DateTime.UtcNow.AddYears(-cid-20);
                var customer = new Customer() { CountryId = countries[cid], DOB = dob, Id=Guid.NewGuid() };
                customers[i] = customer;
            }
            return customers;
        }
    }
}
