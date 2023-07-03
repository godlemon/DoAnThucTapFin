using DoAnThucTapFin.Areas.Admin.Models;
using Humanizer.Localisation;

namespace DoAnThucTapFin
{
	public interface IHomeRepository
	{
		Task<IEnumerable<Product>> GetProduct(string sTerm = "", int tagid = 0, string brand = "");
		Task<IEnumerable<Tags>> tags();
	}
}