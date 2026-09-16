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
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string namSinhStr = txtNamSinh.Text.Trim();
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            if (string.IsNullOrEmpty(namSinhStr) || !int.TryParse(namSinhStr, out int namSinh))
            {
                MessageBox.Show("Năm sinh không được để trống và phải là số nguyên", "Dữ liệu không hợp l",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }
            int namHienTai = DateTime.Now.Year;
            if (namSinh < 1900 || namSinh > namHienTai)
            {
                MessageBox.Show($"Năm sinh phải nằm trong khoản năm 1900 đến {namHienTai}!", "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email không được để trống", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính", "Thiếu th",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int tuoi = namHienTai - namSinh;
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            string khoa = cboKhoa.SelectedItem.ToString();

            string ketQua = "THÔNG TIN SINH VIÊN" + Environment.NewLine +
                             $"Họ tên: {hoTen}" + Environment.NewLine +
                             $"Tuổi: {tuoi}" + Environment.NewLine +
                             $"Email: {email}" + Environment.NewLine +
                             $"Giới tính: {gioiTinh}" + Environment.NewLine +
                             $"Khoa: {khoa}";

            txtKetQua.Text = ketQua;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtNamSinh.Clear();
            txtEmail.Clear();
            radNam.Checked = false;
            radNu.Checked = false;
            cboKhoa.SelectedIndex = -1;
            txtKetQua.Clear();
            txtHoTen.Focus();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}