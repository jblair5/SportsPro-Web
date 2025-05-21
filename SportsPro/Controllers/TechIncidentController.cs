using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {

        private SportsProContext context;

        public TechIncidentController(SportsProContext ctx)
        {
            context = ctx;
		}
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("TechIncident")]
        public IActionResult Select(TechnicianViewModel model)
        {
			var session = new TechSession(HttpContext.Session);

            if(session.GetTechID() != null)
            {
                return RedirectToAction("List", "TechIncident");
            }
			
            model.Customers = context.Customers.ToList();
			model.Products = context.Products.ToList();
			model.Technicians = context.Technicians.ToList();
            model.Incidents = context.Incidents.ToList();
            model.Technician = new Technician();

            session.SetIncidents(model.Incidents);
            session.SetCustomers(model.Customers);
            session.SetProducts(model.Products);
            session.SetIncidents(model.Incidents);

            return View("Select", model);
        }

		public ViewResult List(TechnicianViewModel model)
        {
			var session = new TechSession(HttpContext.Session);

			if (session.GetTechID() == null)
			{
				session.SetTechID(model.ID);
				model.Technician = context.Technicians.Find(model.ID)!;
				session.SetTech(model.Technician);
			}
			

			model.ID = (int)session.GetTechID()!;
            model.Technician = session.GetTechnician();
            model.Customers = session.GetCustomers();
            model.Products = session.GetProducts();
            model.Technicians = context.Technicians.ToList();
            model.Incidents = context.Incidents
                        .Where(i => i.TechnicianID == model.ID)
                        .Include(i => i.Customer)
                        .Include (i => i.Product)
                        .OrderBy(i => i.IncidentID)
                        .ToList();
            
            return View("Manager", model);
        }

        [HttpGet]
        public ViewResult Edit(IncidentViewModel model, int id)
        {
			var session = new TechSession(HttpContext.Session);


			model.Technicians = context.Technicians.ToList();
            model.Customers = session.GetCustomers();
            model.Products = session.GetProducts();

			model.incidents = context.Incidents
						.Include(i => i.Customer)
						.Include(i => i.Product)
						.OrderBy(i => i.IncidentID)
						.ToList();
            model.Incident = context.Incidents.Find(id)!;



			return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {

			if (ModelState.IsValid)
			{
		
			    context.Incidents.Update(model.Incident);
				context.SaveChanges();
				return RedirectToAction("List", "TechIncident");
			}
			else
                return View(model);
;
        }

        public RedirectToActionResult Switch()
        {
			var session = new TechSession(HttpContext.Session);

            session.RemoveTech();
			return RedirectToAction("Select", "TechIncident");
        }
    }
}
