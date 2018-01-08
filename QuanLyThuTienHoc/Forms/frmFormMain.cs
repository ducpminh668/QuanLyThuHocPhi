using QuanLyThuTienHoc.Forms;
using QuanLyThuTienHoc.Functions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuTienHoc
{
    public partial class Form1 : Form
    {
        private string connectionString = Configuration.connectionString;
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        private DataSet dataSet = new DataSet();
        private string flagAction = "";

        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            lockControls();
            addLopConbobox();
            addDoiTuongCombobox();
        }

        private void addDoiTuongCombobox()
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_getAllDoiTuong";
                command.Connection = conn;
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataSet, "DoiTuongMienGiam");
                cbDoiTuong.DataSource = dataSet.Tables["DoiTuongMienGiam"];
                cbDoiTuong.DisplayMember = "TenDoiTuong";
                cbDoiTuong.ValueMember = "MaDoiTuong";
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

        private void Form1_Load(object sender, EventArgs e)
        {
            loadAllSV();
        }

        private void lockControls()
        {
            txtTen.Enabled = false;
            txtTinh.Enabled = false;
            txtNgaySinh.Enabled = false;
            cbLop.Enabled = false;
            cbGioiTinh.Enabled = false;
            cbDoiTuong.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void unlockControls()
        {
            txtTen.Enabled = true;
            txtTinh.Enabled = true;
            txtNgaySinh.Enabled = true;
            cbLop.Enabled = true;
            cbGioiTinh.Enabled = true;
            cbDoiTuong.Enabled = true;
        }

        private void resetCotrols()
        {
            txtTen.Text = "";
            txtMa.Text = "";
            txtTinh.Text = "";
            txtNgaySinh.Text = "";
            cbGioiTinh.SelectedIndex = 0;
        }

        private void addLopConbobox()
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            flagAction = "add";
            resetCotrols();
            unlockControls();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }

        private bool validateData()
        {
            if (string.IsNullOrEmpty(txtTen.Text))
            {
                txtTen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTinh.Text))
            {
                txtTinh.Focus();
                return false;
            }
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (validateData())
            {
                if (flagAction.Equals("add"))
                {
                    addSinhVien();
                }
                if (flagAction.Equals("edit"))
                {
                    updateSinhVien();
                }
            }
        }

        private void addSinhVien()
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_insertSinhVien";
                command.Connection = conn;
                command.Parameters.AddWithValue("@HoTen", txtTen.Text);
                command.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.SelectedItem);
                command.Parameters.AddWithValue("@NgaySinh", txtNgaySinh.Value);
                command.Parameters.AddWithValue("@Tinh", txtTinh.Text);
                command.Parameters.AddWithValue("@MaDoiTuong", cbDoiTuong.SelectedValue);
                command.Parameters.AddWithValue("@MaLop", cbLop.SelectedValue);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                loadAllSV();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            unlockControls();
            flagAction = "edit";
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void updateSinhVien()
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_updateSinhVien";
                command.Connection = conn;
                command.Parameters.AddWithValue("@masv", txtMa.Text);
                command.Parameters.AddWithValue("@HoTen", txtTen.Text);
                command.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.SelectedItem);
                command.Parameters.AddWithValue("@NgaySinh", txtNgaySinh.Value);
                command.Parameters.AddWithValue("@Tinh", txtTinh.Text);
                command.Parameters.AddWithValue("@MaDoiTuong", cbDoiTuong.SelectedValue);
                command.Parameters.AddWithValue("@MaLop", cbLop.SelectedValue);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                loadAllSV();
            }
        }

        private void chinhSuaHocPhiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyMucHocPhi frmHP = new frmQuanLyMucHocPhi();
            frmHP.ShowDialog();
        }


        private void bindingSVToTextbox(DataGridViewRow rowSinhVien)
        {
            txtMa.Text = rowSinhVien.Cells[0].Value.ToString();
            txtTen.Text = rowSinhVien.Cells[1].Value.ToString();
            cbGioiTinh.SelectedIndex = cbGioiTinh.FindString(rowSinhVien.Cells[2].Value.ToString());
            txtNgaySinh.Value = Convert.ToDateTime(rowSinhVien.Cells[3].Value.ToString());
            txtTinh.Text = rowSinhVien.Cells[4].Value.ToString();
            cbLop.SelectedIndex = cbLop.FindString(rowSinhVien.Cells[8].Value.ToString());
        }

        private void dataGridSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvSinhVien = sender as DataGridView;

            if(e.RowIndex < dgvSinhVien.RowCount - 1)
            {
                bindingSVToTextbox(dgvSinhVien.CurrentRow);
                lockControls();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            
        }

        private void unlockButton()
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            resetCotrols();
            unlockButton();
            lockControls();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ban co thuc su muon xoa?", "ThongBao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                deleteSinhVien();
            }
        }

        private void deleteSinhVien()
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_deleteSinhVien";
                command.Connection = conn;
                command.Parameters.AddWithValue("@masv", txtMa.Text);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                loadAllSV();
            }
        }

        private void inBienLaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQLDongHocPhi frQLDHP = new frmQLDongHocPhi();
            frQLDHP.Show();
        }
    }
}