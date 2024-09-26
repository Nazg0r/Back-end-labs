using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("{controller}")]
	public class HealthcheckController: ControllerBase
	{
		[HttpGet]
		public ActionResult GetStatus()
		{
			var currentDate = DateTime.Now.ToShortDateString();
			var currentTime = DateTime.Now.ToLongTimeString();
			var response = new { Date = currentDate,Time = currentTime, Status = "working" };

			return Ok(response);
		}
	}
}
