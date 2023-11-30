using CCKLDemo.Database;
using CCKLDemo.Database.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCKLDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketsController : ControllerBase
    {
        private readonly AccountDbContext _context;

        public MarketsController(AccountDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMarkrts(int n)
        {
            _context.Markets.AddRange(await GenerateMarkrts(n));
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int n)
        {
            return Ok(await _context.Markets.Take(n).Select(x => new
            {
                x.Id,
                x.Name,
                Sales=x.purchases.Sum(x=>x.Amount),
                country= x.Country.Name,
                x.CreatedDate
            }).ToArrayAsync());
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulkMarkrts(int n)
        {
            await _context.BulkInsertAsync(await GenerateMarkrts(n));
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteMarkrts()
        {
            var markets = await _context.Markets.ToArrayAsync();
            await _context.BulkDeleteAsync(markets);
            return Ok();
        }

        private async Task<Market[]> GenerateMarkrts(int n)
        {
            var countries = await _context.Countries.AsNoTracking().OrderBy(x => x.Continent).Take(10).Select(x => x.Id).ToArrayAsync();
            var rdm = new Random();
            Market[] customers = new Market[n];
            for (int i = 0; i < n; i++)
            {
                int cid = rdm.Next(1, 9);
                DateTime dob = DateTime.UtcNow.AddYears(-cid - 20);
                var customer = new Market { CountryId = countries[cid] };
                customers[i] = customer;
            }
            return customers;
        }
    }
}
