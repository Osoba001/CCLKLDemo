using CCKLDemo.Database;
using CCKLDemo.Database.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCKLDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountresController : ControllerBase
    {
        private readonly AccountDbContext _context;

        public CountresController(AccountDbContext context)
        {
            _context = context;
        }

        [HttpPost("save-changes")]
        public async Task<IActionResult> InsertCountriesWithSaveChanges(int n)
        {
            _context.Countries.AddRange(GenerateCountries(n));

           await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> Get(int n)
        {
            return Ok(await _context.Countries.Take(n).ToArrayAsync());
        }
        [HttpPost("bulk-insert")]
        public async Task<IActionResult> InsertCountriesWithBulkInsert(int n)
        {
            await _context.BulkInsertAsync(GenerateCountries(n));
            return Ok();
        }

        [HttpDelete("save-changes")]
        public async Task<IActionResult> WithSaveChnges()
        {
            var res = await _context.Countries.ToListAsync();
             _context.Countries.RemoveRange(res);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("bult-delete")]
        public async Task<IActionResult> WithBulkDelete()
        {
           var res= await _context.Countries.ToListAsync();
            await _context.BulkDeleteAsync(res);
            return Ok();

        }
        private Country[] GenerateCountries(int n)
        {
            Country[] countries = new Country[n];
            for (int i = 0; i < n; i++)
            {
                var country = new Country { Name = i + Guid.NewGuid().ToString() };
                countries[i] = country;
            }
            return countries;
        }

    }
}
