using System.Collections.Generic;

namespace QuanLyThuTienHoc.Model
{
    public class Lop
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public int SiSo { get; set; }
        public List<SinhVien> SinhViens { get; set; }
    }
}