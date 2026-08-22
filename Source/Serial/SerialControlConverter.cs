namespace Serial;
using Models;

public class SerialControlConverter : IControlConverter
{
    public SerialControlConverter()
    {
        
    }

    public Control ConvertControl(ActionMap actionMap)
    {
        return new Control();
    }
    
    public void DrawCircle()
    {
        // Implementation for drawing a circle using serial communication
    }
}