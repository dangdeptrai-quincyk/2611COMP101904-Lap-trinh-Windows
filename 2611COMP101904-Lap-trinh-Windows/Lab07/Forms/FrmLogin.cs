using ShopManagerMini.Models;
namespace ShopManagerMini.Forms;

public class FrmLogin : Form
{
    private readonly List<User> _users = new()
    {
        new User { Username = "admin", Password = "123", FullName = "Quản trị viên", Role = "Admin" },
        new User { Username = "user",  Password = "123", FullName = "Nhân viên bán hàng", Role = "User" }
    };
    private TextBox txtUsername, txtPassword;
    public User LoggedInUser { get; private set; }

    public FrmLogin()
    {
        Name = "FrmLogin"; Text = "Đăng nhập";
        ClientSize = new Size(340, 190); StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;

        Controls.Add(new Label { Text = "Username:", Location = new Point(20, 25), AutoSize = true });
        txtUsername = new TextBox { Name = "txtUsername", Location = new Point(110, 22), Width = 200 };
        Controls.Add(new Label { Text = "Password:", Location = new Point(20, 65), AutoSize = true });
        txtPassword = new TextBox { Name = "txtPassword", Location = new Point(110, 62), Width = 200, UseSystemPasswordChar = true };
        var btnLogin = new Button { Name = "btnDangNhap", Text = "Đăng nhập", Location = new Point(80, 120), Size = new Size(100, 32) };
        var btnExit = new Button { Name = "btnThoat", Text = "Thoát", Location = new Point(200, 120), Size = new Size(100, 32) };
        btnLogin.Click += btnLogin_Click;
        btnExit.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        Controls.AddRange(new Control[] { txtUsername, txtPassword, btnLogin, btnExit });
        AcceptButton = btnLogin; CancelButton = btnExit;
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string u = txtUsername.Text.Trim(), p = txtPassword.Text;
        if (u == "" || p == "")
        {
            MessageBox.Show("Vui lòng nhập username và password.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var user = _users.FirstOrDefault(x => x.Username == u && x.Password == p);
        if (user == null)
        {
            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear(); txtPassword.Focus();
            return;
        }
        LoggedInUser = user;
        DialogResult = DialogResult.OK;
    }
}