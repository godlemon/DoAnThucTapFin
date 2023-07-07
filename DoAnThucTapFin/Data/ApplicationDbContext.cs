using DoAnThucTapFin.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<CartDetail> CartDetails { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

		public DbSet<OrderStatus> orderStatuses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if()
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<OrderStatus>().HasData(
				new OrderStatus { Id = 1, StatusId = 1 , StatusName = "Chờ thanh toán" },
				new OrderStatus { Id = 2, StatusName = "Đang vận chuyển" },
				new OrderStatus { Id = 3, StatusName = "Đã chuyển" }
				);
		}


	}
}