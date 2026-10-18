using System.ComponentModel;
using ShopManagerMini.Models;
namespace ShopManagerMini.Forms;

public class FrmCustomer : Form
{
    private readonly BindingList<Customer> _list = new()
    {
        new Customer { MaKH = "KH01", HoTen = "Nguyễn Văn A", SoDienThoai = "0901234567", LoaiKhach = "Thường" },
        new Customer { MaKH = "KH02", HoTen = "Trần Thị B", SoDienThoai = "0912345678", LoaiKhach = "VIP" }
    };
    private TextBox txtMa, txtTen, txtSdt, txtTim; private ComboBox cboLoai; private DataGridView dgv;

    public FrmCustomer()
    {
        Name = "FrmCustomer"; Text = "Quản lý khách hàng"; ClientSize = new Size(700, 450);
        txtMa = new TextBox { Name = "txtMaKH", Width = 140 }; txtTen = new TextBox { Name = "txtHoTen", Width = 180 };
        txtSdt = new TextBox { Name = "txtSDT", Width = 140 }; txtTim = new TextBox { Name = "txtTimKiem", Width = 140 };
        cboLoai = new ComboBox { Name = "cboLoaiKhach", Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
        cboLoai.Items.AddRange(new object[] { "Thường", "VIP" }); cboLoai.SelectedIndex = 0;

        var pnl = new Panel { Dock = DockStyle.Top, Height = 140 };
        void Row(string t, Control c, int x, int y) { pnl.Controls.Add(new Label { Text = t, Location = new Point(x, y + 3), AutoSize = true }); c.Location = new Point(x + 85, y); pnl.Controls.Add(c); }
        Row("Mã KH:", txtMa, 10, 10); Row("Họ tên:", txtTen, 10, 40); Row("SĐT:", txtSdt, 330, 10); Row("Loại KH:", cboLoai, 330, 40); Row("Tìm tên:", txtTim, 10, 70);
        int bx = 10;
        foreach (var (t, a) in new (string, Action)[] { ("Thêm", Add), ("Sửa", Edit), ("Xóa", Delete), ("Tìm kiếm", Search), ("Làm mới", Reset) })
        { var b = new Button { Text = t, Location = new Point(bx, 102), Width = 90 }; b.Click += (s, e) => a(); pnl.Controls.Add(b); bx += 100; }

        dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, DataSource = _list };
        dgv.SelectionChanged += (s, e) => { if (dgv.CurrentRow?.DataBoundItem is Customer c) { txtMa.Text = c.MaKH; txtTen.Text = c.HoTen; txtSdt.Text = c.SoDienThoai; cboLoai.Text = c.LoaiKhach; } };
        Controls.Add(dgv); Controls.Add(pnl);
    }
    private Customer Read() => new() { MaKH = txtMa.Text.Trim(), HoTen = txtTen.Text.Trim(), SoDienThoai = txtSdt.Text.Trim(), LoaiKhach = cboLoai.Text };
    private bool Valid()
    {
        if (txtMa.Text.Trim() == "" || txtTen.Text.Trim() == "") { MessageBox.Show("Mã KH và họ tên không được rỗng."); return false; }
        return true;
    }
    private void Add()
    {
        if (!Valid()) return;
        if (_list.Any(x => x.MaKH.Equals(txtMa.Text.Trim(), StringComparison.OrdinalIgnoreCase))) { MessageBox.Show("Mã khách hàng đã tồn tại."); return; }
        _list.Add(Read());
    }
    private void Edit() { if (dgv.CurrentRow?.DataBoundItem is Customer c && Valid()) _list[_list.IndexOf(c)] = Read(); }
    private void Delete()
    {
        if (dgv.CurrentRow?.DataBoundItem is Customer c && MessageBox.Show("Xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) _list.Remove(c);
    }
    private void Search() => dgv.DataSource = new BindingList<Customer>(_list.Where(x => x.HoTen.Contains(txtTim.Text.Trim(), StringComparison.OrdinalIgnoreCase)).ToList());
    private void Reset() { txtMa.Clear(); txtTen.Clear(); txtSdt.Clear(); txtTim.Clear(); cboLoai.SelectedIndex = 0; dgv.DataSource = _list; }
}