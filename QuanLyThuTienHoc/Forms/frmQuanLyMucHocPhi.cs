using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuTienHoc.Forms
{
    public partial class frmQuanLyMucHocPhi : Form
    {
        private string connectionString = Functions.Configuration.connectionString;
        private SqlConnection conn;

        public frmQuanLyMucHocPhi()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "sp_InsertMucHocPhi";
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@tien", decimal.Parse(txtTien.Text));
                    command.Parameters.AddWithValue("@mota", txtMoTa.Text);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        this.Close();
                    };
                }
            }
            catch (FormatException exFormat)
            {
                MessageBox.Show("Input invalid");
                // Log(exFormat);
            }
            catch (Exception ex)
            {
                // Log(ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}