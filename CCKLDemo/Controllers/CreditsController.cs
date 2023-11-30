using CCKLDemo.Database;
using CCKLDemo.Database.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCKLDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditsController : ControllerBase
    {
        private readonly AccountDbContext _context;

        public CreditsController(AccountDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int n)
        {
            return Ok(await _context.Credits.Take(n).ToArrayAsync());
        }
        [HttpPost]
        public async Task<IActionResult> PostCredits(int n)
        {
             _context.AddRange( await GenerateCredits(n));
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkCredits(int n)
        {
            await _context.BulkInsertAsync(await GenerateCredits(n));
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCredit()
        {
            await _context.BulkDeleteAsync(await _context.Credits.ToArrayAsync());
            return Ok();
        }

        private async Task<Credit[]> GenerateCredits(int n)
        {
            var customers = await _context.Customers.AsNoTracking().OrderBy(x => x.OtherName).Take(10).Select(x => x.Id).ToArrayAsync();
            var rdm = new Random();
            Credit[] credits = new Credit[n];
            for (int i = 0; i < n; i++)
            {
                int amount = rdm.Next(10, 100000);
                var cid = rdm.Next(1, 9);
                var customerId = customers[cid];
                DateTime date = DateTime.UtcNow.AddDays(-cid);
                var credit = new Credit { Amount=amount, CreatedDate=date, CustomerId =customerId };
                credits[i] = credit;
            }
            return credits;
        }
    }
}
