using Eto.Forms;

namespace Serial;

// Runs the Eto UI loop on its own thread since Kestrel already owns the main thread.
public static class SketchWindowHost
{
    private static readonly object _lock = new();
    private static SketchWindow? _window;
    private static Application? _application;

    public static SketchWindow GetWindow()
    {
        lock (_lock)
        {
            if (_window is not null) return _window;

            using var ready = new ManualResetEventSlim();
            var thread = new Thread(() =>
            {
                _application = new Application();
                _window = new SketchWindow();
                ready.Set();
                _application.Run(_window);
            })
            {
                IsBackground = true
            };
            if (OperatingSystem.IsWindows())
            {
                thread.SetApartmentState(ApartmentState.STA);
            }
            thread.Start();

            ready.Wait();
            return _window!;
        }
    }

    public static void Invoke(Action action)
    {
        _application!.Invoke(action);
    }
}
