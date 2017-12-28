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
        private string _maSV;
        private string _tenSV;
        private int _hocky;
        private SqlConnection conn;

        public frmChiTietBienLai()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        public frmChiTietBienLai(string maSV, string tensv,int hocki) : this()
        {
            _maSV = maSV;
            _hocky = hocki;
            _tenSV = tensv;
        }

        private void frmChiTietBienLai_Load(object sender, EventArgs e)
        {
            txtMaSV.Text = _maSV;
            txtHoTen.Text = _tenSV;
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_SelectChiTietDangKyMonHoc";
                command.Connection = conn;
                command.Parameters.AddWithValue("@masv", _maSV);
                command.Parameters.AddWithValue("@hocky", _hocky);
                DataSet ds = new DataSet();
                SqlDataAdapter adap = new SqlDataAdapter(command);
                adap.Fill(ds);
                dataGridSinhVien.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}