namespace ShopManagerMini.Forms;
public class FrmAbout : Form
{
    public FrmAbout()
    {
        Name = "FrmAbout"; Text = "Giới thiệu"; ClientSize = new Size(380, 160);
        Controls.Add(new Label
        {
            Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter,
            Text = "ShopManagerMini v1.0\nHọ tên SV: ........................\nLớp: ........................\nMôn: COMP1019 - Lập trình trên Windows"
        });
    }
}