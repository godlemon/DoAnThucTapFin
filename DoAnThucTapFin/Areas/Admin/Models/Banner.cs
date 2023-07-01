using System.ComponentModel;

namespace DoAnThucTapFin.Areas.Admin.Models
{
    public class Banner
    {
        public int Id { get; set; }
        [DisplayName("Banner")]
        public string Name { get; set; }
        [DisplayName("Ảnh")]
        public string Url { get; set; }
        public int Active { get; set; }
    }
}
