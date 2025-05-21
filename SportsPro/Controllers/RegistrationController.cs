using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using System.Security.Cryptography;

namespace SportsPro.Controllers
{
	public class RegistrationController : Controller
	{
		private SportsProContext context { get; set; }
		public RegistrationController(SportsProContext ctx) => context = ctx;

		[Route("Registration")]
		public ViewResult Index()
		{
			ViewBag.Customers = context.Customers.ToList();

			return View();
		}
		public ViewResult List(int id)
		{
			var customer = context.Customers
				.Include(p => p.Products)
				.Where(c => c.CustomerID == id);

			ViewBag.Products = context.Products.ToList();

			Customer c = customer.FirstOrDefault()!;


			return View(c);
		}
		public RedirectToActionResult Add(int id, int CustomerID)
		{

			var p = context.Products.Find(id);
			var c = context.Customers.Find(CustomerID);
			try
			{
				c!.Products.Add(p);
				context.SaveChanges();

				return RedirectToAction("List", new { id = CustomerID });
			}
			catch (DbUpdateException ex)
			{

				TempData["Error"] = "Product already registered to Customer";
				return RedirectToAction("List", new { id = CustomerID });

			}
		}
		public RedirectToActionResult Delete(int pid, int cid)
		{

			var p = context.Products.Find(pid);
			var c = context.Customers.Include(p => p.Products).Where(c => c.CustomerID == cid);


			if(c.FirstOrDefault()!.Products.Remove(p))
				context.SaveChanges();

			return RedirectToAction("List", new {id = cid});
		}
	}
}
