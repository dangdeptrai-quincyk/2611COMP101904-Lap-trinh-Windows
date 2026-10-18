using System.Drawing.Drawing2D;
using System.Drawing.Printing;
namespace StudentCard;

public class FrmStudentCard : Form
{
    private TextBox txtMaSV, txtHoTen, txtKhoaHoc;
    private ComboBox cboLop, cboKhoa, cboGioiTinh;
    private DateTimePicker dtpNgaySinh;
    private PictureBox picAvatar;
    private Panel pnlPreview;
    private Button btnChonAnh, btnLamMoi, btnPreview, btnIn, btnThoat;
    private readonly OpenFileDialog ofd = new() { Filter = "Ảnh (JPG, JPEG, PNG, BMP)|*.jpg;*.jpeg;*.png;*.bmp" };
    private readonly PrintDocument printDoc = new();
    private Image _avatar;

    public FrmStudentCard()
    {
        Name = "FrmStudentCard"; Text = "Thẻ sinh viên";
        ClientSize = new Size(980, 520); StartPosition = FormStartPosition.CenterScreen;

        var left = new GroupBox { Text = "Thông tin sinh viên", Location = new Point(10, 10), Size = new Size(380, 495) };
        txtMaSV = new TextBox { Name = "txtMaSV", Width = 220 };
        txtHoTen = new TextBox { Name = "txtHoTen", Width = 220 };
        cboLop = new ComboBox { Name = "cboLop", Width = 220 }; cboLop.Items.AddRange(new object[] { "DH22TIN01", "DH22TIN02", "DH23TIN01" });
        cboKhoa = new ComboBox { Name = "cboKhoa", Width = 220 }; cboKhoa.Items.AddRange(new object[] { "Công nghệ Thông tin", "Toán - Tin", "Vật lý" });
        txtKhoaHoc = new TextBox { Name = "txtKhoaHoc", Width = 220, Text = "K49" };
        dtpNgaySinh = new DateTimePicker { Name = "dtpNgaySinh", Width = 220, Format = DateTimePickerFormat.Short };
        cboGioiTinh = new ComboBox { Name = "cboGioiTinh", Width = 220, DropDownStyle = ComboBoxStyle.DropDownList }; cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
        picAvatar = new PictureBox { Name = "picAvatar", Size = new Size(120, 150), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };
        btnChonAnh = new Button { Name = "btnChonAnh", Text = "Chọn ảnh (F1)", Width = 120 };

        int y = 28;
        void Row(string t, Control c) { left.Controls.Add(new Label { Text = t, Location = new Point(12, y + 3), AutoSize = true }); c.Location = new Point(120, y); left.Controls.Add(c); y += 36; }
        Row("Mã SV:", txtMaSV); Row("Họ tên:", txtHoTen); Row("Lớp:", cboLop); Row("Khoa:", cboKhoa);
        Row("Khóa học:", txtKhoaHoc); Row("Ngày sinh:", dtpNgaySinh); Row("Giới tính:", cboGioiTinh);
        picAvatar.Location = new Point(120, y + 5); btnChonAnh.Location = new Point(250, y + 5 + 60);
        left.Controls.AddRange(new Control[] { picAvatar, btnChonAnh });

        btnLamMoi = new Button { Name = "btnLamMoi", Text = "Làm mới (F6)", Location = new Point(12, 450), Width = 110 };
        btnPreview = new Button { Name = "btnXemTruocIn", Text = "Xem trước in (F4)", Location = new Point(128, 450), Width = 120 };
        btnIn = new Button { Name = "btnIn", Text = "In (F5)", Location = new Point(254, 450), Width = 55 };
        btnThoat = new Button { Name = "btnThoat", Text = "Thoát", Location = new Point(315, 450), Width = 55 };
        left.Controls.AddRange(new Control[] { btnLamMoi, btnPreview, btnIn, btnThoat });

        pnlPreview = new DoubleBufferedPanel { Name = "pnlPreview", Location = new Point(405, 40), Size = new Size(560, 350), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
        Controls.AddRange(new Control[] { left, new Label { Text = "XEM TRƯỚC THẺ", Location = new Point(405, 15), AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, pnlPreview });

        pnlPreview.Paint += (s, e) => DrawCard(e.Graphics, new Rectangle(0, 0, pnlPreview.Width - 1, pnlPreview.Height - 1));
        foreach (var c in new Control[] { txtMaSV, txtHoTen, cboLop, cboKhoa, txtKhoaHoc, cboGioiTinh })
        { c.TextChanged += (s, e) => pnlPreview.Invalidate(); }
        cboLop.SelectedIndexChanged += (s, e) => pnlPreview.Invalidate();
        cboKhoa.SelectedIndexChanged += (s, e) => pnlPreview.Invalidate();
        cboGioiTinh.SelectedIndexChanged += (s, e) => pnlPreview.Invalidate();
        dtpNgaySinh.ValueChanged += (s, e) => pnlPreview.Invalidate();
        btnChonAnh.Click += (s, e) => ChooseImage();
        btnLamMoi.Click += (s, e) => ResetForm();
        btnPreview.Click += (s, e) => PrintPreview();
        btnIn.Click += (s, e) => Print();
        btnThoat.Click += (s, e) => Close();
        printDoc.DefaultPageSettings.Landscape = true;
        printDoc.PrintPage += PrintDoc_PrintPage;

        KeyPreview = true;
        KeyDown += (s, e) =>
        {
            switch (e.KeyCode)
            { case Keys.F1: ChooseImage(); break; case Keys.F4: PrintPreview(); break; case Keys.F5: Print(); break; case Keys.F6: ResetForm(); break; }
        };
        ResetForm();
    }

    private void ChooseImage()
    {
        if (ofd.ShowDialog() != DialogResult.OK) return;
        try
        {
            using var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
            using var tmp = Image.FromStream(fs);
            SetAvatar(new Bitmap(tmp));
        }
        catch (Exception) { MessageBox.Show("Không thể mở file ảnh này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    private void SetAvatar(Image img)
    {
        _avatar?.Dispose(); _avatar = img;
        picAvatar.Image = _avatar;
        pnlPreview.Invalidate();
    }

    private bool ValidateInput()
    {
        string msg = "";
        if (string.IsNullOrWhiteSpace(txtMaSV.Text)) msg = "Mã sinh viên không được rỗng.";
        else if (string.IsNullOrWhiteSpace(txtHoTen.Text)) msg = "Họ tên không được rỗng.";
        else if (_avatar == null) msg = "Vui lòng chọn ảnh đại diện.";
        if (msg == "") return true;
        MessageBox.Show(msg, "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    private void DrawCard(Graphics g, Rectangle r)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        using (var bg = new SolidBrush(Color.FromArgb(235, 244, 255))) g.FillRectangle(bg, r);
        using (var border = new Pen(Color.FromArgb(0, 70, 140), Math.Max(2, r.Width / 150f))) g.DrawRectangle(border, r);

        float k = r.Width / 560f;
        using var fSchool = new Font("Segoe UI", 11 * k, FontStyle.Bold);
        using var fTitle = new Font("Segoe UI", 20 * k, FontStyle.Bold);
        using var fText = new Font("Segoe UI", 12 * k);
        using var fBold = new Font("Segoe UI", 12 * k, FontStyle.Bold);
        using var navy = new SolidBrush(Color.FromArgb(0, 70, 140));
        using var black = new SolidBrush(Color.Black);
        var center = new StringFormat { Alignment = StringAlignment.Center };

        g.DrawString("TRƯỜNG ĐẠI HỌC SƯ PHẠM TP. HỒ CHÍ MINH", fSchool, navy, new RectangleF(r.X, r.Y + 12 * k, r.Width, 30 * k), center);
        g.DrawString($"Khoa: {cboKhoa.Text}", fText, black, new RectangleF(r.X, r.Y + 36 * k, r.Width, 26 * k), center);
        g.DrawString("THẺ SINH VIÊN", fTitle, navy, new RectangleF(r.X, r.Y + 62 * k, r.Width, 40 * k), center);

        var photo = new RectangleF(r.X + 25 * k, r.Y + 125 * k, 130 * k, 170 * k);
        g.FillRectangle(Brushes.White, photo);
        if (_avatar != null)
        {   
            float ratio = Math.Min(photo.Width / _avatar.Width, photo.Height / _avatar.Height);
            float w = _avatar.Width * ratio, h = _avatar.Height * ratio;
            g.DrawImage(_avatar, photo.X + (photo.Width - w) / 2, photo.Y + (photo.Height - h) / 2, w, h);
        }
        g.DrawRectangle(Pens.Gray, photo.X, photo.Y, photo.Width, photo.Height);

        float x = r.X + 175 * k, y = r.Y + 125 * k, lh = 30 * k;
        void Line(string label, string value)
        {
            g.DrawString(label, fBold, black, x, y);
            g.DrawString(value, fText, black, x + 110 * k, y);
            y += lh;
        }
        Line("Mã SV:", txtMaSV.Text); Line("Họ tên:", txtHoTen.Text); Line("Lớp:", cboLop.Text);
        Line("Khóa học:", txtKhoaHoc.Text); Line("Ngày sinh:", dtpNgaySinh.Value.ToString("dd/MM/yyyy")); Line("Giới tính:", cboGioiTinh.Text);
    }

    private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
    {
        var bounds = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y, 800, 500);
        DrawCard(e.Graphics, bounds);
        e.HasMorePages = false;
    }
    private void PrintPreview()
    {
        if (!ValidateInput()) return;
        using var dlg = new PrintPreviewDialog { Document = printDoc, Width = 1000, Height = 700 };
        dlg.ShowDialog();
    }
    private void Print()
    {
        if (!ValidateInput()) return;
        using var dlg = new PrintDialog { Document = printDoc };
        if (dlg.ShowDialog() == DialogResult.OK) printDoc.Print();
    }

    private void ResetForm()
    {
        txtMaSV.Clear(); txtHoTen.Clear(); txtKhoaHoc.Text = "K49";
        cboLop.SelectedIndex = 0; cboKhoa.SelectedIndex = 0; cboGioiTinh.SelectedIndex = 0;
        dtpNgaySinh.Value = new DateTime(2004, 1, 1);
        SetAvatar(null); txtMaSV.Focus();
    }
}

public class DoubleBufferedPanel : Panel { public DoubleBufferedPanel() { DoubleBuffered = true; ResizeRedraw = true; } }