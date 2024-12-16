using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
	[PrimaryKey("Id")]
	public class Category
	{
		public int Id { get; set; }
		public required string Name { get; set; }
	}
}
