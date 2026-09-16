using System;
using System.Windows.Forms;

namespace Lab01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(tenDangNhap) ||
                string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(email))
            {
                lblKetQua.ForeColor = System.Drawing.Color.Red;
                lblKetQua.Text = "Vui lòng nhập thông tin";
                return;
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                lblKetQua.ForeColor = System.Drawing.Color.Red;
                lblKetQua.Text = "Email không hợp lệ";
                return;
            }
            if (matKhau.Length < 6)
            {
                lblKetQua.ForeColor = System.Drawing.Color.Red;
                lblKetQua.Text = "Mật khẩu phải ít nhất 6 ký tự";
                return;
            }
            lblKetQua.ForeColor = System.Drawing.Color.Green;
            lblKetQua.Text = $"Đăng ký thành công, Chào mừng {hoTen}.";
        }
    }
}