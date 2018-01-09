using QuanLyThuTienHoc.Class;
using QuanLyThuTienHoc.Functions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuTienHoc.Forms
{
    public partial class frmChiTietBienLai : Form
    {
        private string connectionString = Configuration.connectionString;

        private SqlConnection conn;
        private BienLai _BienLai;

        public frmChiTietBienLai()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        public frmChiTietBienLai(BienLai bienlai) : this()
        {
            _BienLai = new BienLai();
            _BienLai.HoTen = bienlai.HoTen;
            _BienLai.GioiTinh = bienlai.GioiTinh;
            _BienLai.MaSV = bienlai.MaSV;
            _BienLai.NgaySinh = bienlai.NgaySinh;
            _BienLai.lstMonHoc = bienlai.lstMonHoc;
            dataGridSinhVien.DataSource = _BienLai.lstMonHoc;
            InitDatainControl();
        }

        private void InitDatainControl()
        {
            txtHoTen.Text = _BienLai.HoTen;
            txtMaSV.Text = _BienLai.MaSV;
            txtGioiTinh.Text = _BienLai.GioiTinh;
            dtpkNgaySinh.Text = _BienLai.NgaySinh;
            txtMienGiam.Text = getDoiTuongMienGiam(_BienLai.MaSV).ToString() + " %";
            txtTongTien.Text = (tinhTongTien() *(100 - getDoiTuongMienGiam(_BienLai.MaSV)) / 100).ToString("#,##");
        }

        private void frmChiTietBienLai_Load(object sender, EventArgs e)
        {
        }

        private double tinhTongTien()
        {
            double tongtien = 0;
            foreach (MonHoc monhoc in _BienLai.lstMonHoc)
            {
                tongtien += monhoc.soTien * monhoc.soTinChi;
            }
            return tongtien;
        }

        private double getDoiTuongMienGiam(string maSV)
        {
            double miengiam = 0;
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_LayDoiTuongMienGiam";
                command.Connection = conn;
                command.Parameters.AddWithValue("@masv", maSV);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        miengiam = reader.GetDouble(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();   
            }
            return miengiam;
        }
    }
}