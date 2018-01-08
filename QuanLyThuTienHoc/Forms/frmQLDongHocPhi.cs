using QuanLyThuTienHoc.Functions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuTienHoc.Forms
{
    public partial class frmQLDongHocPhi : Form
    {
        private string connectionString = Configuration.connectionString;
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        private DataSet dataSet = new DataSet();

        public frmQLDongHocPhi()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            loadAllSV();
            loadCbLop();
        }

        private void loadCbLop()
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_getAllLop";
                command.Connection = conn;
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataSet, "Lop");

                cbLop.DataSource = dataSet.Tables["Lop"];
                cbLop.DisplayMember = "TenLop";
                cbLop.ValueMember = "MaLop";
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

        private void loadAllSV()
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_selectAllSV";
                command.Connection = conn;
                adapter = new SqlDataAdapter(command);

                if (dataSet.Tables["SinhVien"] != null)
                {
                    dataSet.Tables["SinhVien"].Clear();
                }

                adapter.Fill(dataSet, "SinhVien");

                dataGridSinhVien.DataSource = dataSet.Tables["SinhVien"];
                dataGridSinhVien.Columns[5].Visible = false;
                dataGridSinhVien.Columns[6].Visible = false;
                dataGridSinhVien.Columns[7].Visible = false;
                dataGridSinhVien.Columns[8].Visible = false;
                dataGridSinhVien.Columns[9].Visible = false;
                dataGridSinhVien.Columns[10].Visible = false;
                dataGridSinhVien.Columns[11].Visible = false;
                dataGridSinhVien.Columns[12].Visible = false;
                dataGridSinhVien.Columns[13].Visible = false;

                dataGridSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

      
        private void dataGridSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvSinhVien = sender as DataGridView;
            if (e.RowIndex < dgvSinhVien.RowCount - 1)
            {
                bindingSVToTextbox(dgvSinhVien.CurrentRow);
            }
        }

        private void bindingSVToTextbox(DataGridViewRow currentRow)
        {
            txtMaSV.Text = currentRow.Cells[0].Value.ToString();
            txtTenSV.Text = currentRow.Cells[1].Value.ToString();
        }

        private void btnInBienLai_Click(object sender, EventArgs e)
        {
            frmChiTietBienLai frmCT = new frmChiTietBienLai(txtMaSV.Text, txtTenSV.Text, Convert.ToInt32(cbHocKy.SelectedItem));
            frmCT.ShowDialog();
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbLop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_layChiTietMonHoc";
                command.Parameters.AddWithValue("@malop", cbLop.SelectedValue);
                command.Connection = conn;
                adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridChiTiet.DataSource = ds.Tables[0];
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