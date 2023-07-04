using DoAnThucTapFin.Areas.Admin.Models;
using Humanizer.Localisation;

namespace DoAnThucTapFin
{
	public interface IHomeRepository
	{
		Task<IEnumerable<Product>> GetProduct(string sTerm = "", int tagid = 0, string brand = "", double minPrice = 0, double maxPrice = double.MaxValue);
		Task<IEnumerable<Tags>> tags();
	}
}