using System.ComponentModel;
namespace ProductManager;

public class FrmProduct : Form
{
    private readonly List<Product> _all = new();
    private readonly BindingList<Product> _view = new();
    private readonly BindingSource _bs = new();
    private readonly ErrorProvider _err = new();
    private string _editingCode;

    private TextBox txtMaSP, txtTenSP, txtTimKiem;
    private ComboBox cboLoaiSP;
    private NumericUpDown numDonGia, numSoLuong;
    private DateTimePicker dtpNgayNhap;
    private CheckBox chkConKinhDoanh;
    private DataGridView dgvProducts;
    private PictureBox picAnh;
    private Button btnChonAnh;
    private ToolStripStatusLabel lblStatus;
    private OpenFileDialog ofd = new() { Filter = "Ảnh|*.jpg;*.jpeg;*.png;*.bmp" };
    private string _imagePath = "";

    public FrmProduct()
    {
        Name = "FrmProduct"; Text = "Quản lý sản phẩm";
        Size = new Size(960, 640); StartPosition = FormStartPosition.CenterScreen;
        BuildUi();
        LoadProducts();
    }

    private void BuildUi()
    {
        var menu = new MenuStrip();
        var mFile = new ToolStripMenuItem("File");
        mFile.DropDownItems.Add("Làm mới", null, (s, e) => ClearInput());
        mFile.DropDownItems.Add("Thoát", null, (s, e) => Close());
        var mManage = new ToolStripMenuItem("Quản lý");
        mManage.DropDownItems.Add("Thêm", null, (s, e) => AddProduct());
        mManage.DropDownItems.Add("Sửa", null, (s, e) => EditProduct());
        mManage.DropDownItems.Add("Xóa", null, (s, e) => DeleteProduct());
        var mHelp = new ToolStripMenuItem("Trợ giúp");
        mHelp.DropDownItems.Add("Giới thiệu", null, (s, e) => MessageBox.Show("ProductManager - Lab 05 (Buổi 6)"));
        menu.Items.AddRange(new ToolStripItem[] { mFile, mManage, mHelp });

        var tool = new ToolStrip();
        tool.Items.Add("Thêm", null, (s, e) => AddProduct());
        tool.Items.Add("Sửa", null, (s, e) => EditProduct());
        tool.Items.Add("Xóa", null, (s, e) => DeleteProduct());
        tool.Items.Add("Làm mới", null, (s, e) => ClearInput());
        tool.Items.Add(new ToolStripSeparator());
        txtTimKiem = new TextBox { Width = 160 };
        var btnSearch = new ToolStripButton("Tìm kiếm");
        btnSearch.Click += (s, e) => SearchProducts();
        tool.Items.Add(new ToolStripControlHost(txtTimKiem));
        tool.Items.Add(btnSearch);

        var status = new StatusStrip();
        lblStatus = new ToolStripStatusLabel("Sẵn sàng");
        status.Items.Add(lblStatus);

        var grp = new GroupBox { Text = "Thông tin sản phẩm", Dock = DockStyle.Top, Height = 190 };
        txtMaSP = new TextBox { Name = "txtMaSP", Width = 160 };
        txtTenSP = new TextBox { Name = "txtTenSP", Width = 160 };
        cboLoaiSP = new ComboBox { Name = "cboLoaiSP", Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
        cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });
        numDonGia = new NumericUpDown { Name = "numDonGia", Width = 160, Maximum = 1000000000, Increment = 1000, ThousandsSeparator = true };
        numSoLuong = new NumericUpDown { Name = "numSoLuong", Width = 160, Maximum = 1000000 };
        dtpNgayNhap = new DateTimePicker { Name = "dtpNgayNhap", Width = 160, Format = DateTimePickerFormat.Short };
        chkConKinhDoanh = new CheckBox { Name = "chkConKinhDoanh", Text = "Còn kinh doanh", Checked = true, AutoSize = true };
        picAnh = new PictureBox { Size = new Size(120, 130), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };
        btnChonAnh = new Button { Text = "Chọn ảnh", Width = 100 };
        btnChonAnh.Click += (s, e) => ChooseImage();

        void Row(string text, Control c, int x, int y)
        {
            grp.Controls.Add(new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true });
            c.Location = new Point(x + 90, y); grp.Controls.Add(c);
        }
        Row("Mã SP:", txtMaSP, 15, 25);       Row("Tên SP:", txtTenSP, 15, 60);
        Row("Loại SP:", cboLoaiSP, 15, 95);   Row("Ngày nhập:", dtpNgayNhap, 15, 130);
        Row("Đơn giá:", numDonGia, 330, 25);  Row("Số lượng:", numSoLuong, 330, 60);
        chkConKinhDoanh.Location = new Point(420, 95); grp.Controls.Add(chkConKinhDoanh);
        picAnh.Location = new Point(620, 20); btnChonAnh.Location = new Point(630, 155);
        grp.Controls.AddRange(new Control[] { picAnh, btnChonAnh });

        dgvProducts = new DataGridView
        {
            Name = "dgvProducts", Dock = DockStyle.Fill, ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false,
            AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        _bs.DataSource = _view; dgvProducts.DataSource = _bs;
        dgvProducts.SelectionChanged += (s, e) => UpdateStatus("Đã chọn dòng");
        var ctx = new ContextMenuStrip();
        ctx.Items.Add("Sửa", null, (s, e) => EditProduct());
        ctx.Items.Add("Xóa", null, (s, e) => DeleteProduct());
        ctx.Items.Add("Xem chi tiết", null, (s, e) => ShowDetail());
        dgvProducts.ContextMenuStrip = ctx;
        dgvProducts.CellMouseDown += (s, e) =>
        {   
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0) dgvProducts.Rows[e.RowIndex].Selected = true;
        };

        Controls.Add(dgvProducts); Controls.Add(grp); Controls.Add(tool); Controls.Add(menu); Controls.Add(status);
        MainMenuStrip = menu;
        KeyPreview = true;
        KeyDown += (s, e) =>
        {
            if (e.Control && e.KeyCode == Keys.N) AddProduct();
            else if (e.KeyCode == Keys.Delete && dgvProducts.Focused) DeleteProduct();
        };
    }
    private void LoadProducts()
    {
        _all.Add(new Product { MaSP = "SP01", TenSP = "Laptop Dell", LoaiSP = "Điện tử", DonGia = 15000000, SoLuong = 10, NgayNhap = DateTime.Today.AddDays(-30), ConKinhDoanh = true });
        _all.Add(new Product { MaSP = "SP02", TenSP = "Bút bi Thiên Long", LoaiSP = "Văn phòng", DonGia = 5000, SoLuong = 500, NgayNhap = DateTime.Today.AddDays(-10), ConKinhDoanh = true });
        _all.Add(new Product { MaSP = "SP03", TenSP = "Nồi cơm điện", LoaiSP = "Gia dụng", DonGia = 1200000, SoLuong = 25, NgayNhap = DateTime.Today.AddDays(-5), ConKinhDoanh = false });
        RefreshGrid(_all);
        ClearInput();
    }
    private void RefreshGrid(IEnumerable<Product> source)
    {
        _view.Clear();
        foreach (var p in source) _view.Add(p);
        UpdateStatus("Đã cập nhật danh sách");
    }
    private void UpdateStatus(string action) => lblStatus.Text = $"{action} | Số sản phẩm: {_all.Count}";

    private Product Selected => dgvProducts.CurrentRow?.DataBoundItem as Product;
    private bool ValidateInput()
    {
        _err.Clear();
        bool ok = true;
        void Fail(Control c, string m) { _err.SetError(c, m); ok = false; }

        if (string.IsNullOrWhiteSpace(txtMaSP.Text)) Fail(txtMaSP, "Mã sản phẩm không được rỗng");
        else if (_editingCode == null && _all.Any(p => p.MaSP.Equals(txtMaSP.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            Fail(txtMaSP, "Mã sản phẩm đã tồn tại");
        if (string.IsNullOrWhiteSpace(txtTenSP.Text)) Fail(txtTenSP, "Tên sản phẩm không được rỗng");
        if (cboLoaiSP.SelectedIndex < 0) Fail(cboLoaiSP, "Vui lòng chọn loại sản phẩm");
        if (numDonGia.Value <= 0) Fail(numDonGia, "Đơn giá phải lớn hơn 0");
        if (numSoLuong.Value < 0) Fail(numSoLuong, "Số lượng phải >= 0");
        if (dtpNgayNhap.Value.Date > DateTime.Today) Fail(dtpNgayNhap, "Ngày nhập không được lớn hơn hôm nay");
        if (!ok) MessageBox.Show("Dữ liệu nhập chưa hợp lệ, vui lòng kiểm tra các ô báo lỗi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return ok;
    }

    private Product ReadInput() => new()
    {
        MaSP = txtMaSP.Text.Trim(), TenSP = txtTenSP.Text.Trim(), LoaiSP = cboLoaiSP.Text,
        DonGia = numDonGia.Value, SoLuong = (int)numSoLuong.Value, NgayNhap = dtpNgayNhap.Value.Date,
        ConKinhDoanh = chkConKinhDoanh.Checked, DuongDanAnh = _imagePath
    };
    private void AddProduct()
    {
        _editingCode = null;
        if (!ValidateInput()) return;
        _all.Add(ReadInput());
        RefreshGrid(_all); ClearInput(); UpdateStatus("Đã thêm sản phẩm");
    }
    private void EditProduct()
    {
        var p = Selected;
        if (p == null) { MessageBox.Show("Vui lòng chọn một dòng để sửa.", "Thông báo"); return; }
        if (_editingCode == null)
        {   
            _editingCode = p.MaSP; LoadProductToForm(p); txtMaSP.ReadOnly = true;
            UpdateStatus("Đang sửa - chỉnh dữ liệu rồi bấm Sửa lần nữa để lưu");
            return;
        }
        if (!ValidateInput()) return;
        var edited = ReadInput();
        int idx = _all.FindIndex(x => x.MaSP == _editingCode);
        _all[idx] = edited;
        RefreshGrid(_all); ClearInput(); UpdateStatus("Đã sửa sản phẩm");
    }
    private void DeleteProduct()
    {
        var p = Selected;
        if (p == null) { MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo"); return; }
        if (MessageBox.Show($"Xóa sản phẩm '{p.TenSP}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        _all.Remove(p);
        RefreshGrid(_all); ClearInput(); UpdateStatus("Đã xóa sản phẩm");
    }
    private void SearchProducts()
    {
        string kw = txtTimKiem.Text.Trim();
        var result = _all.Where(p => p.MaSP.Contains(kw, StringComparison.OrdinalIgnoreCase)
                                  || p.TenSP.Contains(kw, StringComparison.OrdinalIgnoreCase));
        RefreshGrid(result); UpdateStatus("Kết quả tìm kiếm");
    }
    private void ShowDetail()
    {
        var p = Selected; if (p == null) return;
        MessageBox.Show($"Mã: {p.MaSP}\nTên: {p.TenSP}\nLoại: {p.LoaiSP}\nĐơn giá: {p.DonGia:N0}\nSố lượng: {p.SoLuong}\n" +
                        $"Ngày nhập: {p.NgayNhap:dd/MM/yyyy}\nKinh doanh: {(p.ConKinhDoanh ? "Còn" : "Ngừng")}", "Chi tiết sản phẩm");
    }

    private void LoadProductToForm(Product p)
    {
        txtMaSP.Text = p.MaSP; txtTenSP.Text = p.TenSP; cboLoaiSP.SelectedItem = p.LoaiSP;
        numDonGia.Value = p.DonGia; numSoLuong.Value = p.SoLuong; dtpNgayNhap.Value = p.NgayNhap;
        chkConKinhDoanh.Checked = p.ConKinhDoanh; ShowImage(p.DuongDanAnh);
    }
    private void ClearInput()
    {
        _err.Clear(); _editingCode = null; txtMaSP.ReadOnly = false;
        txtMaSP.Clear(); txtTenSP.Clear(); cboLoaiSP.SelectedIndex = -1;
        numDonGia.Value = 0; numSoLuong.Value = 0; dtpNgayNhap.Value = DateTime.Today;
        chkConKinhDoanh.Checked = true; txtTimKiem.Clear(); ShowImage("");
        RefreshGrid(_all); txtMaSP.Focus();
    }
    private void ChooseImage()
    {
        if (ofd.ShowDialog() == DialogResult.OK) ShowImage(ofd.FileName);
    }
    private void ShowImage(string path)
    {
        _imagePath = path ?? "";
        picAnh.Image?.Dispose();
        if (string.IsNullOrEmpty(_imagePath) || !File.Exists(_imagePath)) { picAnh.Image = null; return; }
        using var fs = new FileStream(_imagePath, FileMode.Open, FileAccess.Read);
        picAnh.Image = new Bitmap(Image.FromStream(fs));
    }
}