using Models;
using Eto.Drawing;
namespace Serial;

/// <summary>
/// A test model of the etch-a-sketch. It uses a drawing library to emulate the cursor in an etch-a-sketch
/// and allows for virtual testing of the model.
/// </summary>
public class VirtualControlConverter : IControlConverter
{
    public VirtualControlConverter()
    {
        
    }

    public Control ConvertControl(ActionMap actionMap)
    {
        return new Control();
    }

    public void DrawCircle()
    {
        const float centerX = 250, centerY = 250, radius = 150;

        var window = SketchWindowHost.GetWindow();
        SketchWindowHost.Invoke(window.Reset);

        var points = new List<PointF>();
        for (double angle = 0; angle <= 360; angle += 2)
        {
            double radians = angle * Math.PI / 180;
            points.Add(new PointF(
                centerX + (float)(radius * Math.Cos(radians)),
                centerY + (float)(radius * Math.Sin(radians))));
        }

        // Animate on a background thread so this call returns immediately for the HTTP request.
        Task.Run(async () =>
        {
            foreach (var point in points)
            {
                var plotted = point;
                SketchWindowHost.Invoke(() => window.PlotPoint(plotted));
                await Task.Delay(15);
            }

            SaveCircleImage(window);
        });
    }

    private void SaveCircleImage(SketchWindow window)
    {
        string outputDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");
        Directory.CreateDirectory(outputDirectoryPath);
        string outputFilePath = Path.Combine(outputDirectoryPath, "sketch-output.png");

        SketchWindowHost.Invoke(() =>
        {
            using var bitmap = window.CaptureBitmap();
            bitmap.Save(outputFilePath, ImageFormat.Png);
        });
    }
}
