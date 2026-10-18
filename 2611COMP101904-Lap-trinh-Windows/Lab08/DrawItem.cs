namespace COMP1019_Lab08_SimplePaint;

public enum DrawMode { Pen, Line, Rectangle, Ellipse }

public class DrawItem
{
    public DrawMode Mode { get; set; }
    public Color Color { get; set; }
    public int Width { get; set; }
    public List<Point> Points { get; } = new();
    public Point Start { get; set; }
    public Point End { get; set; }

    public Rectangle GetRect() => new(
        Math.Min(Start.X, End.X), Math.Min(Start.Y, End.Y),
        Math.Abs(End.X - Start.X), Math.Abs(End.Y - Start.Y));

    public void Draw(Graphics g)
    {
        using var pen = new Pen(Color, Width) { StartCap = System.Drawing.Drawing2D.LineCap.Round, EndCap = System.Drawing.Drawing2D.LineCap.Round, LineJoin = System.Drawing.Drawing2D.LineJoin.Round };
        switch (Mode)
        {
            case DrawMode.Pen:
                if (Points.Count == 1) g.DrawEllipse(pen, Points[0].X, Points[0].Y, 1, 1);
                else if (Points.Count > 1) g.DrawLines(pen, Points.ToArray());
                break;
            case DrawMode.Line: g.DrawLine(pen, Start, End); break;
            case DrawMode.Rectangle: { var r = GetRect(); g.DrawRectangle(pen, r); break; }
            case DrawMode.Ellipse: { var r = GetRect(); g.DrawEllipse(pen, r); break; }
        }
    }
}