namespace Serial;
using Models;

public class VirtualControlConverter : IControlConverter
{
    public VirtualControlConverter()
    {
        
    }

    public Control ConvertControl(ActionMap actionMap)
    {
        return new Control();
    }
}