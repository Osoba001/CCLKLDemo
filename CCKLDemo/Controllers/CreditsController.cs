using CCKLDemo.Database;
using CCKLDemo.Database.Models;
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

        private async Task<Credit[]> GenerateCredits(int n)
        {
            var countries = await _context.Customers.AsNoTracking().OrderBy(x => x.Purchases).Take(10).Select(x => x.Id).ToArrayAsync();
            var rdm = new Random();
            Credit[] customers = new Credit[n];
            for (int i = 0; i < n; i++)
            {
                int cid = rdm.Next(1, 10);
                DateTime dob = DateTime.UtcNow.AddYears(-cid - 20);
                var customer = new Credit {  };
                customers[i] = customer;
            }
            return customers;
        }
    }
}
