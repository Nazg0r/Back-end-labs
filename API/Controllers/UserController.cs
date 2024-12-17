using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	public class UserController(DataContext dbContext) : BaseApiController
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			User? user = await dbContext.Users.FindAsync(id);

			if (user is null) 
				return NotFound("Selected user is not found in system");
			
			return Ok(new
			{
				Id = user.Id,
				Name = user.Name,
				Currency = user.Currency.Name
			});
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			User? user = await dbContext.Users.FindAsync(id);
			if (user is null)
				return NotFound("Selected user is not found in system");

			var response = new 
			{
				Id = user.Id,
				Name = user.Name,
				Currency = user.Currency.Name
			};

			dbContext.Users.Remove(user);
			await dbContext.SaveChangesAsync();

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> AddUser(UserDTO user)
		{
			if (await CheckUser(user.Name))
				return BadRequest("A user with this name already exists");

			Currency? currency = await dbContext.Currencies.FirstOrDefaultAsync(c => c.Name == user.Currency);

			if (currency is null)
				return BadRequest("No such currency in our system. Try \"USD\", \"EUR\", \"UAH\"");


			User newUser = new() { Name = user.Name, CurrencyId = currency.Id };

			await dbContext.AddAsync(newUser);
			await dbContext.SaveChangesAsync();

			return Ok(new
			{
				Id = newUser.Id,
				Name = newUser.Name,
				Currency = newUser.Currency.Name
			});
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			if (!await dbContext.Users.AnyAsync())
				return NotFound("The system does not contain any users");

			return Ok
			(
				await dbContext.Users
				.Select(user => new 
				{
					Id = user.Id,
					Name = user.Name,
					Currency = user.Currency.Name
				})
				.ToListAsync()
			);
		}

		private async Task<bool> CheckUser(string name)
		{
			return await dbContext.Users.AnyAsync(user => user.Name.ToLower() == name.ToLower());
		}
	}
}
