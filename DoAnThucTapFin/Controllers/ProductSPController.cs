using Microsoft.AspNetCore.Mvc;

namespace DoAnThucTapFin.Controllers
{
	public class ProductSPController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Detail ()
		{
			return View();
		}
	}
}
