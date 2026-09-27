using Models;
using Eto.Drawing;
using OpenCvSharp;
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

    public void DetectImageLines()
    {

        Mat image = Cv2.ImRead("UploadedImages/latest.png");
        Mat dstImage = new();
        Cv2.Resize(image, dstImage, new OpenCvSharp.Size(800, 600));

        Mat gray = new();

        Cv2.CvtColor(dstImage, gray, ColorConversionCodes.RGB2GRAY);

        var lsd = LineSegmentDetector.Create();

        Vec4f[] lines;
        double[] widths;
        double[] precisions;
        double[] nfas;
        
        lsd.Detect(gray, out lines, out widths, out precisions, out nfas);
        DrawLines(lines);

        Cv2.WaitKey();
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
        string outputDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "VirtualSketches");
        Directory.CreateDirectory(outputDirectoryPath);
        string outputFilePath = Path.Combine(outputDirectoryPath, "sketch-output.png");

        SketchWindowHost.Invoke(() =>
        {
            using var bitmap = window.CaptureBitmap();
            bitmap.Save(outputFilePath, ImageFormat.Png);
        });
    }

    private Vec4f[] ConnectLines(Vec4f[] lines)
    {
        Vec4f[] connectedLines = lines;

        foreach (var line in lines)
        {
            
        }

        return connectedLines;
    }
    private void DrawLines(Vec4f[] lines)
    {
        Mat blankImage = Cv2.ImRead("VirtualSketches/black_800x600.png");
        OpenCvSharp.Point p1 = new OpenCvSharp.Point();
        OpenCvSharp.Point p2 = new OpenCvSharp.Point();

        foreach (var line in lines)
        {
            p1.X = (int)line.Item0;
            p1.Y = (int)line.Item1;
            p2.X = (int)line.Item2;
            p2.Y = (int)line.Item3;

            if (Math.Abs(p1.X - p2.X) < 10 && Math.Abs(p1.Y - p2.Y) < 10) continue;

            Cv2.Line(
                blankImage,
                p1,
                p2,
                Scalar.White,
                2,
                LineTypes.AntiAlias);
        }
        Cv2.ImShow("Detected Lines", blankImage);
    }
}
