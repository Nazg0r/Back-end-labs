using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
	public class UserController(DataContext dbContext, ITokenService tokenService) : BaseApiController
	{


		[HttpPost("register")]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
		{

			using HMACSHA512 hmac = new HMACSHA512();

			if (await UserExist(registerDTO.Name)) return BadRequest("Selected name is already used");

			User user = new User
			{
				Name = registerDTO.Name,
				HashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
				Salt = hmac.Key
			};

			dbContext.Users.Add(user);
			await dbContext.SaveChangesAsync();

			UserDTO userDTO = new()
			{
				Id = user.Id,
				Name = registerDTO.Name,
				Token = tokenService.CreateToken(user),
			};

			return Ok(userDTO);
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
		{
			if (!await UserExist(loginDTO.Name)) return Unauthorized("Invalid username");

			User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == loginDTO.Name);

			if (!CheckPassword(user, loginDTO.Password)) return Unauthorized("Invalid password");

			UserDTO userDTO = new()
			{
				Id = user.Id,
				Name = loginDTO.Name,
				Token = tokenService.CreateToken(user),
			};

			return Ok(userDTO);
		}

		[Authorize]
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

		[Authorize]
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

		[Authorize]
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

		private async Task<bool> UserExist(string name)
		{
			return await dbContext.Users.AnyAsync(u => u.Name.ToLower() == name.ToLower());
		}

		private bool CheckPassword(User user, string password)
		{
			using HMAC hmac = new HMACSHA512(user.Salt);

			byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

			for (int i = 0; i < user.HashPassword.Length; i++)
			{
				if (user.HashPassword[i] != computedHash[i]) return false;
			}

			return true;
		}
	}
}
