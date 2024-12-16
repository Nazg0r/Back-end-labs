using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{
	public class CategoryController(DataContext dbContext) : BaseApiController
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategory(int id)
		{
			Category? category = await dbContext.Categories.FindAsync(id);

			if (category is null)
				return NotFound("Category is not found in system");

			return Ok(category);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			Category? category = await dbContext.Categories.FindAsync(id);
			if (category is null)
				return NotFound("Category is not found in system");

			dbContext.Categories.Remove(category);
			await dbContext.SaveChangesAsync();

			return Ok(category);
		}

		[HttpPost]
		public async Task<IActionResult> AddCategory(CategoryDTO category)
		{
			if (dbContext.Categories.Any(ctgry => category.Name.ToLower() == ctgry.Name.ToLower()))
				return BadRequest("A category with this name already exists");

			Category newCategory = new()
			{
				Name = category.Name
			};

			dbContext.Add(newCategory);
			await dbContext.SaveChangesAsync();

			return Ok(newCategory);
		}
	}
}
