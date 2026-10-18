using ShopManagerMini.Models;
namespace ShopManagerMini.Forms;

public class FrmMain : Form
{
    private readonly User _user;
    private readonly ToolStripStatusLabel lblUser = new(), lblTime = new();
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 1000 };

    public FrmMain(User user)
    {
        _user = user;
        Name = "FrmMain"; Text = "ShopManagerMini";
        IsMdiContainer = true; WindowState = FormWindowState.Maximized;

        var menu = new MenuStrip();
        var mSys = new ToolStripMenuItem("Hệ thống");
        mSys.DropDownItems.Add("Đăng xuất", null, (s, e) => { if (Confirm("Đăng xuất?")) { Hide(); Restart(); } });
        mSys.DropDownItems.Add("Thoát", null, (s, e) => Close());
        var mCat = new ToolStripMenuItem("Danh mục");
        mCat.DropDownItems.Add("Quản lý sản phẩm", null, (s, e) => OpenChild<FrmProduct>());
        mCat.DropDownItems.Add("Quản lý khách hàng", null, (s, e) => OpenChild<FrmCustomer>());
        var mHelp = new ToolStripMenuItem("Trợ giúp");
        mHelp.DropDownItems.Add("Giới thiệu", null, (s, e) => OpenChild<FrmAbout>());
        menu.Items.AddRange(new ToolStripItem[] { mSys, mCat, mHelp });
        MainMenuStrip = menu;

        var status = new StatusStrip();
        lblUser.Text = $"Người dùng: {user.FullName} | Vai trò: {user.Role}";
        status.Items.AddRange(new ToolStripItem[] { lblUser, new ToolStripStatusLabel { Spring = true }, lblTime });
        Controls.Add(menu); Controls.Add(status);

        _timer.Tick += (s, e) => lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        _timer.Start();
        FormClosing += (s, e) =>
        {
            if (!_restarting && !Confirm("Bạn có chắc muốn thoát chương trình?")) e.Cancel = true;
        };
    }

    private bool _restarting;
    private void Restart() { _restarting = true; Application.Restart(); Environment.Exit(0); }
    private static bool Confirm(string m) =>
        MessageBox.Show(m, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    private void OpenChild<T>() where T : Form, new()
    {
        var existing = MdiChildren.OfType<T>().FirstOrDefault();
        if (existing != null)
        {
            if (existing.WindowState == FormWindowState.Minimized) existing.WindowState = FormWindowState.Normal;
            existing.Activate();
            return;
        }
        new T { MdiParent = this }.Show();
    }
}