using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
	public class TechnicianController : Controller
	{
		private SportsProContext context { get; set; }

		public TechnicianController(SportsProContext ctx) => context = ctx;

		[Route("Technicians")]
		public IActionResult List()
		{
			ViewBag.Action = "Manager";
			var technician = context.Technicians.OrderBy(c => c.TechnicianID).ToList();
			return View("Manager", technician);
		}

		[HttpGet]
		public IActionResult Add()
		{
			ViewBag.Action = "Add";
			ViewBag.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
			return View("Edit", new Technician());
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewBag.Action = "Edit";
			ViewBag.Technicians = context.Technicians.OrderBy(c => c.TechnicianID).ToList();
			var technician = context.Technicians.Find(id);
			return View("Edit", technician);
		}

		[HttpPost]
		public IActionResult Edit(Technician technician)
		{
			if (ModelState.IsValid)
			{
				if (technician.TechnicianID == 0)
				{
					context.Technicians.Add(technician);
				}
				else
					context.Technicians.Update(technician);
				context.SaveChanges();
				return RedirectToAction("List", "Technician");
			}
			else
			{
				ViewBag.Action = (technician.TechnicianID == 0) ? "Add" : "Edit";
				ViewBag.Technicians = context.Technicians.OrderBy(c => c.TechnicianID).ToList();
				return View("Manager", "Technician");
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var technician = context.Technicians.Find(id);
			return View(technician);
		}

		[HttpPost]
		public IActionResult Delete(Technician technician)
		{
			context.Technicians.Remove(technician);
			context.SaveChanges();
			return RedirectToAction("List", "Technician");
		}
	}
}
