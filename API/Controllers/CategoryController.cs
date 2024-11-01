using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{
	public class CategoryController : BaseApiController
	{
		[HttpGet("{id}")]
		public IActionResult GetCategory(int id)
		{
			Category? category = DataContext.Categories.FirstOrDefault(c => c.Id == id);

			if (category == null)
			{
				return NotFound("Category is not found in system");
			}

			return Ok(category);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCategory(int id)
		{
			Category? category = DataContext.Categories.FirstOrDefault(ctgry => ctgry.Id == id);
			if (category == null)
			{
				return NotFound("Category is not found in system");
			}
			DataContext.Categories.Remove(category);

			return Ok(category);
		}

		[HttpPost]
		public IActionResult AddCategory(CategoryDTO category)
		{
			if (DataContext.Categories.Any(ctgry => category.Name.ToLower() == ctgry.Name.ToLower()))
			{
				return BadRequest("A category with this name already exists");
			}

			Category newCategory = new()
			{
				Id = DataContext.Categories.Count + 1,
				Name = category.Name
			};

			DataContext.Categories.Add(newCategory);

			return Ok(newCategory);
		}
	}
}
