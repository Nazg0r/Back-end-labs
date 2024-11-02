using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class UserController() : BaseApiController
	{
		[HttpGet("{id}")]
		public IActionResult GetUser(int id)
		{
			User? user = DataContext.Users.FirstOrDefault(user => user.Id == id);

			if (user == null) {
				return NotFound("Selected user is not found in system");
			}

			return Ok(user);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(int id)
		{
			User? user = DataContext.Users.FirstOrDefault(user => user.Id == id);
			if (user == null)
			{
				return NotFound("Selected user is not found in system");
			}
			DataContext.Users.Remove(user);

			return Ok(user);
		}

		[HttpPost]
		public IActionResult AddUser(UserDTO user)
		{
			if (CheckUser(user.Name))
			{
				return BadRequest("A user with this name already exists");
			}

			User newUser = new()
			{
				Id = DataContext.Users[DataContext.Users.Count - 1].Id + 1,
				Name = user.Name
			};

			DataContext.Users.Add(newUser);

			return Ok(newUser);
		}

		[HttpGet]
		public IActionResult GetUsers()
		{
			if (DataContext.Users == null)
			{
				return NotFound("The system does not contain any users");
			}
			return Ok(DataContext.Users);
		}

		private bool CheckUser(string name)
		{
			return DataContext.Users.Any(user => user.Name.ToLower() == name.ToLower());
		}


	}
}
