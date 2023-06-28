using Microsoft.AspNetCore.Mvc;

namespace DoAnThucTapFin.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Shopping()
		{
			return View();
		}
	}
}
