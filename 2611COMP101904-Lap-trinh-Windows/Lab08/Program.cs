namespace COMP1019_Lab08_SimplePaint;
static class Program
{
    [STAThread]
    static void Main() { ApplicationConfiguration.Initialize(); Application.Run(new FrmPaint()); }
}