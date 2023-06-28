using DoAnThucTapFin.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAnThucTapFin.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Product> products { get; set; }
		public DbSet<Banner> banners { get; set; }
		public DbSet<Tags> tags { get; set; }
	}
}