using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class HealthcheckController: BaseApiController
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