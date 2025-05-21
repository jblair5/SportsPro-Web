using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
	public class ValidationController : Controller
	{
		private SportsProContext context;
		public ValidationController(SportsProContext cxt) => context = cxt;
		public JsonResult CheckEmail(string email, string FullName)
		{
			string msg = Check.EmailExists(context, email, FullName);
			if (string.IsNullOrEmpty(msg)) 
			{
				TempData["okEmail"] = true;
				return Json(true);
			}
			else return Json(msg);


		}
	}
}
