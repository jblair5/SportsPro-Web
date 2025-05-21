using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SportsPro.Controllers
{
	public class IncidentController : Controller
	{
		private SportsProContext context;

		public IncidentController(SportsProContext ctx)
		{
			context = ctx;
	
		}

		[Route("Incidents")]
		public ViewResult List(IncidentViewModel model)
        {
            ViewBag.Action = "Manager";
			
			if(model.Filter == "All")
			{
				model.incidents = context.Incidents.OrderBy(i => i.IncidentID).ToList();
				model.Customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
				model.Products = context.Products.OrderBy(c => c.Name).ToList();
				model.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
			}
			if(model.Filter == "Unassigned")
			{
				model.incidents = context.Incidents.Where(i => i.TechnicianID == -1).OrderBy(i => i.IncidentID).ToList();
				model.Customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
				model.Products = context.Products.OrderBy(c => c.Name).ToList();
				model.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
			}
			if(model.Filter == "Open")
			{
				model.incidents = context.Incidents.Where(i => i.DateClosed == null).OrderBy(i => i.IncidentID).ToList();
				model.Customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
				model.Products = context.Products.OrderBy(c => c.Name).ToList();
				model.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
			}

            return View("Manager", model);
		}

		[HttpGet]
		public IActionResult Add(IncidentViewModel model)
		{
            model.Action = "Add";
			model.Customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
            model.Products = context.Products.OrderBy(c => c.Name).ToList();
            model.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
			model.Incident = new Incident();

			return View("Edit", model);
		}

		[HttpGet]
		public IActionResult Edit(IncidentViewModel model, int id)
		{
            model.Action = "Edit";
			var incident = context.Incidents.Find(id);
            model.Customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
            model.Products = context.Products.OrderBy(c => c.Name).ToList();
            model.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
			model.Incident = incident!;

            return View(model);
		}

		[HttpPost]
		public IActionResult Edit(IncidentViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Incident.IncidentID == 0)
				{
					model.Incident.DateOpened = DateTime.Now;
					context.Incidents.Add(model.Incident);
				}
				else
					context.Incidents.Update(model.Incident);
				context.SaveChanges();
				return RedirectToAction("List", "Incident");
			}
			else
			{
				model.Action = (model.Incident.IncidentID == 0) ? "Add" : "Edit";
				//var incidents = context.Incidents.OrderBy(c => c.IncidentID).ToList();

                return RedirectToAction("List", "Incident");
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var incident = context.Incidents.Find(id);
			return View(incident);
		}

		[HttpPost]
		public IActionResult Delete(Incident incident)
		{
			context.Incidents.Remove(incident);
			context.SaveChanges();
			return RedirectToAction("List", "Incident");
		}		

		public RedirectToActionResult Filter(IncidentViewModel model, string filter)
		{
			
			model.Filter = filter;
			return RedirectToAction("List", model);
		}
	}
}
