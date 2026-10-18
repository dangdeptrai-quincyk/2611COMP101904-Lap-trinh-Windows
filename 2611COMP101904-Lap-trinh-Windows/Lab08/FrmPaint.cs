using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace COMP1019_Lab08_SimplePaint;

public class BufferedPanel : Panel
{
    public BufferedPanel() { DoubleBuffered = true; ResizeRedraw = true; }
}

public class FrmPaint : Form
{
    private readonly List<DrawItem> _items = new();
    private DrawItem _current;
    private bool _drawing;
    private Color _color = Color.Black;

    private ComboBox cboMode;
    private NumericUpDown nudWidth;
    private Button btnColor, btnUndo, btnClear, btnSave;
    private BufferedPanel canvas;

    public FrmPaint()
    {
        Text = "COMP1019 - Simple Paint"; ClientSize = new Size(900, 620); StartPosition = FormStartPosition.CenterScreen;
        KeyPreview = true;

        var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(6) };
        cboMode = new ComboBox { Name = "cboMode", DropDownStyle = ComboBoxStyle.DropDownList, Width = 110 };
        cboMode.Items.AddRange(new object[] { "Pen", "Line", "Rectangle", "Ellipse" }); cboMode.SelectedIndex = 0;
        nudWidth = new NumericUpDown { Name = "nudWidth", Minimum = 1, Maximum = 20, Value = 3, Width = 60 };
        btnColor = new Button { Name = "btnColor", Text = "Màu", BackColor = _color, ForeColor = Color.White, Width = 70 };
        btnUndo = new Button { Name = "btnUndo", Text = "Undo", Width = 70 };
        btnClear = new Button { Name = "btnClear", Text = "Clear", Width = 70 };
        btnSave = new Button { Name = "btnSave", Text = "Lưu PNG", Width = 80 };
        top.Controls.AddRange(new Control[] { new Label { Text = "Chế độ:", AutoSize = true, Margin = new Padding(3, 7, 3, 0) }, cboMode,
            new Label { Text = "Độ dày:", AutoSize = true, Margin = new Padding(10, 7, 3, 0) }, nudWidth, btnColor, btnUndo, btnClear, btnSave });

        var status = new StatusStrip();
        var tsl = new ToolStripStatusLabel("X: 0, Y: 0");
        status.Items.Add(tsl);

        canvas = new BufferedPanel { Name = "canvas", Dock = DockStyle.Fill, BackColor = Color.White };
        Controls.Add(canvas); Controls.Add(top); Controls.Add(status);

        canvas.Paint += Canvas_Paint;
        canvas.MouseDown += Canvas_MouseDown;
        canvas.MouseMove += (s, e) =>
        {
            tsl.Text = $"X: {e.X}, Y: {e.Y}";
            if (!_drawing) return;
            if (_current.Mode == DrawMode.Pen) _current.Points.Add(e.Location);
            else _current.End = e.Location;
            canvas.Invalidate();
        };
        canvas.MouseUp += (s, e) =>
        {
            if (!_drawing) return;
            _drawing = false; canvas.Capture = false;
            if (_current.Mode != DrawMode.Pen) _current.End = e.Location;
            canvas.Invalidate();
        };
        btnColor.Click += (s, e) =>
        {
            using var dlg = new ColorDialog { Color = _color };
            if (dlg.ShowDialog() == DialogResult.OK) { _color = dlg.Color; btnColor.BackColor = _color; btnColor.ForeColor = _color.GetBrightness() > 0.5 ? Color.Black : Color.White; }
        };
        btnUndo.Click += (s, e) => Undo();
        btnClear.Click += (s, e) => { _items.Clear(); canvas.Invalidate(); };
        btnSave.Click += SaveImage;
        KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.Z) { Undo(); e.Handled = true; } };
    }

    private void Canvas_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;
        _drawing = true;
        _current = new DrawItem { Mode = (DrawMode)cboMode.SelectedIndex, Color = _color, Width = (int)nudWidth.Value, Start = e.Location, End = e.Location };
        if (_current.Mode == DrawMode.Pen) _current.Points.Add(e.Location);
        _items.Add(_current);
        canvas.Capture = true;
    }

    private void Canvas_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        foreach (var item in _items) item.Draw(e.Graphics);
    }

    private void Undo()
    {
        if (_items.Count == 0) return;
        _items.RemoveAt(_items.Count - 1);
        canvas.Invalidate();
    }

    private void SaveImage(object sender, EventArgs e)
    {
        using var dlg = new SaveFileDialog { Filter = "PNG|*.png", FileName = "paint.png" };
        if (dlg.ShowDialog() != DialogResult.OK) return;
        using var bmp = new Bitmap(canvas.Width, canvas.Height);
        canvas.DrawToBitmap(bmp, new Rectangle(0, 0, canvas.Width, canvas.Height));
        bmp.Save(dlg.FileName, ImageFormat.Png);
    }
}