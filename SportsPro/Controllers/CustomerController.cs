using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
	public class CustomerController : Controller
	{
		private SportsProContext context { get; set; }

		public CustomerController(SportsProContext ctx) => context = ctx;
		[Route("Customers")]
		public IActionResult List()
		{
			ViewBag.Action = "Manager";
			var customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
			return View("Manager", customers);
		}

		[HttpGet]
		public IActionResult Add()
		{
			
			ViewBag.Action = "Add";
			ViewBag.Countries = context.Countries.OrderBy(c => c.Name).ToList();
			return View("Edit", new Customer());
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewBag.Action = "Edit";
			ViewBag.Countries = context.Countries.OrderBy(c => c.Name).ToList();
			ViewBag.Customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
			var customer = context.Customers.Find(id);
			return View("Edit", customer);
		}

		[HttpPost]
		public IActionResult Edit(Customer customer)
		{
			
			if (ModelState.IsValid)
			{
				if (customer.CustomerID == 0)
				{

					context.Customers.Add(customer);

				}
				else
					context.Customers.Update(customer);
				context.SaveChanges();
				return RedirectToAction("List", "Customer");
			}
			else
			{
				ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                ViewBag.Countries = context.Countries.OrderBy(c => c.Name).ToList();

                //	var customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
                //	return View("Manager", customers);

				return View(customer);
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var customer = context.Customers.Find(id);
			return View(customer);
		}

		[HttpPost]
		public IActionResult Delete(Customer customer)
		{
			context.Customers.Remove(customer);
			context.SaveChanges();
			return RedirectToAction("List", "Customer");
		}
	}
}
