namespace API.DTOs
{
	public class UserDTO
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Token { get; set; }
		public string? Currency { get; set; } = "USD";
	}
}
