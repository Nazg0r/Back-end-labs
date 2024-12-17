using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
	[PrimaryKey("Id")]
	[Table("users")]
	public class User
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("currency_id")]
		public int? CurrencyId { get; set; } = 1;

		[Column("name")]
		public required string Name { get; set; }

		[ForeignKey("CurrencyId")]
		public virtual Currency Currency { get; set; }
	}
}
