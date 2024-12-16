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
			
			return Ok(user);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			User? user = await dbContext.Users.FindAsync(id);
			if (user is null)
				return NotFound("Selected user is not found in system");

			dbContext.Users.Remove(user);
			await dbContext.SaveChangesAsync();

			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> AddUser(UserDTO user)
		{
			if (await CheckUser(user.Name))
				return BadRequest("A user with this name already exists");

			User newUser = new() { Name = user.Name };

			await dbContext.AddAsync(newUser);
			await dbContext.SaveChangesAsync();

			return Ok(newUser);
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			if (!await dbContext.Users.AnyAsync())
				return NotFound("The system does not contain any users");

			return Ok(await dbContext.Users.ToListAsync());
		}

		private async Task<bool> CheckUser(string name)
		{
			return await dbContext.Users.AnyAsync(user => user.Name.ToLower() == name.ToLower());
		}
	}
}
