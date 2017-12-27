using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuTienHoc.Model
{
    public class Khoa
    {
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public virtual IEnumerable<Lop> Lops { get; set; }
    }
}
