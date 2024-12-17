using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
	[PrimaryKey("Id")]
	public class User
	{
		public int Id { get; set; }
		public int? CurrencyId { get; set; } = 1;
		public required string Name { get; set; }

		[ForeignKey("CurrencyId")]
		public virtual Currency Currency { get; set; }
	}
}
