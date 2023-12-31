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

        private int? quantitysell = 0;

        [DisplayName("Số lượng đã bán")]
        public int? Quantitysell
        {
            get { return quantitysell; }
            set
            {
                quantitysell = value;
                if (quantitysell.HasValue && Quantitysell == Quantity)
                {
                    Status = false;
                }
                else
                {
                    Status = true;
                }
            }
        }

        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        [Required(ErrorMessage = "Hãy điền đúng giá trị")]
        [DisplayName("Giá tiền")]
        public double Price { get; set; }

        [DisplayName("Ảnh sản phẩm")]
        public string? Productimg { get; set; }
        [DisplayName("Loại sản phẩm")]
        public int TagId { get; set; }

        [DisplayName("Trạng thái")]
        public bool Status { get; set; } = true;

        [DisplayName("Loại sản phẩm")]
        [ForeignKey("TagId")]
        public Tags tags { get; set; }

		public List<OrderDetail> OrderDetail { get; set; }

		public List<CartDetail> CartDetail { get; set; }

		[NotMapped]
        public string tagname { get; set; }
	}
}
