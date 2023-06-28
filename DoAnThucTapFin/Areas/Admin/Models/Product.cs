﻿using Azure;
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

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Hãy điền đúng giá trị")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [DisplayName("Ảnh sản phẩm")]
        public string? Productimg { get; set; }
        public int TagId { get; set; }

        [DisplayName("Loại sản phẩm")]
        [ForeignKey("TagId")]
        public Tags tags { get; set; }

        [NotMapped] 
        public string tagname { get; set; }
	}
}