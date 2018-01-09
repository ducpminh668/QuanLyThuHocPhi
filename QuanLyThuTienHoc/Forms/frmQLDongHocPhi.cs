using QuanLyThuTienHoc.Class;
using QuanLyThuTienHoc.Functions;
using System;
using System.Collections.Generic;
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
            loadCbLop();
            loadNamHoc();
            loadSVByLop(cbLop.SelectedValue.ToString());
            var a = cbLop.SelectedValue;
            cbHocKy.SelectedIndex = 0;
            layChiTietMonHoc();
        }

        private void loadNamHoc()
        {
            DateTime now = DateTime.Now;
            int year = now.Year;
            string hocky = String.Format("{0}-{1}", year, year + 1);
            txtNamHoc.Text = hocky;
        }

        private void loadSVByLop(string MaLop)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_loadSVBYLop";
                command.Connection = conn;
                command.Parameters.AddWithValue("@malop", MaLop);
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
            txtGioiTinh.Text = currentRow.Cells[2].Value.ToString();
            dtpkNgaySinh.Value = Convert.ToDateTime(currentRow.Cells[3].Value);
        }

        private void btnInBienLai_Click(object sender, EventArgs e)
        {
            if(txtMaSV.Text != "")
            {
                BienLai bienlai = new BienLai();
                bienlai.MaSV = txtMaSV.Text;
                bienlai.HoTen = txtTenSV.Text;
                bienlai.GioiTinh = txtGioiTinh.Text;
                bienlai.NgaySinh = dtpkNgaySinh.Value.ToString();
                List<MonHoc> lstMH = new List<MonHoc>();

                for (int i = 0; i < dataGridChiTiet.Rows.Count - 1; i++)
                {
                    string maMH = dataGridChiTiet.Rows[i].Cells[0].Value.ToString();
                    string tenMH = dataGridChiTiet.Rows[i].Cells[1].Value.ToString();
                    string soTC = dataGridChiTiet.Rows[i].Cells[2].Value.ToString();
                    string maMucHP = dataGridChiTiet.Rows[i].Cells[3].Value.ToString();
                    string soTien = dataGridChiTiet.Rows[i].Cells[4].Value.ToString();
                    bool dangKy = bool.Parse(dataGridChiTiet.Rows[i].Cells[5].Value.ToString());

                    MonHoc monhoc = new MonHoc();
                    monhoc.MaMonHoc = maMH;
                    monhoc.TenMonHoc = tenMH;
                    monhoc.soTinChi = int.Parse(soTC);
                    monhoc.MaMucHP = maMucHP;
                    monhoc.soTien = double.Parse(soTien);
                    monhoc.dangky = dangKy;

                    if (dangKy == true)
                    {
                        lstMH.Add(monhoc);
                    }


                }
                bienlai.lstMonHoc = lstMH;
                frmChiTietBienLai frmCT = new frmChiTietBienLai(bienlai);
                frmCT.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ban chua chon sinh vien!", "Thong bao");
            }
        
        }

        private void cbLop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            layChiTietMonHoc();
            
        }

        private void layChiTietMonHoc()
        {
            try
            {
                string hocky = cbHocKy.SelectedItem.ToString();
                string namhoc = txtNamHoc.Text;
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_layChiTietMonHoc";
                command.Parameters.AddWithValue("@malop", cbLop.SelectedValue);
                command.Parameters.AddWithValue("@hocki", hocky);
                command.Parameters.AddWithValue("@namhoc", namhoc);
                command.Connection = conn;
                adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataColumn col = new DataColumn();
                col.ColumnName = "DangKy";
                col.DataType = typeof(bool);
                col.DefaultValue = true;
                ds.Tables[0].Columns.Add(col);
                dataGridChiTiet.DataSource = ds.Tables[0];
                //dataGridChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                loadSVByLop(cbLop.SelectedValue.ToString());
            }
        }
    }
}