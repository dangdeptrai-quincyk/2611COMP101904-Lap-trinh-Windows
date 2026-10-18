using ShopManagerMini.Forms;
using ShopManagerMini.Models;
namespace ShopManagerMini;
static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        using var login = new FrmLogin();
        if (login.ShowDialog() == DialogResult.OK)
            Application.Run(new FrmMain(login.LoggedInUser));
    }
}