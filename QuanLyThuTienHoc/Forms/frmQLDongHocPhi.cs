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

        private void txtTimKiem_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_searchSVByName";
                command.Parameters.AddWithValue("@name", txtTimKiem.Text);
                command.Connection = conn;
                adapter = new SqlDataAdapter(command);
                dataSet.Reset();
                adapter.Fill(dataSet);
                dataGridSinhVien.DataSource = dataSet.Tables[0];
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
            bindingSVToTextbox(dgvSinhVien.CurrentRow);
        }

        private void bindingSVToTextbox(DataGridViewRow currentRow)
        {
            txtMaSV.Text = currentRow.Cells[0].Value.ToString();
            txtTenSV.Text = currentRow.Cells[1].Value.ToString();
        }

        private void btnInBienLai_Click(object sender, EventArgs e)
        {
            frmChiTietBienLai frmCT = new frmChiTietBienLai(txtMaSV.Text, txtTenSV.Text,Convert.ToInt32(cbHocKy.SelectedItem));
            frmCT.ShowDialog();
        }
    }
}