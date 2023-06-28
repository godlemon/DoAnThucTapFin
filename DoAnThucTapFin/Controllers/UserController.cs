using Microsoft.AspNetCore.Mvc;

namespace DoAnThucTapFin.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
		public IActionResult Register()
		{
			return View();
		}
	}
}
