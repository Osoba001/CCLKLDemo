using CCKLDemo.Database;
using CCKLDemo.Database.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCKLDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly AccountDbContext _context;

        public PurchasesController(AccountDbContext context)
        {
            _context = context;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreatePurches(int n)
        {
            await _context.BulkInsertAsync(await GeneratePurchases(n));
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> PostPurches(int n)
        {
            _context.AddRange(await GeneratePurchases(n));
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int n)
        {
            return Ok(await _context.Purchases.Take(n).ToArrayAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePurchases()
        {
            await _context.BulkDeleteAsync(await _context.Purchases.ToArrayAsync());
            return Ok();
        }
        private async Task<Purchase[]> GeneratePurchases(int n)
        {
            var customers = await _context.Customers.AsNoTracking().OrderBy(x => x.OtherName).Take(10).Select(x => x.Id).ToArrayAsync();
            var markets = await _context.Markets.AsNoTracking().OrderBy(x => x.MarketType).Take(10).Select(x => x.Id).ToArrayAsync();
            var rdm = new Random();
            Purchase[] Purchases = new Purchase[n];
            for (int i = 0; i < n; i++)
            {
                int amount = rdm.Next(10, 100000);
                var cid = rdm.Next(1, 9);
                DateTime date = DateTime.UtcNow.AddDays(-cid);
                var Purchase = new Purchase { Amount = amount, CreatedDate = date, CustomerId = customers[cid], MarketId= markets[cid] };
                Purchases[i] = Purchase;
            }
            return Purchases;
        }
    }
}
