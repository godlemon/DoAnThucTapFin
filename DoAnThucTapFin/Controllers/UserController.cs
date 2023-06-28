using Microsoft.AspNetCore.Mvc;

namespace DoAnThucTap.Controllers
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
