namespace API.DTOs
{
	public class UserDTO
	{
		public required string Name { get; set; }
		public string? Currency { get; set; } = "USD";
	}
}
