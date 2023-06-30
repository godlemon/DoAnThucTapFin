using Azure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnThucTapFin.Areas.Admin.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nhãn hiệu là bắt buộc.")]
        [DisplayName("Nhãn hiệu")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Độ phân giải là bắt buộc.")]
        [DisplayName("Độ phân giải")]
        public string Resolution { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc.")]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Hãy điền đúng giá trị")]
        [DisplayName("Giá tiền")]
        public double Price { get; set; }

        [DisplayName("Ảnh sản phẩm")]
        public string? Productimg { get; set; }
        [DisplayName("Loại sản phẩm")]
        public int TagId { get; set; }

        [DisplayName("Loại sản phẩm")]
        [ForeignKey("TagId")]
        public Tags tags { get; set; }
		public List<OrderDetail> OrderDetail { get; set; }
		public List<CartDetail> CartDetail { get; set; }
		[NotMapped] 
        public string tagname { get; set; }
	}
}
