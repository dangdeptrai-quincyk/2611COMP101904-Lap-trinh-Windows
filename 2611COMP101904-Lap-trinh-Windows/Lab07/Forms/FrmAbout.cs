namespace ShopManagerMini.Forms;
public class FrmAbout : Form
{
    public FrmAbout()
    {
        Name = "FrmAbout"; Text = "Giới thiệu"; ClientSize = new Size(380, 160);
        Controls.Add(new Label
        {
            Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter,
            Text = "ShopManagerMini v1.0\nHọ tên SV: Nguyễn Hải Đăng\nLớp: 51.CNTT.A\nMôn: 2611COMP101904 - Lập trình trên Windows"
        });
    }
}