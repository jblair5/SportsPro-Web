using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Xml.Linq;

namespace SportsPro.Controllers
{
	public class ProductController : Controller
	{
		private SportsProContext context { get; set; }

		public ProductController(SportsProContext ctx) => context = ctx;

		[Route("Products")]
		public ViewResult List()
		{
			ViewBag.Action = "Manager";
			var products = context.Products.OrderBy(c => c.ProductID).ToList();
			ViewBag.Message = TempData["message"];

			return View("Manager", products);
		}

		[HttpGet]
		public ViewResult Add()
		{
			ViewBag.Action = "Add";
			ViewBag.Products = context.Products.OrderBy(c => c.Name).ToList();
			return View("Edit", new Product());
		}

		[HttpGet]
		public ViewResult Edit(int id)
		{
			ViewBag.Action = "Edit";
			ViewBag.Products = context.Products.OrderBy(c => c.ProductID).ToList();
			var product = context.Products.Find(id);
			return View("Edit", product);
		}

		[HttpPost]
		public RedirectToActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				if (product.ProductID == 0)
				{
					product.ReleaseDate = DateTime.Now;
					context.Products.Add(product);
					TempData["message"] = $"{product.Name} was successfully added!";

				}
				else
				{
					context.Products.Update(product);
					TempData["message"] = $"{product.Name} was successfully edited!";
				}
				context.SaveChanges();
				return RedirectToAction("List", "Product");
			}
			else
			{
				ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
				ViewBag.Products = context.Products.OrderBy(c => c.Name).ToList();
				return RedirectToAction("List", "Product");
			}
		}

		[HttpGet]
		public ViewResult Delete(int id)
		{
			var product = context.Products.Find(id);
			TempData["name"] = product!.Name;

            return View(product);
		}

		[HttpPost]
		public RedirectToActionResult Delete(Product product)
		{
            TempData["message"] = $"{TempData["name"]} was successfully deleted!";


            context.Products.Remove(product);
			context.SaveChanges();

			return RedirectToAction("List", "Product");
		}
	}
}
