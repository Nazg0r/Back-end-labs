using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Authorize]
	public class CurrencyController(DataContext dbContext) : BaseApiController
	{
		[HttpGet]
		public async Task<IActionResult> GetCurrencies()
		{
			IEnumerable<Currency>? currencies = await dbContext.Currencies.ToListAsync();

			if (currencies is null || !currencies.Any())
				return NotFound("Currencies is not found in system");

			return Ok(currencies);
		}

		[HttpPost]
		public async Task<IActionResult> AddCurrency(CurrencyDTO currency)
		{
			if (dbContext.Currencies.Any(c => c.Name.ToLower() == currency.Name.ToLower()))
				return BadRequest("This currency already exists");

			Currency newCurrency = new()
			{
				Name = currency.Name
			};

			dbContext.Add(newCurrency);
			await dbContext.SaveChangesAsync();

			return Ok(newCurrency);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCurrency(int id)
		{
			Currency? currency = await dbContext.Currencies.FindAsync(id);
			if (currency is null)
				return NotFound("Currency with such id is not found in system");


			dbContext.Currencies.Remove(currency);
			await dbContext.SaveChangesAsync();

			return Ok(currency);
		}
	}
}
