namespace QuanLyThuTienHoc.Forms
{
    partial class frmQLDongHocPhi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridSinhVien = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSinhVien)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(118, 122);
            this.txtTimKiem.Multiline = true;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(303, 39);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tim Kiem";
            // 
            // dataGridSinhVien
            // 
            this.dataGridSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSinhVien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridSinhVien.Location = new System.Drawing.Point(0, 179);
            this.dataGridSinhVien.Name = "dataGridSinhVien";
            this.dataGridSinhVien.RowTemplate.Height = 28;
            this.dataGridSinhVien.Size = new System.Drawing.Size(718, 297);
            this.dataGridSinhVien.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 51);
            this.button1.TabIndex = 3;
            this.button1.Text = "In chi tiet bien lai";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(597, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 51);
            this.button2.TabIndex = 4;
            this.button2.Text = "Giao dich";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // frmQLDongHocPhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 476);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridSinhVien);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTimKiem);
            this.Name = "frmQLDongHocPhi";
            this.Text = "frmQLDongHocPhi";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSinhVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridSinhVien;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}