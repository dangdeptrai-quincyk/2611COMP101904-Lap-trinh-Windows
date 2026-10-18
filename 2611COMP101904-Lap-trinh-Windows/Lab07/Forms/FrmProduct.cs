using System.ComponentModel;
using ShopManagerMini.Models;
namespace ShopManagerMini.Forms;

public class FrmProduct : Form
{
    private readonly BindingList<Product> _list = new()
    {
        new Product { MaSP = "SP01", TenSP = "Chuột không dây", LoaiSP = "Điện tử", DonGia = 150000, SoLuong = 30 },
        new Product { MaSP = "SP02", TenSP = "Vở 200 trang", LoaiSP = "Văn phòng", DonGia = 12000, SoLuong = 100 }
    };
    private TextBox txtMa, txtTen, txtLoai, txtTim;
    private NumericUpDown numGia, numSL;
    private DataGridView dgv;

    public FrmProduct()
    {
        Name = "FrmProduct"; Text = "Quản lý sản phẩm"; ClientSize = new Size(760, 480);
        txtMa = new TextBox { Name = "txtMaSP", Width = 120 }; txtTen = new TextBox { Name = "txtTenSP", Width = 160 };
        txtLoai = new TextBox { Name = "txtLoaiSP", Width = 120 };
        numGia = new NumericUpDown { Name = "numDonGia", Width = 120, Maximum = 1000000000, ThousandsSeparator = true };
        numSL = new NumericUpDown { Name = "numSoLuong", Width = 120, Maximum = 1000000 };
        txtTim = new TextBox { Name = "txtTimKiem", Width = 140 };

        var pnl = new Panel { Dock = DockStyle.Top, Height = 140 };
        void Row(string t, Control c, int x, int y) { pnl.Controls.Add(new Label { Text = t, Location = new Point(x, y + 3), AutoSize = true }); c.Location = new Point(x + 80, y); pnl.Controls.Add(c); }
        Row("Mã SP:", txtMa, 10, 10); Row("Tên SP:", txtTen, 10, 40); Row("Loại SP:", txtLoai, 10, 70);
        Row("Đơn giá:", numGia, 330, 10); Row("Số lượng:", numSL, 330, 40); Row("Tìm tên:", txtTim, 330, 70);
        int bx = 10; 
        foreach (var (text, act) in new (string, Action)[] { ("Thêm", Add), ("Sửa", Edit), ("Xóa", Delete), ("Làm mới", Reset), ("Tìm kiếm", Search) })
        {
            var b = new Button { Text = text, Location = new Point(bx, 102), Width = 90 }; b.Click += (s, e) => act(); pnl.Controls.Add(b); bx += 100;
        }
        dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, DataSource = _list };
        dgv.SelectionChanged += (s, e) => { if (dgv.CurrentRow?.DataBoundItem is Product p) Fill(p); };
        Controls.Add(dgv); Controls.Add(pnl);
    }

    private void Fill(Product p) { txtMa.Text = p.MaSP; txtTen.Text = p.TenSP; txtLoai.Text = p.LoaiSP; numGia.Value = p.DonGia; numSL.Value = p.SoLuong; }
    private bool Validate_()
    {
        if (txtMa.Text.Trim() == "" || txtTen.Text.Trim() == "") { MessageBox.Show("Mã và tên sản phẩm không được rỗng."); return false; }
        if (numGia.Value < 0 || numSL.Value < 0) { MessageBox.Show("Đơn giá và số lượng không được âm."); return false; }
        return true;
    }
    private Product Read() => new() { MaSP = txtMa.Text.Trim(), TenSP = txtTen.Text.Trim(), LoaiSP = txtLoai.Text.Trim(), DonGia = numGia.Value, SoLuong = (int)numSL.Value };

    private void Add()
    {
        if (!Validate_()) return;
        if (_list.Any(x => x.MaSP.Equals(txtMa.Text.Trim(), StringComparison.OrdinalIgnoreCase))) { MessageBox.Show("Mã sản phẩm đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        _list.Add(Read());
    }
    private void Edit()
    {
        if (dgv.CurrentRow == null) return;
        if (!Validate_()) return;
        var cur = (Product)dgv.CurrentRow.DataBoundItem;
        if (!cur.MaSP.Equals(txtMa.Text.Trim(), StringComparison.OrdinalIgnoreCase) && _list.Any(x => x.MaSP.Equals(txtMa.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
        { MessageBox.Show("Mã sản phẩm bị trùng."); return; }
        _list[_list.IndexOf(cur)] = Read();
    }
    private void Delete()
    {
        if (dgv.CurrentRow?.DataBoundItem is not Product p) return;
        if (MessageBox.Show($"Xóa '{p.TenSP}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) _list.Remove(p);
    }
    private void Reset() { txtMa.Clear(); txtTen.Clear(); txtLoai.Clear(); numGia.Value = 0; numSL.Value = 0; txtTim.Clear(); dgv.DataSource = _list; txtMa.Focus(); }
    private void Search()
    {
        var r = _list.Where(x => x.TenSP.Contains(txtTim.Text.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
        dgv.DataSource = new BindingList<Product>(r);
    }
}