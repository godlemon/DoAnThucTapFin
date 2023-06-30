using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnThucTapFin.Areas.Admin.Models
{
    public class Tags
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
        [DisplayName("Danh mục")]
        public string Name { get; set; }
        [DisplayName("Miêu tả")]
        public string Description { get; set; }
        [DisplayName("Ảnh")]
        public string? Image { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
