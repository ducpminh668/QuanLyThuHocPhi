using System;
using System.Collections.Generic;

namespace QuanLyThuTienHoc.Model
{
    public class DoiTuong
    {
        public string MaDoiTuong { get; set; }
        public string TenDoiTuong { get; set; }
        public decimal MienGiam { get; set; }
        public virtual IEnumerable<SinhVien> SinhViens { get; set; }

        public static implicit operator string(DoiTuong v)
        {
            throw new NotImplementedException();
        }
    }
}