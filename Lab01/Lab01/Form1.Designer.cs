using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab01
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblHoTen = new Label();
            this.txtHoTen = new TextBox();
            this.lblNamSinh = new Label();
            this.txtNamSinh = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblGioiTinh = new Label();
            this.radNam = new RadioButton();
            this.radNu = new RadioButton();
            this.lblKhoa = new Label();
            this.cboKhoa = new ComboBox();
            this.btnHienThi = new Button();
            this.btnXoa = new Button();
            this.btnThoat = new Button();
            this.txtKetQua = new TextBox();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(90, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(350, 32);
            this.lblTitle.Text = "THÔNG TIN SINH VIÊN";

            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new Point(40, 80);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new Size(60, 15);
            this.lblHoTen.Text = "Họ tên:";

            this.txtHoTen.Location = new Point(160, 77);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new Size(250, 23);

            this.lblNamSinh.AutoSize = true;
            this.lblNamSinh.Location = new Point(40, 120);
            this.lblNamSinh.Name = "lblNamSinh";
            this.lblNamSinh.Size = new Size(70, 15);
            this.lblNamSinh.Text = "Năm sinh:";

            this.txtNamSinh.Location = new Point(160, 117);
            this.txtNamSinh.Name = "txtNamSinh";
            this.txtNamSinh.Size = new Size(250, 23);

            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(40, 160);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(45, 15);
            this.lblEmail.Text = "Email:";

            this.txtEmail.Location = new Point(160, 157);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(250, 23);

            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Location = new Point(40, 200);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new Size(65, 15);
            this.lblGioiTinh.Text = "Giới tính:";

            this.radNam.AutoSize = true;
            this.radNam.Location = new Point(160, 199);
            this.radNam.Name = "radNam";
            this.radNam.Size = new Size(50, 19);
            this.radNam.Text = "Nam";

            this.radNu.AutoSize = true;
            this.radNu.Location = new Point(240, 199);
            this.radNu.Name = "radNu";
            this.radNu.Size = new Size(42, 19);
            this.radNu.Text = "Nữ";

            this.lblKhoa.AutoSize = true;
            this.lblKhoa.Location = new Point(40, 240);
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Size = new Size(80, 15);
            this.lblKhoa.Text = "Khoa:";

            this.cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboKhoa.Location = new Point(160, 237);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Size = new Size(250, 23);
            this.cboKhoa.Items.AddRange(new object[] {
                "CNTT",
                "Toán",
                "Thể chất",
                "Phùng"});

            this.btnHienThi.Location = new Point(40, 290);
            this.btnHienThi.Name = "btnHienThi";
            this.btnHienThi.Size = new Size(100, 30);
            this.btnHienThi.Text = "Hiển thị";
            this.btnHienThi.UseVisualStyleBackColor = true;
            this.btnHienThi.Click += new EventHandler(this.btnHienThi_Click);

            this.btnXoa.Location = new Point(160, 290);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new Size(100, 30);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new EventHandler(this.btnXoa_Click);

            this.btnThoat.Location = new Point(280, 290);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new Size(100, 30);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new EventHandler(this.btnThoat_Click);

            this.txtKetQua.Location = new Point(40, 340);
            this.txtKetQua.Multiline = true;
            this.txtKetQua.Name = "txtKetQua";
            this.txtKetQua.ReadOnly = true;
            this.txtKetQua.ScrollBars = ScrollBars.Vertical;
            this.txtKetQua.Size = new Size(430, 150);

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(520, 520);
            this.Controls.Add(this.txtKetQua);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnHienThi);
            this.Controls.Add(this.cboKhoa);
            this.Controls.Add(this.lblKhoa);
            this.Controls.Add(this.radNu);
            this.Controls.Add(this.radNam);
            this.Controls.Add(this.lblGioiTinh);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtNamSinh);
            this.Controls.Add(this.lblNamSinh);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.lblTitle);
            this.Name = "Form1";
            this.Text = "THÔNG TIN SINH VIÊN";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblTitle;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblNamSinh;
        private TextBox txtNamSinh;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblGioiTinh;
        private RadioButton radNam;
        private RadioButton radNu;
        private Label lblKhoa;
        private ComboBox cboKhoa;
        private Button btnHienThi;
        private Button btnXoa;
        private Button btnThoat;
        private TextBox txtKetQua;
    }
}