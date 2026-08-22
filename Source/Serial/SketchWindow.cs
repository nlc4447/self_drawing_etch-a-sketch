using Eto.Drawing;
using Eto.Forms;

namespace Serial;

// Displays sketch progress live instead of only writing a final image to disk.
public class SketchWindow : Form
{
    private readonly List<PointF> _points = new();
    private readonly Drawable _canvas;

    public SketchWindow()
    {
        Title = "Self Drawing Etch-A-Sketch";
        ClientSize = new Size(500, 500);

        _canvas = new Drawable();
        _canvas.Paint += (_, e) => Render(e.Graphics);
        Content = _canvas;
    }

    public void Reset()
    {
        _points.Clear();
        _canvas.Invalidate();
    }

    public void PlotPoint(PointF point)
    {
        _points.Add(point);
        _canvas.Invalidate();
    }

    public Bitmap CaptureBitmap()
    {
        var bitmap = new Bitmap(ClientSize.Width, ClientSize.Height, PixelFormat.Format32bppRgba);
        using var graphics = new Graphics(bitmap);
        Render(graphics);
        return bitmap;
    }

    private void Render(Graphics graphics)
    {
        graphics.Clear(Colors.White);
        using var pen = new Pen(Colors.Blue, 2);
        for (int i = 1; i < _points.Count; i++)
        {
            graphics.DrawLine(pen, _points[i - 1], _points[i]);
        }
    }
}
