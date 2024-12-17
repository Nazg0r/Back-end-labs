using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
	[PrimaryKey("Id")]
	[Table("currencies")]
	public class Currency
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("name")]
		public string Name { get; set; }
	}
}
