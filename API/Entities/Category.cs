using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
	[PrimaryKey("Id")]
	[Table("categories")]
	public class Category
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("name")]
		public required string Name { get; set; }
	}
}
