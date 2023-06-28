using DoAnThucTapFin.Areas.Admin.Models;

namespace DoAnThucTapFin.Models.DTOs
{
    public class ProductDisplayModel
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<Tags> tags { get; set; }
		public PaginationModel Pagination { get; set; }
		public string STerm { get; set; } = "";
        public int tagid { get; set; } = 0;
    }
	public class PaginationModel
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
	}
}
